using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Sound : MonoBehaviour
{
    [SerializeField] public AudioClip[] clips;
    
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
    
    public bool PlaySfx(string clipName, float volume = 1f, bool destroyed = false, bool allowOverlap = true,
        float pitch1 = 0.8f, float pitch2 = 1.2f)
    {
        var clip = GetClipByName(clipName);
        
        if (clip == null)
        {
            return false;
        }
        else
        {
            PlaySfx(clip, destroyed, allowOverlap);
            return true;
        }
    }
    
    private AudioClip GetClipByName(string clipName)
    {
        foreach (var clip in clips)
        {
            if (clip.name == clipName)
            {
                return clip;
            }
        }

        return null;
    }
    
    public void PlaySfx(AudioClip clip, bool destroyed = false, bool allowOverlap = true)
    {
        if (_audioSource.isPlaying && !allowOverlap)
        {
            return;
        }
        PlayClip(clip, destroyed);
    }

    private void PlayClip(AudioClip clip, bool destroyed)
    {
        _audioSource.pitch = 1;
        
        if (destroyed)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position, _volume);
        }
        else
        {
            _audioSource.PlayOneShot(clip, _volume);
            Debug.Log("Где" + clip.name + "    " + _volume);
        }
    }
}
