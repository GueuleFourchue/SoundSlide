using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleToMenu : MonoBehaviour {

    public GameObject container;
    public GameObject Menu;
    public Image titleGlobalBlackFade;
    public LaunchMenuAnim launchMenu;
    public AudioSource music_01;
    public AudioSource music_02;

    public bool canPressAnyKey;
    string sceneToLoad;

    private void Start()
    {
        CheckIfFirstPlay();
        StartCoroutine(StartDelay());
    }

    void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(TitleToMenuTransition());
        }

        if (Input.anyKeyDown && canPressAnyKey)
        {
            StartCoroutine(TitleToMenuTransition());
        }
    }

    IEnumerator TitleToMenuTransition()
    {
        music_01.DOFade(0, 1.5f);
        music_02.DOFade(0, 1.5f);

        titleGlobalBlackFade.DOFade(1, 1.5f);
        yield return new WaitForSeconds(1.5f);

        /*
        Menu.SetActive(true);
        launchMenu.StartCoroutine("MenuLaunch");
        this.gameObject.SetActive(false);
        */

        SceneManager.LoadScene(sceneToLoad);
    }

    void CheckIfFirstPlay()
    {
        if (PlayerPrefs.HasKey("HasPlayed"))
        {
            sceneToLoad = "MAIN_MENU";
        }
        else
        {
            PlayerPrefs.SetString("HasPlayed", "HasPlayed");
            sceneToLoad = "TUTO";
        }
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(0.5f);
        container.SetActive(true);
    }
}
