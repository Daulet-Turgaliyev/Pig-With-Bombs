using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsOfEnemies : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    
    [SerializeField]
    private AudioClip[] _clips;

    public void UpdateState(bool isAngry)
    {
        if(source == null) return;
        if (isAngry)
        {
            source.clip = _clips[1];
            source.Play(0);
        }
        else
        {
            source.clip = _clips[0];
            source.Play(0);
        }
    }
}
