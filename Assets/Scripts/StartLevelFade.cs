using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartLevelFade : MonoBehaviour
{

    public CanvasGroup blackFade;

    void Start()
    {
        blackFade.alpha = 1;

        blackFade.DOFade(0, 1.5f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
    }
}
