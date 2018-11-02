using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PauseMenuAnims : MonoBehaviour {

	public SfxMenuInGame sfxScript;

	[Header ("Images")]
	public Image replayImage;
	public Image mainMenuImage;
	public Image resumeImage;

	public void ReplayHover ()
	{
		HoverOpacity (replayImage, true);
		sfxScript.PlaySFX (sfxScript.sfxMenuMouseover, 0.2f);
	}
	public void ReplayHoverEnd ()
	{
		HoverOpacity (replayImage, false);
	}
	public void MainMenuHover ()
	{
		HoverOpacity (mainMenuImage, true);
		sfxScript.PlaySFX (sfxScript.sfxMenuMouseover, 0.2f);
	}
	public void MainMenuHoverEnd ()
	{
		HoverOpacity (mainMenuImage, false);
	}
	public void ResumeHover ()
	{
		HoverOpacity (resumeImage, true);
		sfxScript.PlaySFX (sfxScript.sfxMenuMouseover, 0.2f);
	}
	public void ResumeHoverEnd ()
	{
		HoverOpacity (resumeImage, false);
	}

	void HoverOpacity (Image image, bool hover)
	{
		Color newColor = image.color;
		float alpha;
		if (hover)
			alpha = 1;
		else
			alpha = 0.4f;
		newColor.a = alpha;
		image.color = newColor;
	}
}
