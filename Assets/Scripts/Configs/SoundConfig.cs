using UnityEngine;

[CreateAssetMenu(fileName = "SoundConfig", menuName = "Configs/SoundConfig")]
public class SoundConfig : ScriptableObject
{
    [Header ("Player Sound")]
    [Header ("PlayerMove Sound")]
    public AudioClip movePlayerSound;
    [Header ("PlayerAttack Sound")]
    public AudioClip attackPlayerSound;
    
    [Space(100)]
    [Header ("Music Sound")]
    public AudioClip musicSound1;
    public AudioClip musicSound2;
}