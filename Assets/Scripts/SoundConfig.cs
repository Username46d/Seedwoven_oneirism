using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundConfig", menuName = "Sounds")]
public class SoundConfig : ScriptableObject
{
    [Header("Configurate")]
    public AudioClip audioclip;
    public Vector2 volumeRange = new Vector2(0.8f, 1.2f);
    public Vector2 pitchRange = new Vector2(0.8f, 1.2f);
}