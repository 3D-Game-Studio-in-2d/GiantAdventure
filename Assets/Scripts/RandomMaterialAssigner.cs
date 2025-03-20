using System.Collections.Generic;
using UnityEngine;

public class RandomMaterialAssigner : MonoBehaviour
{
    public List<Material> materials; // ������ ����������
    public List<Transform> excludedObjects; // ������ ��������, ��� ��������� �� ����� ��������

    void Start()
    {
        AssignRandomMaterials();
    }

    public void AssignRandomMaterials()
    {
        // �������� ��� ��������� � ������� ������� � ��� �������� ��������
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            // ���������, �������� �� ������ �����������
            if (excludedObjects.Contains(renderer.transform))
            {
                continue; // ���������� ���� ��������
            }

            // ����������� ��������� ��������
            if (materials.Count > 0)
            {
                int randomIndex = Random.Range(0, materials.Count);
                renderer.material = materials[randomIndex];
            }
        }
    }
}
