using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainMenuAnims : MonoBehaviour {



	[Header ("Buttons")]
	public Button buttonPlay;
	public Button buttonOptions;
	public Button buttonCredits;
	public Button buttonExit;
	public Button buttonBackCredits;
	public Button buttonBackOptions;

	[Header ("Images")]
	public Image playImage;
	public Image optionsImage;
	public Image creditsImage;
	public Image exitImage;
	public Image BackCreditsImage;
	public Image noImage;
	public Image yesImage;
	public Image optionsBackImage;

	public CanvasGroup canvasGroup;

	[Header("Sound")]
	public AudioSource audioSource;
	public AudioClip sfxMenuMouseover;



	public void PlayHover ()
	{
		ButtonsHoverAnim (buttonPlay, -240.5f, playImage, true); 
		PlaySFX (sfxMenuMouseover, 0.2f);
	}
	public void PlayHoverEnd ()
	{
		ButtonsHoverAnim (buttonPlay, -295.6f, playImage, false); 
	}
	public void OptionsHover ()
	{
		ButtonsHoverAnim (buttonOptions, -240.5f, optionsImage, true); 
		PlaySFX (sfxMenuMouseover, 0.2f);
	}
	public void OptionsHoverEnd ()
	{
		ButtonsHoverAnim (buttonOptions, -295.6f, optionsImage, false); 
	}
	public void CreditsHover ()
	{
		ButtonsHoverAnim (buttonCredits, -240.5f, creditsImage, true); 
		PlaySFX (sfxMenuMouseover, 0.2f);
	}
	public void CreditsHoverEnd ()
	{
		ButtonsHoverAnim (buttonCredits, -295.6f, creditsImage, false); 
	}
	public void ExitHover ()
	{
		ButtonsHoverAnim (buttonExit, -317f, exitImage, true); 
		PlaySFX (sfxMenuMouseover, 0.2f);
	}
	public void ExitHoverEnd ()
	{
		ButtonsHoverAnim (buttonExit, -371, exitImage, false); 
	}
	public void CreditsBackHover ()
	{
		ButtonsHoverAnim (buttonBackCredits, 325, BackCreditsImage, true); 
		PlaySFX (sfxMenuMouseover, 0.2f);
	}
	public void CreditsBackHoverEnd ()
	{
		ButtonsHoverAnim (buttonBackCredits, 358.8f, BackCreditsImage, false); 
	}
	public void OptionsBackHover ()
	{
		ButtonsHoverAnim (buttonBackOptions, 325, optionsBackImage, true); 
		PlaySFX (sfxMenuMouseover, 0.2f);
	}
	public void OptionsBackHoverEnd ()
	{
		ButtonsHoverAnim (buttonBackOptions, 358.8f, optionsBackImage, false); 
	}

		
	void ButtonsHoverAnim(Button button, float xPosition, Image image, bool hover)
	{
		Color newColor = image.color;
		float alpha;
		if (hover)
			alpha = 1;
		else
			alpha = 0.6f;
		newColor.a = alpha;
		image.color = newColor;
    
		button.transform.DOKill ();
		button.transform.DOLocalMoveX (xPosition, 0.5f).SetEase(Ease.OutQuart);
	}

	public void YesHover()
	{
		HoverOpacity (yesImage, true);
		PlaySFX (sfxMenuMouseover, 0.2f);
	}
	public void YesHoverEnd()
	{
		HoverOpacity (yesImage, false);
	}
	public void NoHover()
	{
		HoverOpacity (noImage, true);
		PlaySFX (sfxMenuMouseover, 0.2f);
	}
	public void NoHoverEnd()
	{
		HoverOpacity (noImage, false);
	}

	void HoverOpacity (Image image, bool hover)
	{
		Color newColor = image.color;
		float alpha;
		if (hover)
			alpha = 1;
		else
			alpha = 0.3f;
		newColor.a = alpha;
		image.color = newColor;
	}

	public void PlaySFX(AudioClip clip, float volume)
	{
		audioSource.volume = volume;
		audioSource.clip = clip;
		audioSource.pitch = Random.Range (0.9f, 1.1f);
		audioSource.Play ();
	}
}
