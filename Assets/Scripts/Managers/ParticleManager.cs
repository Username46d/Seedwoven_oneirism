using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [Header("Настройки")]
    public ParticleSystem[] effects;

    private void Start()
    {
        EventsManager.particleEvent += PlayEffect;
    }

   public void PlayEffect(ParticleEvent eventsData)
   {
        ParticleSystem currentEffect = effects[eventsData.effectIndex];
        currentEffect.transform.position = eventsData.position;
        currentEffect.Play();
   }
}
