using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        // Ищем объект с таким же именем на сцене
        GameObject existingObject = GameObject.Find(gameObject.name);

        // Если объект уже существует и это не текущий объект, уничтожаем новый объект
        if (existingObject != null && existingObject != this.gameObject)
        {
            Destroy(gameObject);
            return;
        }

        // Запрещаем уничтожение объекта при загрузке новой сцены
        DontDestroyOnLoad(gameObject);
    }
}