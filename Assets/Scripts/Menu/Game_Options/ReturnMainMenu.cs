using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ReturnMainMenu : MonoBehaviour {

	public MenuManager menuManager;
	public Button buttonReturn;
	public Button buttonPlay;

	public GameObject LevelSelectionMenu;
	public GameObject mainMenu;

	public GameOptionsMenu gameOptionsMenu;


	public void ReturnHover ()
	{
		ButtonsHoverAnim (buttonReturn, 335);
		menuManager.PlaySFX (menuManager.sfxMenuMouseover, 0.2f);
		HoverOpacity (buttonReturn.GetComponentInChildren<Image> (), true);
	}
	public void ReturnHoverEnd ()
	{
		ButtonsHoverAnim (buttonReturn, 361); 
		HoverOpacity (buttonReturn.GetComponentInChildren<Image> (), false);
	}
	public void PlayHover ()
	{
		ButtonsHoverAnim (buttonPlay, 304);
		menuManager.PlaySFX (menuManager.sfxMenuMouseover, 0.2f);
		HoverOpacity (buttonPlay.GetComponentInChildren<Image> (), true);
	}
	public void PlayHoverEnd ()
	{
		ButtonsHoverAnim (buttonPlay, 340); 
		HoverOpacity (buttonPlay.GetComponentInChildren<Image> (), false);
	}

	void ButtonsHoverAnim(Button button, float xPosition)
	{
		button.transform.DOKill ();
		button.transform.DOLocalMoveX (xPosition, 0.5f).SetEase(Ease.OutQuart);
	}

	public void ReturnToMainMenu()
	{
		buttonReturn.transform.DOKill ();
		buttonReturn.transform.DOLocalMoveX (515f, 0.5f).SetEase(Ease.OutQuart);
		menuManager.ReturnToMain (mainMenu, LevelSelectionMenu);
		menuManager.PlaySFX (menuManager.sfxMenuBack, 1f);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (gameOptionsMenu.levelOptionsUIenabled == false) 
			{
				menuManager.ReturnToMain (mainMenu, LevelSelectionMenu);
			}
		}
	}

	void HoverOpacity (Image image, bool hover)
	{
		Color newColor = image.color;
		float alpha;
		if (hover)
			alpha = 1;
		else
			alpha = 0.5f;
		newColor.a = alpha;
		image.color = newColor;
	}
}
