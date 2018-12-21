using UnityEngine;
using System.Collections;
using DG.Tweening;

public class InputsManager : MonoBehaviour
{
    public static InputsManager IM;

    public KeyCode left1 { get; set; }
    public KeyCode left2 { get; set; }
    public KeyCode right1 { get; set; }
    public KeyCode right2 { get; set; }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (IM == null)
        {
            DontDestroyOnLoad(gameObject);
            IM = this;
        }
        else if (IM != this)
        {
            Destroy(gameObject);
        }

        left1 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey1", "A"));
        left2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey2", "LeftArrow"));
        right1 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey1", "D"));
        right2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey2", "RightArrow"));
    }






    public void DisableCanvasGroup(CanvasGroup canvasGroup, float duration, System.Action callback = null, bool disableGameobject = false)
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        canvasGroup.DOFade(0, duration).OnComplete(() =>
        {
            if (callback != null)
                callback();

            if (disableGameobject)
                canvasGroup.gameObject.SetActive(false);
        });
    }

    public void DisableCanvasGroup(CanvasGroup canvasGroup, bool disableGameobject = false)
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        if (disableGameobject)
            canvasGroup.gameObject.SetActive(false);
    }

    public void EnableCanvasGroup(CanvasGroup canvasGroup, float duration, System.Action callback = null, bool enableGameobject = false)
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        canvasGroup.DOFade(1, duration).OnComplete(() =>
        {
            if (callback != null)
                callback();

            if (enableGameobject)
                canvasGroup.gameObject.SetActive(true);
        });
    }

    public void EnableCanvasGroup(CanvasGroup canvasGroup, bool enableGameobject = false)
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        if (enableGameobject)
            canvasGroup.gameObject.SetActive(true);
    }
}