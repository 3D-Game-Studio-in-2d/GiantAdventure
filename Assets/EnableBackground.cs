using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBackground : MonoBehaviour
{
    [SerializeField] private GameObject backgroundUI;

    public static EnableBackground Instance; // НАДО ИЗБЕГАТЬ Instance, ПОКА ЧТО ВРЕМЕННОЕ РЕШЕНИЕ

    private void Start()
    {
        Debug.Log("EnableBackground Start");
        Instance = this;
        gameObject.SetActive(false);
        backgroundUI.SetActive(false);
    }

    private void OnEnable()
    {
        backgroundUI.SetActive(true);
    }
}
