using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{

    public GameOptionsMenu gameOptionsMenu;

    public AvatarMenu avatarMenu;

    public Transform mainMenuButtons;
    public Transform musicBars;

    public Skybox skybox;

    public GameObject LevelSelectionMenu;
    public GameObject mainMenu;

    public string sceneToLoad;

    public SpriteRenderer blackFade;
    public GameObject creditsObj;
    public GameObject exitObj;
    public GameObject optionsObj;

    [Header("Buttons")]
    public Button buttonPlay;
    public Button buttonOptions;
    public Button buttonCredits;
    public Button buttonExit;

    public Image creditsImage;

    bool creditsEnabled;
    bool optionsEnabled;
    bool exitEnabled;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip sfxMenuSubmit;
    public AudioClip sfxMenuBack;
    public AudioClip sfxMenuMouseover;

    [Header("NewNavigation")]
    public Button buttonExitNo;
    public Button buttonBackCredits;
    public Dropdown resoDropdown;
    public Button buttonPlayLevel;

    public void Play()
    {
        PlaySFX(sfxMenuSubmit, 0.5f);
        StartCoroutine(MenuAnim(LevelSelectionMenu, mainMenu, false));
        //Camera.main.clearFlags.Equals(skybox); 
        //
        EventSystem.current.SetSelectedGameObject(buttonPlayLevel.gameObject);
    }

    public void ReturnToMain(GameObject In, GameObject Out)
    {
        StartCoroutine(MenuAnim(In, Out, true));
    }

    public void Options()
    {
        PlaySFX(sfxMenuSubmit, 0.5f);
        MenuAwayAnim(buttonOptions, -295.6f, -275f);
        StartCoroutine(OptionsAnim());
    }
    public void Credits()
    {
        PlaySFX(sfxMenuSubmit, 0.5f);
        MenuAwayAnim(buttonCredits, -295.6f, -275f);
        creditsObj.SetActive(true);
        creditsObj.transform.DOScaleX(1, 0.3f);
        StartCoroutine(CreditsFade());

        creditsEnabled = true;
        //
        EventSystem.current.SetSelectedGameObject(buttonBackCredits.gameObject);
    }

    public void Exit()
    {
        PlaySFX(sfxMenuSubmit, 0.5f);
        MenuAwayAnim(buttonExit, -371f, -275f);
        StartCoroutine(ExitAnim());
    }

    IEnumerator ExitAnim()
    {
        MenuAwayAnim(buttonCredits, -295.6f, -275f);
        yield return new WaitForSeconds(0.7f);
        exitObj.SetActive(true);
        exitObj.transform.DOScaleX(1, 0.3f);
        exitEnabled = true;

        //
        EventSystem.current.SetSelectedGameObject(buttonExitNo.gameObject);
    }

    public void MenuAwayAnim(Button button, float butt, float buttons)
    {
        mainMenuButtons.DOKill();
        button.transform.DOKill();
        button.transform.DOLocalMoveX(butt, 0.05f);
        mainMenuButtons.DOLocalMoveX(buttons, 1f).SetEase(Ease.InOutBack);
    }



    public IEnumerator MenuAnim(GameObject menuIn, GameObject menuOut, bool boolean)
    {
        if (!boolean)
            MenuAwayAnim(buttonPlay, -295.6f, -275f);

        yield return new WaitForSeconds(0.5f);

        for (float t = 0.0f; t < 1.1f; t += Time.deltaTime * 6)
        {
            Color newColor = blackFade.color;
            newColor.a = Mathf.Lerp(0, 1, t);
            blackFade.color = newColor;

            yield return null;
        }
        menuOut.SetActive(false);

        Camera.main.orthographic = boolean;

        menuIn.SetActive(true);

        for (float t = 0.0f; t < 1.1f; t += Time.deltaTime * 6)
        {
            Color newColor = blackFade.color;
            newColor.a = Mathf.Lerp(1, 0, t);
            blackFade.color = newColor;

            yield return null;
        }

        if (boolean)
            mainMenuButtons.DOLocalMoveX(0, 0.5f).SetEase(Ease.InOutBack);
    }

    IEnumerator CreditsFade()
    {
        yield return new WaitForSeconds(0.5f);

        for (float t = 0.0f; t < 1.1f; t += Time.deltaTime * 6)
        {
            Color newColor = creditsImage.color;
            newColor.a = Mathf.Lerp(0, 1, t);
            creditsImage.color = newColor;

            yield return null;
        }

    }

    public void CreditsBack()
    {
        PlaySFX(sfxMenuBack, 1f);
        StartCoroutine(CreditsBackAnim());
        creditsEnabled = false;
    }

    IEnumerator CreditsBackAnim()
    {
        for (float t = 0.0f; t < 1.1f; t += Time.deltaTime * 6)
        {
            Color newColor = creditsImage.color;
            newColor.a = Mathf.Lerp(1, 0, t);
            creditsImage.color = newColor;

            yield return null;
        }
        creditsObj.transform.DOScaleX(0, 0.3f);
        creditsObj.SetActive(false);
        //
        yield return new WaitForSeconds(0.2f);
        mainMenuButtons.DOLocalMoveX(0, 0.5f).SetEase(Ease.InOutBack);

        yield return new WaitForSeconds(0.4f);
        //
        EventSystem.current.SetSelectedGameObject(buttonCredits.gameObject);
    }


    public void ExitBack()
    {
        PlaySFX(sfxMenuBack, 1f);
        StartCoroutine(ExitBackAnim());
        gameOptionsMenu.isOnMainMenu = true;
    }

    IEnumerator ExitBackAnim()
    {
        exitObj.GetComponent<CanvasGroup>().DOFade(0, 0.3f).OnComplete(() =>
        {
            exitObj.SetActive(false);
        });
        avatarMenu.enabled = true;
        EventSystem.current.SetSelectedGameObject(buttonPlay.gameObject);
        yield return null;
    }


    public void QuitGame()
    {
        Application.Quit();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (creditsEnabled == true)
            {
                CreditsBack();
            }
            else if (exitEnabled == true)
            {
                StartCoroutine(ExitBackAnim());
                exitEnabled = false;
            }
            else if (optionsEnabled == true)
            {
                StartCoroutine(OptionsBackAnim());
                optionsEnabled = false;
            }
        }
    }

    public void OptionsBack()
    {
        PlaySFX(sfxMenuBack, 1f);
        StartCoroutine(OptionsBackAnim());
    }

    IEnumerator OptionsAnim()
    {
        MenuAwayAnim(buttonCredits, -295.6f, -275f);
        yield return new WaitForSeconds(0.7f);
        optionsObj.SetActive(true);
        optionsObj.transform.DOScaleX(1, 0.3f);
        optionsEnabled = true;

        yield return new WaitForSeconds(0.2f);
        //
        EventSystem.current.SetSelectedGameObject(resoDropdown.gameObject);
    }

    IEnumerator OptionsBackAnim()
    {
        optionsObj.transform.DOScaleX(0, 0.3f);
        yield return new WaitForSeconds(0.3f);
        optionsObj.SetActive(false);
        //
        mainMenuButtons.DOLocalMoveX(0, 0.5f).SetEase(Ease.InOutBack);

        yield return new WaitForSeconds(0.4f);
        //
        EventSystem.current.SetSelectedGameObject(buttonOptions.gameObject);
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        audioSource.volume = volume;
        audioSource.clip = clip;
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
    }

}
