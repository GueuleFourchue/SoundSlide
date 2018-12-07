using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;
using UnityEngine.EventSystems;

public class GameOptionsMenu : MonoBehaviour
{

    public bool isOnMainMenu = true;

    [Header("QuiGame")]
    public GameObject quitGame;
    public Button yesQuit;
    bool quitGameOn;

    [Header("Options")]
    public GameObject options;
    bool optionsOn;

    [Header("Credits")]
    public GameObject credits;
    bool creditsOn;

    public Transform container;
    public AvatarMenu avatarMenu;

    public bool peaceful;
    public bool normal;
    public bool flawless;
    public bool speed75;
    public bool speed100;
    public bool speed125;
    public bool speed150;
    public bool noFarLanes;
    public bool noNearLanes;

    [Header("Arrow")]
    public RectTransform selecArrow;
    public RectTransform arrow1;
    public RectTransform arrow2;

    [Header("info Level")]
    public info_managerscript infoLevel;

    [Header("Image Scripts")]
    public Image peacefulSprite;
    public Image normalSprite;
    public Image flawlessSprite;
    public Image speed75Sprite;
    public Image speed100Sprite;
    public Image speed125Sprite;
    public Image speed150Sprite;
    public Image noFarLanesSprite;
    public Image noNearLanesSprite;

    [Header("Sprites")]
    public Sprite peacefulSpriteB;
    public Sprite normalSpriteB;
    public Sprite flawlessSpriteB;
    public Sprite speed75SpriteB;
    public Sprite speed100SpriteB;
    public Sprite speed125SpriteB;
    public Sprite speed150SpriteB;
    public Sprite noFarLanesSpriteB;
    public Sprite noNearLanesSpriteB;

    public Sprite peacefulSpriteW;
    public Sprite normalSpriteW;
    public Sprite flawlessSpriteW;
    public Sprite speed75SpriteW;
    public Sprite speed100SpriteW;
    public Sprite speed125SpriteW;
    public Sprite speed150SpriteW;
    public Sprite noFarLanesSpriteW;
    public Sprite noNearLanesSpriteW;

    public Sprite peacefulSpriteTT;
    public Sprite normalSpriteTT;
    public Sprite flawlessSpriteTT;
    public Sprite speedSpriteTT;
    public Sprite noFarLanesSpriteTT;
    public Sprite noNearLanesSpriteTT;


    [Header("Toggles")]
    public Toggle togglePeaceful;
    public Toggle toggleNormal;
    public Toggle toggleFlawless;
    public Toggle toggleSpeed75;
    public Toggle toggleSpeed100;
    public Toggle toggleSpeed125;
    public Toggle toggleSpeed150;
    public Toggle toggleNoFarLanes;
    public Toggle toggleNoNearLanes;

    [Header("Buttons")]
    public Button buttonPlay;
    public Button buttonSelectionPlay;
    public Button buttonReturn;

    Vector3 originScale = new Vector3(1, 1, 1);
    //Vector3 hoverScale = new Vector3 (1.07f, 1.07f, 1.07f);
    Vector3 enabledScale = new Vector3(1.2f, 1.2f, 1.2f);

    bool canToggleUI = true;
    bool canDisplayTooltip = true;

    public string sceneToLoad;
    public CanvasGroup canvasGroup;
    public GameObject returnToMainCanvas;

    public bool levelOptionsUIenabled;

    public Text textMultiplier;
    public float scoreMultiplier = 1;
    public Image ttImage;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip sfxMenuMouseover;
    public AudioClip sfxMenuSubmit;
    public AudioClip sfxMenuBack;

    [Header("LoadingScreen")]
    public GameObject loadingScreen;
    public Slider loadingSlider;

    [Header("Script")]
    public MenuScript menuScript;

    public void Peaceful()
    {
        ToggleChangeState(ref peaceful, peacefulSprite, togglePeaceful, peacefulSpriteW, peacefulSpriteB);
        PlaySFX(sfxMenuSubmit, 0.5f);
    }
    public void Normal()
    {
        ToggleChangeState(ref normal, normalSprite, toggleNormal, normalSpriteW, normalSpriteB);
        PlaySFX(sfxMenuSubmit, 0.5f);
    }
    public void Flawless()
    {
        ToggleChangeState(ref flawless, flawlessSprite, toggleFlawless, flawlessSpriteW, flawlessSpriteB);
        PlaySFX(sfxMenuSubmit, 0.5f);
    }
    public void Speed75()
    {
        ToggleChangeState(ref speed75, speed75Sprite, toggleSpeed75, speed75SpriteW, speed75SpriteB);
        PlaySFX(sfxMenuSubmit, 0.5f);
    }
    public void Speed100()
    {
        ToggleChangeState(ref speed100, speed100Sprite, toggleSpeed100, speed100SpriteW, speed100SpriteB);
        PlaySFX(sfxMenuSubmit, 0.5f);
    }
    public void Speed125()
    {
        ToggleChangeState(ref speed125, speed125Sprite, toggleSpeed125, speed125SpriteW, speed125SpriteB);
        PlaySFX(sfxMenuSubmit, 0.5f);
    }
    public void Speed150()
    {
        ToggleChangeState(ref speed150, speed150Sprite, toggleSpeed150, speed150SpriteW, speed150SpriteB);
        PlaySFX(sfxMenuSubmit, 0.5f);
    }
    public void NoFarLanes()
    {
        ToggleChangeState(ref noFarLanes, noFarLanesSprite, toggleNoFarLanes, noFarLanesSpriteW, noFarLanesSpriteB);
        PlaySFX(sfxMenuSubmit, 0.5f);
    }
    public void NoNearLanes()
    {
        ToggleChangeState(ref noNearLanes, noNearLanesSprite, toggleNoNearLanes, noNearLanesSpriteW, noNearLanesSpriteB);
        PlaySFX(sfxMenuSubmit, 0.5f);
    }

    //0 pour la plupart des derniers float même s'ils servent pas, parce que je pouvais pas mettre "null"
    public void PeacefulHover()
    {
        ToggleHoverAnim(togglePeaceful, peacefulSprite, 0.9f, 0);
        PlaySFX(sfxMenuMouseover, 0.2f);
        StartCoroutine(ToolTipOn(peacefulSpriteTT));
    }
    public void NormalHover()
    {
        ToggleHoverAnim(toggleNormal, normalSprite, 0.9f, 0);
        PlaySFX(sfxMenuMouseover, 0.2f);
        StartCoroutine(ToolTipOn(normalSpriteTT));
    }
    public void FlawlessHover()
    {
        ToggleHoverAnim(toggleFlawless, flawlessSprite, 0.9f, 0);
        PlaySFX(sfxMenuMouseover, 0.2f);
        StartCoroutine(ToolTipOn(flawlessSpriteTT));
    }
    public void Speed75Hover()
    {
        ToggleHoverAnim(toggleSpeed75, speed75Sprite, 0.9f, 0);
        PlaySFX(sfxMenuMouseover, 0.2f);
        StartCoroutine(ToolTipOn(speedSpriteTT));
    }
    public void Speed100Hover()
    {
        ToggleHoverAnim(toggleSpeed100, speed100Sprite, 0.9f, 0);
        PlaySFX(sfxMenuMouseover, 0.2f);
        StartCoroutine(ToolTipOn(speedSpriteTT));
    }
    public void Speed125Hover()
    {
        ToggleHoverAnim(toggleSpeed125, speed125Sprite, 0.9f, 0);
        PlaySFX(sfxMenuMouseover, 0.2f);
        StartCoroutine(ToolTipOn(speedSpriteTT));
    }
    public void Speed150Hover()
    {
        ToggleHoverAnim(toggleSpeed150, speed150Sprite, 0.9f, 0);
        PlaySFX(sfxMenuMouseover, 0.2f);
        StartCoroutine(ToolTipOn(speedSpriteTT));
    }
    public void NoFarLanesHover()
    {
        ToggleHoverAnim(toggleNoFarLanes, noFarLanesSprite, 0.9f, 0.8f);
        PlaySFX(sfxMenuMouseover, 0.2f);
        StartCoroutine(ToolTipOn(noFarLanesSpriteTT));
    }
    public void NoNearLanesHover()
    {
        ToggleHoverAnim(toggleNoNearLanes, noNearLanesSprite, 0.9f, 0.8f);
        PlaySFX(sfxMenuMouseover, 0.2f);
        StartCoroutine(ToolTipOn(noNearLanesSpriteTT));
    }


    public void PeacefulHoverEnd()
    {
        ToggleHoverAnim(togglePeaceful, peacefulSprite, 0.5f, 0);
        StartCoroutine(ToolTipOff());
    }
    public void NormalHoverEnd()
    {
        ToggleHoverAnim(toggleNormal, normalSprite, 0.5f, 0);
        StartCoroutine(ToolTipOff());
    }
    public void FlawlessHoverEnd()
    {
        ToggleHoverAnim(toggleFlawless, flawlessSprite, 0.5f, 0);
        StartCoroutine(ToolTipOff());
    }
    public void Speed75HoverEnd()
    {
        ToggleHoverAnim(toggleSpeed75, speed75Sprite, 0.5f, 0);
        StartCoroutine(ToolTipOff());
    }
    public void Speed100HoverEnd()
    {
        ToggleHoverAnim(toggleSpeed100, speed100Sprite, 0.5f, 0);
        StartCoroutine(ToolTipOff());
    }
    public void Speed125HoverEnd()
    {
        ToggleHoverAnim(toggleSpeed125, speed125Sprite, 0.5f, 0);
        StartCoroutine(ToolTipOff());
    }
    public void Speed150HoverEnd()
    {
        ToggleHoverAnim(toggleSpeed150, speed150Sprite, 0.5f, 0);
        StartCoroutine(ToolTipOff());
    }
    public void NoFarLanesHoverEnd()
    {
        ToggleHoverAnim(toggleNoFarLanes, noFarLanesSprite, 0.5f, 1);
        StartCoroutine(ToolTipOff());
    }
    public void NoNearLanesHoverEnd()
    {
        ToggleHoverAnim(toggleNoNearLanes, noNearLanesSprite, 0.5f, 1);
        StartCoroutine(ToolTipOff());

    }



    public void PlayHover()
    {
        ButtonsHoverAnim(buttonPlay, 303.55f);
        buttonPlay.transform.GetChild(0).GetComponent<Image>().DOFade(1, 0.2f);

        PlaySFX(sfxMenuMouseover, 0.2f);
    }
    public void PlayHoverEnd()
    {
        ButtonsHoverAnim(buttonPlay, 340);
        buttonPlay.transform.GetChild(0).GetComponent<Image>().DOFade(0.47f, 0.2f);
    }
    public void ReturnHover()
    {
        ButtonsHoverAnim(buttonReturn, 335);
        buttonReturn.transform.GetChild(0).GetComponent<Image>().DOFade(1, 0.2f);
        PlaySFX(sfxMenuMouseover, 0.2f);
    }
    public void ReturnHoverEnd()
    {
        ButtonsHoverAnim(buttonReturn, 361);
        buttonReturn.transform.GetChild(0).GetComponent<Image>().DOFade(0.47f, 0.2f);
    }


    void PlaySFX(AudioClip clip, float volume)
    {
        audioSource.volume = volume;
        audioSource.clip = clip;
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
    }


    public void ToggleChangeState(ref bool boolean, Image image, Toggle toggle, Sprite white, Sprite black)
    {
        boolean = toggle.isOn;

        if (boolean)
        {
            Color newColor = image.color;
            newColor.a = 1;
            image.color = newColor;

            image.sprite = white;

            toggle.transform.DOScale(enabledScale, 0.1f).SetEase(Ease.OutBack);

            if (arrow1.localPosition.x != -60)
            {
                arrow1.DOLocalMoveX(-60, 0.1f);
                arrow2.DOLocalMoveX(60, 0.1f);
            }
        }
        else
        {
            Color newColor = image.color;
            newColor.a = 0.5f;
            image.color = newColor;

            image.sprite = black;

            toggle.transform.DOScale(originScale, 0.2f);

            if (arrow1.localPosition.x != -65)
            {
                arrow1.DOLocalMoveX(-65, 0.1f);
                arrow2.DOLocalMoveX(65, 0.1f);
            }
        }

        StartCoroutine(GlobalScoreMultiplier());

    }
    public void ToggleHoverAnim(Toggle toggle, Image image, float opacity, float specialOpacity)
    {
        //Arrow
        if (selecArrow.GetComponent<CanvasGroup>().alpha < 1)
            selecArrow.GetComponent<CanvasGroup>().DOFade(1, 0.1f);
        selecArrow.transform.position = toggle.transform.position;

        if (toggle.isOn == false)
        {
            Color newColor = image.color;
            newColor.a = opacity;
            image.color = newColor;

            if (arrow1.localPosition.x != -60)
            {
                arrow1.DOLocalMoveX(-60, 0.1f);
                arrow2.DOLocalMoveX(60, 0.1f);
            }
        }
        else if (toggle.name == "Toggle_NoNear" || toggle.name == "Toggle_NoFar")
        {
            Color newColor = image.color;
            newColor.a = specialOpacity;
            image.color = newColor;
        }
        if (toggle.isOn)
        {
            if (arrow1.localPosition.x != -65)
            {
                arrow1.DOLocalMoveX(-65, 0.1f);
                arrow2.DOLocalMoveX(65, 0.1f);
            }
        }


    }

    void ButtonsHoverAnim(Button button, float xPosition)
    {
        button.transform.DOKill();
        button.transform.DOLocalMoveX(xPosition, 0.5f).SetEase(Ease.OutQuart);
        selecArrow.GetComponent<CanvasGroup>().DOFade(0, 0.05f);
    }

    void Start()
    {
        LoadSettings();

        InputsManager.IM.DisableCanvasGroup(canvasGroup);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            avatarMenu.enabled = true;


            if (isOnMainMenu)
            {
                //QuitGame
                avatarMenu.enabled = false;
                quitGame.SetActive(true);
                quitGame.GetComponent<CanvasGroup>().DOFade(1, 0.35f);
                quitGameOn = true;
                isOnMainMenu = false;
                EventSystem.current.SetSelectedGameObject(yesQuit.gameObject);
            }

            else
            {
                if (quitGameOn)
                {
                    quitGameOn = false;
                    isOnMainMenu = true;

                    InputsManager.IM.DisableCanvasGroup(quitGame.GetComponent<CanvasGroup>(), 0.35f);
                    // quitGame.GetComponent<CanvasGroup>().DOFade(0, 0.35f).OnComplete(() =>
                    // {
                    //     quitGame.SetActive(false);
                    // });
                    EventSystem.current.SetSelectedGameObject(buttonSelectionPlay.gameObject);
                }
                else if (creditsOn)
                {
                    InputsManager.IM.DisableCanvasGroup(credits.GetComponent<CanvasGroup>(), 0.2f);

                    // credits.GetComponent<CanvasGroup>().DOFade(0, 0.2f).OnComplete(() =>
                    // {
                    //     credits.SetActive(false);
                    // });
                    creditsOn = false;
                }
                else if (optionsOn)
                {
                    if (menuScript.inInputMenu)
                    {
                        avatarMenu.enabled = false;
                        menuScript.BackToBaseMenu();
                    }
                    else
                    {
                        InputsManager.IM.DisableCanvasGroup(options.GetComponent<CanvasGroup>(), 0.2f);

                        // options.GetComponent<CanvasGroup>().DOFade(0, 0.2f).OnComplete(() =>
                        // {
                        //     options.SetActive(false);
                        // });
                        optionsOn = false;
                        StartCoroutine(ReturnAnim());
                    }
                }
                else
                {
                    StartCoroutine(ReturnAnim());
                }
            }

        }
    }

    public void Play()
    {
        infoLevel.getInfoGame(peaceful, normal, flawless, speed75, speed100, speed125, speed150, noFarLanes, noNearLanes);
        StartCoroutine(LoadAsynchronously(sceneToLoad));
    }

    public void Return()
    {
        StartCoroutine(ReturnAnim());
        avatarMenu.enabled = true;
        PlaySFX(sfxMenuBack, 1);
    }

    public void Activation()
    {
        isOnMainMenu = false;
        quitGameOn = false;

        if (sceneToLoad == "OPTIONS")
        {
            optionsOn = true;
            avatarMenu.enabled = false;
            options.gameObject.SetActive(true);
            options.transform.DOKill();

            InputsManager.IM.EnableCanvasGroup(options.GetComponent<CanvasGroup>(), 0.5f);

            // options.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
            EventSystem.current.SetSelectedGameObject(null);
        }
        else if (sceneToLoad == "CREDITS")
        {
            creditsOn = true;
            avatarMenu.enabled = false;
            credits.gameObject.SetActive(true);
            credits.transform.DOKill();

            InputsManager.IM.EnableCanvasGroup(credits.GetComponent<CanvasGroup>(), 0.5f);
            // credits.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        }
        else
        {
            StartCoroutine(Activate());
            EventSystem.current.SetSelectedGameObject(buttonPlay.gameObject);
            //ICI
            avatarMenu.enabled = false;
            PlaySFX(sfxMenuSubmit, 0.5f);
        }

    }


    public IEnumerator Activate()
    {
        if (canToggleUI)
        {
            canToggleUI = false;

            container.gameObject.SetActive(true);

            container.DOKill();
            container.DOScale(new Vector3(1, 1, 1), 0.3f);

            // for (float t = 0.0f; t < 1.1f; t += Time.deltaTime * 6)
            // {
            //     canvasGroup.alpha = Mathf.Lerp(0, 1, t);
            //     yield return null;
            // }
            // canvasGroup.alpha = 1;

            InputsManager.IM.EnableCanvasGroup(canvasGroup, 0.3f);
            returnToMainCanvas.SetActive(false);

            canToggleUI = true;
            levelOptionsUIenabled = true;

            //
            EventSystem.current.SetSelectedGameObject(buttonPlay.gameObject);
        }

        yield return 0;
    }

    public IEnumerator ReturnAnim()
    {
        isOnMainMenu = true;

        if (canToggleUI)
        {
            canToggleUI = false;

            container.DOKill();
            container.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.3f);

            returnToMainCanvas.SetActive(true);

            InputsManager.IM.DisableCanvasGroup(canvasGroup, 0.3f);

            // for (float t = 0.0f; t < 1.1f; t += Time.deltaTime * 6)
            // {
            //     canvasGroup.alpha = Mathf.Lerp(1, 0, t);
            //     yield return null;
            // }
            // canvasGroup.alpha = 0;

            container.gameObject.SetActive(false);

            canToggleUI = true;
            levelOptionsUIenabled = false;

            //
            EventSystem.current.SetSelectedGameObject(buttonSelectionPlay.gameObject);

            ReturnHoverEnd();
        }

        yield return 0;
    }

    IEnumerator GlobalScoreMultiplier()
    {
        if (peaceful)
            scoreMultiplier = 0;
        else
        {
            float scoreChanger = 0;

            if (normal)
                scoreChanger += 0;
            if (flawless)
                scoreChanger += 0;
            if (speed75)
                scoreChanger += -0.3f;
            if (speed100)
                scoreChanger += 0;
            if (speed125)
                scoreChanger += 0.3f;
            if (speed150)
                scoreChanger += 0.6f;
            if (noFarLanes)
                scoreChanger += 0.6f;
            if (noNearLanes)
                scoreChanger += 0.3f;

            scoreMultiplier = 1 + scoreChanger;
        }

        textMultiplier.text = "x" + scoreMultiplier;

        /*
		if (scoreMultiplier < 1) 
		{
			Color newColor = textMultiplier.color;
			newColor = new Vector4 (187, 212, 255, 1);
			textMultiplier.color = newColor;
		}
		if (scoreMultiplier >= 1 && scoreMultiplier < 1.25f) 
		{
			Color newColor = textMultiplier.color;
			newColor = new Vector4 (158, 212, 255, 1);
			textMultiplier.color = newColor;
		}
		if (scoreMultiplier >= 1.25f && scoreMultiplier < 1.5f) 
		{
			Color newColor = textMultiplier.color;
			newColor = new Vector4 (235, 212, 255, 1);
			textMultiplier.color = newColor;
		}
		if (scoreMultiplier >= 1.5f) 
		{
			Color newColor = textMultiplier.color;
			//newColor = new Vector4 (155, 250, 255, 1);
			ColorUtility.TryParseHtmlString ("9BFAFF", out newColor);
			textMultiplier.color = newColor;
		}
		*/

        textMultiplier.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.1f);
        yield return new WaitForSeconds(0.1f);
        textMultiplier.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
    }

    IEnumerator ToolTipOn(Sprite spr)
    {
        yield return new WaitForSeconds(0.1f);
        if (canDisplayTooltip)
        {
            ttImage.sprite = spr;
            ttImage.DOFade(1, 0.5f);
        }
    }
    IEnumerator ToolTipOff()
    {
        canDisplayTooltip = false;
        ttImage.DOFade(0, 0.5f);
        canDisplayTooltip = true;
        yield return null;
    }


    IEnumerator LoadAsynchronously(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = Mathf.Lerp(loadingSlider.value, progress, 0.1f);

            yield return null;
        }
    }

    void HoverOpacity(Image image, bool hover)
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


    public void newBackButton(CanvasGroup canvas)
    {
        canvas.DOFade(0, 0.3f);
        EventSystem.current.SetSelectedGameObject(buttonSelectionPlay.gameObject);
        avatarMenu.enabled = true;

        StartCoroutine(ReturnAnim());
    }

    void OnDestroy()
    {
        SaveSettings();
    }

    void SaveSettings()
    {
        PlayerPrefs.SetInt("peaceful", peaceful ? 1 : 0);
        PlayerPrefs.SetInt("normal", normal ? 1 : 0);
        PlayerPrefs.SetInt("flawless", flawless ? 1 : 0);
        PlayerPrefs.SetInt("speed75", speed75 ? 1 : 0);
        PlayerPrefs.SetInt("speed100", speed100 ? 1 : 0);
        PlayerPrefs.SetInt("speed125", speed125 ? 1 : 0);
        PlayerPrefs.SetInt("speed150", speed150 ? 1 : 0);
        PlayerPrefs.SetInt("noFarLanes", noFarLanes ? 1 : 0);
        PlayerPrefs.SetInt("noNearLanes", noNearLanes ? 1 : 0);
    }

    void LoadSettings()
    {
        if (!PlayerPrefs.HasKey("peaceful"))
            return;

        togglePeaceful.isOn = PlayerPrefs.GetInt("peaceful") == 1;
        toggleNormal.isOn = PlayerPrefs.GetInt("normal") == 1;
        toggleFlawless.isOn = PlayerPrefs.GetInt("flawless") == 1;

        Peaceful();
        Normal();
        Flawless();

        toggleSpeed75.isOn = PlayerPrefs.GetInt("speed75") == 1;
        toggleSpeed100.isOn = PlayerPrefs.GetInt("speed100") == 1;
        toggleSpeed125.isOn = PlayerPrefs.GetInt("speed125") == 1;
        toggleSpeed150.isOn = PlayerPrefs.GetInt("speed150") == 1;

        Speed75();
        Speed100();
        Speed125();
        Speed150();

        toggleNoFarLanes.isOn = PlayerPrefs.GetInt("noFarLanes") == 1;
        toggleNoNearLanes.isOn = PlayerPrefs.GetInt("noNearLanes") == 1;

        NoFarLanes();
        NoNearLanes();
    }
}
