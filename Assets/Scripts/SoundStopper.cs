using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundStopper : MonoBehaviour
{
    private AudioSource ass;

    void Awake()
    {
        ass = this.GetComponent<AudioSource>();
    }
    
    void Update ()
    {
        if(!ass.isPlaying)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
