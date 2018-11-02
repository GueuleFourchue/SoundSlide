using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxMenuInGame : MonoBehaviour {

	[Header("Sound")]
	public AudioSource audioSource;
	public AudioClip sfxMenuMouseover;
	public AudioClip sfxMenuSubmit;
	public AudioClip sfxMenuBack;
	public AudioClip sfxMenuPlay;

	public void PlaySFX(AudioClip clip, float volume)
	{
		audioSource.volume = volume;
		audioSource.clip = clip;
		audioSource.pitch = Random.Range (0.9f, 1.1f);
		audioSource.Play ();
	}
}
