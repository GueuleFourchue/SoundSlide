using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHalo : MonoBehaviour {

    public List<SpriteRenderer> haloSprites = new List<SpriteRenderer>();
    public ParticleSystem ps;
    public Transform avatar;
    SpriteRenderer nextSprite;

	public AudioSource audioSource;

    private void Start()
    {
        PlaySFX(0.2f);
        ps.Play();
    }

    public void test(){
        
    }

    void Update()
    {
        for (int i = 0; i< haloSprites.Count; i++)
        {
            if (haloSprites[i].transform.position.z < avatar.position.z)
            {
				PlaySFX (0.2f);

				nextSprite = haloSprites[i];
                ps.Play();
                haloSprites.RemoveAt(i);
            }
        }

        if (nextSprite!= null)
        {
            nextSprite.enabled = false;
            nextSprite = null;
        }
    }

	void PlaySFX(float volume)
	{
		audioSource.volume = volume;
		audioSource.Play ();
	}
}
