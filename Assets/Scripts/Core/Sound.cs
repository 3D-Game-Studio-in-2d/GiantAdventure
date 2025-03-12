using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Sound : MonoBehaviour
{
    [SerializeField] public AudioClip[] clips;
    
    private AudioSource _audioSource => GetComponent<AudioSource>();
    private static List<Sound> _sounds = new List<Sound>(0);
    
    public SoundSettings Settings { get; private set; }

    private void Awake()
    {
        _sounds.Add(this);
        Settings = new SoundSettings();
    }

    public static void UpdateAllMasterVolume(float masterVolume)
    {
        foreach (var sound in _sounds)
        {
            sound.Settings.MasterVolume = masterVolume;
        }
    }
    
    public static void UpdateAllSfxVolume(float sfxVolume)
    {
        foreach (var sound in _sounds)
        {
            sound.Settings.SfxVolume = sfxVolume;
        }
    }
    
    public static void UpdateAllMusicVolume(float musicVolume)
    {
        foreach (var sound in _sounds)
        {
            sound.Settings.MusicVolume = musicVolume;
        }
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
            PlaySfx(clip, volume, destroyed, allowOverlap, pitch1, pitch2);
            return true;
        }
    }
    
    public bool PlayMusic(string clipName, float volume = 1f, bool destroyed = false, bool allowOverlap = true, 
        float pitch = 1f)
    {
        var clip = GetClipByName(clipName);
        
        if (clip == null)
        {
            return false;
        }
        else
        {
            PlayMusic(clip, volume, destroyed, allowOverlap, pitch);
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
    
    public void PlaySfx(AudioClip clip, float volume = 1f, bool destroyed = false, bool allowOverlap = true,
        float pitch1 = 0.8f, float pitch2 = 1.2f)
    {
        if (_audioSource.isPlaying && !allowOverlap)
        {
            return;
        }
        
        volume = Mathf.Clamp01(volume) * Settings.SfxVolume;
        var pitch = Random.Range(pitch1, pitch2);
        PlayClip(clip, volume, destroyed, pitch);
    }

    public void PlayMusic(AudioClip clip, float volume = 1f, bool destroyed = false, bool allowOverlap = true,
        float pitch = 1f)
    {
        if (_audioSource.isPlaying && !allowOverlap)
        {
            return;
        }
        
        volume = Mathf.Clamp01(volume) * Settings.MusicVolume;
        PlayClip(clip, volume, destroyed, pitch);
    }

    private void PlayClip(AudioClip clip, float volume, bool destroyed, float pitch)
    {
        volume = Mathf.Clamp01(volume);
        _audioSource.pitch = pitch;
        
        if (destroyed)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
        }
        else
        {
            _audioSource.PlayOneShot(clip, volume);
        }
    }
}
