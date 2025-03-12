using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AudioSettingsUI : MonoBehaviour
{
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;

    [SerializeField] private AudioSource _musicSource;
    
    private const string MASTER_VOLUME_KEY = "MasterVolume";
    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private const string SFX_VOLUME_KEY = "SfxVolume";

    private float _masterVolume;
    private float _musicVolume;
    private float _sfxVolume;

    private void Start()
    {
        Initialize();
        
        _masterVolumeSlider.value = _masterVolume;
        _musicVolumeSlider.value = _musicVolume;
        _sfxVolumeSlider.value = _sfxVolume;
        
        _masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        _musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        _sfxVolumeSlider.onValueChanged.AddListener(SetSfxVolume);

        UpdateVolume();
    }
    
    [Inject]
    public void Initialize()
    {
        _masterVolume = Mathf.Clamp01(PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, 1f));
        _musicVolume = Mathf.Clamp01(PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f));
        _sfxVolume = Mathf.Clamp01(PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1f));
        
        SetMasterVolume(_masterVolume);
        SetMusicVolume(_musicVolume);
        SetSfxVolume(_sfxVolume);
    }

    public void SetMasterVolume(float volume)
    {
        _masterVolume = Mathf.Clamp01(volume);
        UpdateVolume();
        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
    }

    public void SetSfxVolume(float volume)
    {
        _sfxVolume = Mathf.Clamp01(volume);
        UpdateVolume();
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
    }

    public void SetMusicVolume(float volume)
    {
        _musicVolume = Mathf.Clamp01(volume);
        UpdateVolume();
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
    }

    private void UpdateVolume()
    {
        _musicSource.volume = _musicVolume * _masterVolume;
        Sound.UpdateAllVolume(_sfxVolume * _masterVolume);
    }
    
    private void OnDestroy()
    {
        PlayerPrefs.Save();
    }
}