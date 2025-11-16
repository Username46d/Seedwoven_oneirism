using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [Header("Настройки")]
    public ParticleSystem[] effects;

    private void Start()
    {
        GameManager.gameManagerEvents += PlayEffect;
        TIleManager.tileManagerEvents += PlayEffect;
    }

   public void PlayEffect(EventsData eventsData)
   {
        ParticleSystem currentEffect = effects[eventsData.effectIndex];
        currentEffect.transform.position = eventsData.position;
        currentEffect.Play();
   }
}
