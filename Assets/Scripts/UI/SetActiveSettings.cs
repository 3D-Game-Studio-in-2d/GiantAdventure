using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveSettings : MonoBehaviour
{
    private GameObject _gameObjectUI; // Объект, который включил настройки

    public void EnableSettings(GameObject gameObjectUI)
    {
        _gameObjectUI = gameObjectUI;
        gameObject.SetActive(true);
        _gameObjectUI.SetActive(false);
    }

    public void DisableSettings()
    {
        gameObject.SetActive(false);
        _gameObjectUI.SetActive(true);
    }
}
