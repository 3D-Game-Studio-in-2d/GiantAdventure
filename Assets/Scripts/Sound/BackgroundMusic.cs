using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField]
    private string mainMenuSceneName = "MainMenu";
    [SerializeField]
    private AudioClip[] mainMenuMusic;
    
    [SerializeField]
    private string lvl1SceneName = "Vilage (1)";
    [SerializeField]
    private AudioClip[] lvl1Music;
    
    [SerializeField]
    private string lvl2SceneName = "MainMenu";
    [SerializeField]
    private AudioClip[] lvl2Music;
    
    [SerializeField]
    private string lvl3SceneName = "MainMenu";
    [SerializeField]
    private AudioClip[] lvl3Music;
    
    [SerializeField]
    private string lvl4SceneName = "MainMenu";
    [SerializeField]
    private AudioClip[] lvl4Music;
    
    [SerializeField]
    private string lvl5SceneName = "MainMenu";
    [SerializeField]
    private AudioClip[] lvl5Music;
    
    [SerializeField]
    private string lvl6SceneName = "MainMenu";
    [SerializeField]
    private AudioClip[] lvl6Music;

    private AudioClip[] _currentMusic;
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        BackgroundMusic existingMusic = FindObjectOfType<BackgroundMusic>();

        if (existingMusic != null && existingMusic != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        _currentMusic = mainMenuMusic;
        PlayMusic(0);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Новая сцена: {scene.name} (режим: {mode})");
        
        if (scene.name == mainMenuSceneName)
        {
            _currentMusic = mainMenuMusic;
        }
        else if (scene.name == lvl1SceneName)
        {
            _currentMusic = lvl1Music;
        }
        else if (scene.name == lvl2SceneName)
        {
            _currentMusic = lvl2Music;
        }
        else if (scene.name == lvl3SceneName)
        {
            _currentMusic = lvl3Music;
        }
        else if (scene.name == lvl4SceneName)
        {
            _currentMusic = lvl4Music;
        }
        else if (scene.name == lvl5SceneName)
        {
            _currentMusic = lvl5Music;
        }
        else if (scene.name == lvl6SceneName)
        {
            _currentMusic = lvl6Music;
        }

        PlayMusic(0);
    }

    public void PlayMusic(int index)
    {
        _audioSource.clip = _currentMusic[index];
        _audioSource.Play();
    }
}
