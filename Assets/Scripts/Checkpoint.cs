using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    
    public SpriteRenderer haloSprites;
    public ParticleSystem ps;
    public AudioSource audioSource;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && haloSprites.enabled == true){
            PlaySFX(0.2f);
            ps.Play();
            haloSprites.enabled = false;
        }
    }

    void PlaySFX(float volume)
    {
        audioSource.volume = volume;
        audioSource.Play();
    }
}
