using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static EventsManager Instance;
    private void Start()
    {
        Instance = this;
    }
    public static event System.Action<AudioEvent> audioEvent;
    public static event System.Action<ParticleEvent> particleEvent;
    public void DoAudioEvents(AudioEvent enentsData) 
    {
        audioEvent.Invoke(enentsData);
    }
    public void DoParticleEvents(ParticleEvent enentsData)
    {
        particleEvent.Invoke(enentsData);
    }
}
public struct AudioEvent
{
    public int sfxIndex;
    public int audioSource;

    public AudioEvent( int s, int a) => (sfxIndex, audioSource) = (s, a);
}
public struct ParticleEvent
{
    public Vector3 position;
    public int effectIndex;

    public ParticleEvent(Vector3 pos, int e) => (position, effectIndex) = (pos, e);
}
//public struct UIEvent
//{
//    public Vector3 position;
//    public int effectIndex;
//    public int sfxIndex;
//    public int audioSource;

//    public UIEvent(Vector3 pos, int e, int s, int a) => (position, effectIndex, sfxIndex, audioSource) = (pos, e, s, a);
//}
