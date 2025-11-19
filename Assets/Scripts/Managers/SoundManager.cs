using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Sourcer")]
    public AudioSource[] audioSources;
    public SoundConfig[] musics;
    public SoundConfig[] sfx;

    private void Start()
    {
        EventsManager.audioEvent += PlaySFX;
    }
    void StartMusic(int number)
    {
        
    }

    void PlaySFX(AudioEvent eventsData)
    {
        SoundConfig currentSfx = sfx[eventsData.sfxIndex];
        audioSources[eventsData.audioSource].clip = currentSfx.audioclip;
        audioSources[eventsData.audioSource].volume = Random.RandomRange(currentSfx.volumeRange.x, currentSfx.volumeRange.y);
        audioSources[eventsData.audioSource].pitch = Random.RandomRange(currentSfx.pitchRange.x, currentSfx.pitchRange.y);

        audioSources[eventsData.audioSource].Play();
    }
}
