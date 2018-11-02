using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TutoMenuAnims : MonoBehaviour
{
    public CanvasGroup globalFade2Canvas;
    public GameObject tuto;
    public GameObject askTuto;

    [Header("Images")]
    public Image noImage;
    public Image yesImage;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip sfxMenuMouseover;
    public AudioClip sfxMenuSubmit;

    private void Start()
    {
        globalFade2Canvas.DOFade(0, 1.5f);
    }

    public void YesHover()
    {
        HoverOpacity(yesImage, true);
        PlaySFX(sfxMenuMouseover, 0.5f);
    }
    public void YesHoverEnd()
    {
        HoverOpacity(yesImage, false);
    }
    public void NoHover()
    {
        HoverOpacity(noImage, true);
        PlaySFX(sfxMenuMouseover, 0.5f);
    }
    public void NoHoverEnd()
    {
        HoverOpacity(noImage, false);
    }

    void HoverOpacity(Image image, bool hover)
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
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
    }

    public void LaunchMainMenu()
    {
        globalFade2Canvas.DOFade(1, 1.5f);
        StartCoroutine(LoadMainMenu());
        PlaySFX(sfxMenuSubmit, 0.5f);
    }
    public void LaunchTuto()
    {
        StartCoroutine(LoadTuto());
        PlaySFX(sfxMenuSubmit, 0.5f);
    }

    IEnumerator LoadMainMenu()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("MAIN_MENU");
        while (!operation.isDone)
        {
            yield return null;
        }
    }

    IEnumerator LoadTuto()
    {
        globalFade2Canvas.DOFade(1, 1.5f);
        yield return new WaitForSeconds(1.5f);
        tuto.SetActive(true);
        askTuto.SetActive(false);
    }
}
