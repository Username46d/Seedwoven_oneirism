using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Sourcer")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource mouseSource;
    public SoundConfig[] musics;
    public SoundConfig[] sfx;

    private void Start()
    {
        GameManager.gameManagerEvents += PlaySFX;
        TIleManager.tileManagerEvents += PlaySFX;
    }
    void StartMusic(int number)
    {
        
    }

    void PlaySFX(EventsData eventsData)
    {
        SoundConfig currentSfx = sfx[eventsData.sfxIndex];
        if (eventsData.sfxIndex == 0)
        {
            mouseSource.clip = currentSfx.audioclip;
            mouseSource.volume = Random.RandomRange(currentSfx.volumeRange.x, currentSfx.volumeRange.y);
            mouseSource.pitch = Random.RandomRange(currentSfx.pitchRange.x, currentSfx.pitchRange.y);

            mouseSource.Play();
            return;
        }
        sfxSource.clip = currentSfx.audioclip;
        sfxSource.volume = Random.RandomRange(currentSfx.volumeRange.x, currentSfx.volumeRange.y);
        sfxSource.pitch = Random.RandomRange(currentSfx.pitchRange.x, currentSfx.pitchRange.y);

        sfxSource.Play();
    }
}
