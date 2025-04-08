using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Sound : MonoBehaviour
{
    [SerializeField] public ArraySound[] sounds;
    
    private AudioSource _audioSource;
    private static Action<float> OnUpdateVolume;
    
    private float _volume = 1f;
    private const string SFX_VOLUME_KEY = "SfxVolume";
    
    private void Awake()
    {
        OnUpdateVolume += UpdateVolume;
        _audioSource = GetComponent<AudioSource>();
        _volume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1f);
    }

    private void UpdateVolume(float value)
    {
        _volume = value;
        if (!_audioSource) return;
        _audioSource.volume = value;
    }
    
    public static void UpdateAllVolume(float value)
    {
        OnUpdateVolume?.Invoke(value);
    }
    
    public bool PlaySfx(string clipName, bool destroyed = false, bool allowOverlap = true)
    {
        foreach (var sound in sounds)
        {
            if (sound.name == clipName)
            {
                PlayClip(sound, destroyed, allowOverlap);
                return true;
            }
        }
        return false;
    }

    private void PlayClip(ArraySound sound, bool destroyed, bool allowOverlap)
    {
        // Если overlap запрещен и звук уже играет — пропускаем
        if (!allowOverlap && _audioSource.isPlaying)
        {
            Debug.Log("Playing clip return: " + sound.name);
            return;
        }
        Debug.Log("Playing clip: " + sound.name);
        int clipIndex = Random.Range(0, sound.clips.Length);
        AudioClip clip = sound.clips[clipIndex];
        
        _audioSource.pitch = Random.Range(sound.pitchStart, sound.pitchEnd);
        _audioSource.volume = sound.volume * _volume; 
        
        if (destroyed)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position, _audioSource.volume);
        }
        else
        {
            _audioSource.PlayOneShot(clip, _audioSource.volume);
        }
    }
}