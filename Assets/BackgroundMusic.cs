using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private Sound _sound;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _sound = GetComponent<Sound>();
    }

    private void Start()
    {
        if (!_sound.PlayMusic("music"))
        {
            Debug.Log("Отсутствует музыка");
        }
    }
}
