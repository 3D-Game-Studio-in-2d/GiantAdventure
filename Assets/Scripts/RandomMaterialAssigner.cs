using System.Collections.Generic;
using UnityEngine;

public class RandomMaterialAssigner : MonoBehaviour
{
    public List<Material> materials; // Список материалов
    public List<Transform> excludedObjects; // Список объектов, чьи материалы не будут меняться

    void Start()
    {
        AssignRandomMaterials();
    }

    public void AssignRandomMaterials()
    {
        // Получаем все рендереры в текущем объекте и его дочерних объектах
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            // Проверяем, является ли объект исключенным
            if (excludedObjects.Contains(renderer.transform))
            {
                continue; // Пропускаем этот рендерер
            }

            // Присваиваем случайный материал
            if (materials.Count > 0)
            {
                int randomIndex = Random.Range(0, materials.Count);
                renderer.material = materials[randomIndex];
            }
        }
    }
}
