using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutoManager : MonoBehaviour {

    KeyCode buttonLeft1;
    KeyCode buttonLeft2;
    KeyCode buttonRight1;
    KeyCode buttonRight2;

    public float zPositions_Text_01;
    public float zPositions_Text_02;
    public float zPositions_Text_03;
    public float zPositions_Text_04;
    public float zPositions_Text_05;
    public float zPositions_End;

    public CanvasGroup text_01;
    public CanvasGroup text_02;
    public CanvasGroup text_03;
    public CanvasGroup text_05;

    bool text01Displayed;
    bool text02Displayed;
    bool text03Displayed;
    bool text04Displayed;
    bool text05Displayed;
    bool endLevel;

    bool text01_Over;
    bool text02_Over;
    bool text03_Over;
    bool text04_Over;
    bool text05_Over;

    public Transform player;
    public CanvasGroup spacebarCheckpoint;

    [Header("Audio")]
    public AudioSource PopUp;
    public AudioSource Submit;

    [Header("Lanes")]
    public Transform lane_01;
    public Transform lane_02;
    public Transform lane_03;
    public Transform lane_04;

    [Header("BeginAnim")]
    public Image globalBlackFade;
    public CanvasGroup playCanvasGroup;

    [Header("End")]
    public CanvasGroup globalCanvasGroup;
    public GameObject begin;
    public CanvasGroup text1Canvas;
    public CanvasGroup text2Canvas;
    public CanvasGroup spaceBarCanvas;
    public CanvasGroup globalFade2Canvas;
    public CanvasGroup tutoTextCanvas;

    CanvasGroup displayedCanvas;
    float originPlayerSpeed;
    bool endTutoText;
    bool end;

    private void Start()
    {
        buttonLeft1 = InputsManager.IM.left1;
        buttonLeft2 = InputsManager.IM.left2;
        buttonRight1 = InputsManager.IM.right1;
        buttonRight2 = InputsManager.IM.right2;

        originPlayerSpeed = player.GetComponent<TutoPlayerMove>().forwardSpeed;
        StartCoroutine(StartAnim());
    }

    void Update ()
    {
        CheckPosition();
        CheckDisplayedText();

        if (endTutoText && Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(EndTutoText());

        if (end && Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(ChangeScene());
    }

    void CheckPosition()
    {
       if (!text01_Over)
       {
            if (player.position.z > zPositions_Text_01 && !text01Displayed)
                DisplayText(text_01, ref text01Displayed);
       }
       if (!text02_Over)
       {
            if (player.position.z > zPositions_Text_02 && !text02Displayed)
                DisplayText(text_02, ref text02Displayed);
       }
       if (!text03_Over)
       {
            if (player.position.z > zPositions_Text_03 && !text03Displayed)
                DisplayText(text_03, ref text03Displayed);
       }
        if (!text04_Over)
        {
            if (player.position.z > zPositions_Text_04 && !text04Displayed)
            {
                text04Displayed = true;
                player.GetComponent<TutoPlayerMove>().forwardSpeed = 0f;
            }
        }
        if (!text05_Over)
        {
            if (player.position.z > zPositions_Text_05 && !text05Displayed)
            {
                StartCoroutine(CheckpointSpacebar());
                DisplayText(text_05, ref text05Displayed);
            }
                
        }
        if (player.position.z > zPositions_End && !endLevel)
        {
            endLevel = true;
            StartCoroutine(EndLevel());
        }
    }

    void DisplayText(CanvasGroup canvasGroup, ref bool textBool)
    {
        textBool = true;

        PlaySFX(PopUp);

        //MoveAnim
        Vector3 originPos = canvasGroup.transform.GetComponent<RectTransform>().position;
        canvasGroup.transform.GetComponent<RectTransform>().position = new Vector3(originPos.x, originPos.y - 0.25f, originPos.z);
        canvasGroup.transform.GetComponent<RectTransform>().DOMoveY(originPos.y, 1f);

        canvasGroup.DOFade(1, 0.5f);
        player.GetComponent<TutoPlayerMove>().forwardSpeed = 0f;

        displayedCanvas = canvasGroup;
    }

    void CheckDisplayedText()
    {
        if (text01Displayed)
        {
            if (Input.GetKeyDown(buttonRight1) || Input.GetKeyDown(buttonRight2))
            {
                displayedCanvas.DOKill();
                displayedCanvas.DOFade(0, 0.6f);
                displayedCanvas = null;

                player.GetComponent<TutoPlayerMove>().forwardSpeed = originPlayerSpeed;

                text01Displayed = false;
                text01_Over = true;
                
                //MoveLane
                MoveLane(lane_01);
                PlaySFX(Submit);
            }
        }
        if (text02Displayed)
        {
            if (Input.GetKeyDown(buttonLeft1) || Input.GetKeyDown(buttonLeft2))
            {
                displayedCanvas.DOKill();
                displayedCanvas.DOFade(0, 0.6f);
                displayedCanvas = null;

                player.GetComponent<TutoPlayerMove>().forwardSpeed = originPlayerSpeed;

                text02Displayed = false;
                text02_Over = true;

                //MoveLane
                MoveLane(lane_02);
                PlaySFX(Submit);
            }
        }
        if (text03Displayed)
        {
            if (Input.GetKeyDown(buttonRight1) || Input.GetKeyDown(buttonRight2))
            {
                displayedCanvas.DOKill();
                displayedCanvas.DOFade(0, 0.6f);
                displayedCanvas = null;

                player.GetComponent<TutoPlayerMove>().forwardSpeed = originPlayerSpeed;

                text03Displayed = false;
                text03_Over = true;

                //MoveLane
                MoveLane(lane_03);
                PlaySFX(Submit);
            }
        }
        if (text04Displayed)
        {
            if (Input.GetKeyDown(buttonRight1) || Input.GetKeyDown(buttonRight2))
            {
                displayedCanvas.DOKill();
                displayedCanvas.DOFade(0, 0.6f);
                displayedCanvas = null;

                player.GetComponent<TutoPlayerMove>().forwardSpeed = originPlayerSpeed;

                text04Displayed = false;
                text04_Over = true;

                //MoveLane
                MoveLane(lane_04);
            }
        }
        if (text05Displayed)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                displayedCanvas.DOKill();
                displayedCanvas.DOFade(0, 0.6f);
                displayedCanvas = null;

                player.GetComponent<TutoPlayerMove>().forwardSpeed = originPlayerSpeed;

                text05Displayed = false;
                text05_Over = true;

                PlaySFX(Submit);
            }
        }
    }


    void MoveLane(Transform lane)
    {
        player.parent = lane;
        player.DOLocalMove(new Vector3(0, 0.3f, player.localPosition.z), 0.2f);
        player.DORotateQuaternion(lane.transform.rotation, 0.3f);
    }

    IEnumerator StartAnim()
    {
        globalBlackFade.DOFade(0, 1.5f);
        yield return new WaitForSeconds(1f);
        playCanvasGroup.DOFade(1, 1f);
    }

    IEnumerator CheckpointSpacebar()
    {
        yield return new WaitForSeconds(1);
        spacebarCheckpoint.DOFade(1, 0.8f);
    }

    public void PlaySFX(AudioSource audio)
    {
        audio.Play();
    }

    IEnumerator EndLevel()
    {
        begin.SetActive(false);
        globalCanvasGroup.alpha = 1;
        globalBlackFade.DOFade(1, 2.5f);

        yield return new WaitForSeconds(1.75f);

        /*
        tutoTextCanvas.DOFade(1, 1);
        yield return new WaitForSeconds(3);
        spaceBarCanvas.DOFade(1, 1);
        endTutoText = true;
        */

        //Below
        spaceBarCanvas.DOFade(0, 0.2f);
        tutoTextCanvas.DOFade(0, 1);
        yield return new WaitForSeconds(1);
        text1Canvas.DOFade(1, 1);
        yield return new WaitForSeconds(1.25f);
        text2Canvas.DOFade(1, 1);
        yield return new WaitForSeconds(1);
        end = true;
        spaceBarCanvas.DOFade(1, 1);
    }
    IEnumerator EndTutoText()
    {
        yield return null;
    }
        IEnumerator ChangeScene()
    {
        globalFade2Canvas.DOFade(1, 1.5f);

        AsyncOperation operation = SceneManager.LoadSceneAsync("MAIN_MENU");
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
