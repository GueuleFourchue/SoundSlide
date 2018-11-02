using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LaunchMenuAnim : MonoBehaviour {

    public Image menuGlobalBlackFade;
    public AudioSource menuMusic;

    private void Start()
    {
        StartCoroutine(MenuLaunch());
    }

    public IEnumerator MenuLaunch()
    {
        menuMusic.DOFade(0.8f, 1f);
        menuGlobalBlackFade.DOFade(0, 1.5f);
        yield return new WaitForSeconds(1.5f);
        menuGlobalBlackFade.gameObject.SetActive(false);
    }
}
