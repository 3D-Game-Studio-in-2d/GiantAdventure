using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class ArraySound
{
    public string name;
    [Range(-3f, 3f)]
    public float pitchStart = 1f; 
    [Range(-3f, 3f)]
    public float pitchEnd = 1f;

    [Range(0f, 1f)]
    public float volume = 1f;
    public AudioClip[] clips; 
}