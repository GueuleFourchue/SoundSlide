using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityStandardAssets;
using UnityEngine.UI;

public class Avatar_Cine : MonoBehaviour
{

    public float speed = 2;
    public List<Transform> lanesList = new List<Transform>();
    public float endPosZ;
    public UnityStandardAssets.ImageEffects.DepthOfField DoF;
    public Image blackFade;
    public GameObject avatarMesh;
    public UnityStandardAssets.ImageEffects.MotionBlur motionBlur;
    public Transform logo;
    public Animator logoAnim;
    public TitleToMenu titleToMenu;
    public AudioSource Music_TitleScreenEnd;

    [Header("Title")]
    public Text[] lettersTitle;
    public RectTransform soundTextHolder;
    public RectTransform slideTextHolder;
    public Transform anyKey;
    public Vector3 soundTextFinalPosition;
    public Vector3 slideTextFinalPosition;
    float timeOfTravel = 1.6f;
    float currentTime = 0;
    float normalizedValue;
    Vector3 soundTextPosition;
    Vector3 slideTextPosition;




    bool endActive;
    bool moveTitle;

    private void Awake()
    {
        StartCoroutine(StartAnim());
    }

    void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * speed);

        if (transform.position.z > endPosZ && !endActive)
        {
            StartCoroutine(End());
        }
    }

    public IEnumerator MoveLane(Transform lane)
    {
        if (!lanesList.Contains(lane))
        {
            yield return new WaitForSeconds(0.05f);
            transform.parent = lane;

            transform.DOKill();
            transform.DOLocalMoveX(0, 0.2f);
            transform.DOLocalMoveY(0.4f, 0.2f);
            transform.DORotate(new Vector3(0, 0, lane.transform.eulerAngles.z), 0.2f);

            lanesList.Add(lane);
        }
    }

    IEnumerator StartAnim()
    {
        blackFade.DOFade(0, 3f);
        yield return new WaitForSeconds(2.8f);
        DOTween.To(() => DoF.aperture, x => DoF.aperture = x, 1, 3f);
        DOTween.To(() => motionBlur.blurAmount, x => motionBlur.blurAmount = x, 0.5f, 3f);

    }

    IEnumerator End()
    {
        endActive = true;
        blackFade.transform.parent.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        blackFade.DOFade(1, 0.05f);
        avatarMesh.SetActive(false);

        yield return new WaitForSeconds(1f);
        logo.gameObject.SetActive(true);
        logo.GetComponent<RectTransform>().DOLocalMoveY(3.35f, 1f).SetEase(Ease.OutBack);

        yield return new WaitForSeconds(0.3f);
        Music_TitleScreenEnd.Play();
        yield return new WaitForSeconds(0.7f);



        foreach (Text text in lettersTitle)
        {
            text.DOFade(1, 2f);
            text.transform.DOShakeScale(0.25f, 0.6f, 50, 90);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.6f);

        logoAnim.enabled = true;

        //soundTextHolder.DOMove(new Vector3(675, 550, soundTextHolder.position.z), 1.6f);
        //slideTextHolder.DOMove(new Vector3(1275, 550, slideTextHolder.position.z), 1.6f);

        //yield return new WaitForSeconds(1.6f);

        anyKey.GetComponent<Text>().DOFade(0.3f, 0.5f);
        titleToMenu.canPressAnyKey = true;
        yield return new WaitForSeconds(0.4f);
        anyKey.GetComponent<Animator>().enabled = true;
    }

    IEnumerator LerpObject()
    {
        while (currentTime <= timeOfTravel)
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / timeOfTravel; // we normalize our time 

            soundTextHolder.anchoredPosition = Vector3.Lerp(soundTextPosition, soundTextFinalPosition, normalizedValue);
            slideTextHolder.anchoredPosition = Vector3.Lerp(slideTextPosition, slideTextFinalPosition, normalizedValue);
            yield return null;
        }
    }

}
