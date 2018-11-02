using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;

public class ManagerMoveLanes : MonoBehaviour
{
    
    [Header("UI")]
    public Transform pauseContainer;
    public Text pauseScoreText;
    public Button buttonPauseResume;
    
    [Header("Audio")]
    public AudioClip Mus_75;
    public AudioClip Mus_100;
    public AudioClip Mus_125;
    public AudioClip Mus_150;


    [Header("Button")]
    public KeyCode buttonLeft1;
    public KeyCode buttonLeft2;
    public KeyCode buttonRight1;
    public KeyCode buttonRight2;

    [Header("Button Pause")]
    public KeyCode buttonPause;

    [Header("Start Lane")]
    public lineTree actulane;

    [Header("Lane Shader Value")]
    public float base_Near;
    public float base_Far;
    public float base_Smooth;
    public float noNear_Near;
    public float noNear_Far;
    public float noNear_Smooth;
    public float noFar_Near;
    public float noFar_Far;
    public float noFar_Smooth;


    [Header("Material")]
    public Material laneOnMaterial;
    public Material laneDeadMaterial;
    public Material originMaterial;
    public Material originFastMaterial;

    [Header("Anims")]
    public Animator anim;


    [Header("CheckPoint")]
    public GameObject parentCheckPoint;

    [Header("FeedbackCombo")]
    public CombosFeedback combosFeedback;

    /*
    [Header("LD Offsets")]
    public Transform LD;
    public float speed75Offset;
    public float speed100Offset;
    public float speed125Offset;
    public float speed150Offset;
    */

    /// <summary>
    //Private value;
    /// </summary>
    /// 
    private float topValue;
    private float topTime;
    private float downTime;
    private float scoreMultiplier = 1;

    private bool pauseGame = false;
    private bool dead = true;

    private ManagerMovePlayer mvPlayer;
    private ManagerScore mScore;
    private info_managerscript levelInfomanager;
    private AudioSource au;

    private GameObject infoLevel;
    private lineTree deadLane;

    private Material lastLaneMat;
    private Material lastDeadLaneMaterial;

    [Header("Lives")]
    public int livesCount;
    public float invuDuration;
    public Transform sphere;
    public AudioMixer audioMixer;
    public float lowPassValue;
    public PostProcessVolume PPVolume;
    public float lowSaturation;

    [HideInInspector]
    public int lives;
    bool invu = false;
    ColorGrading colorGradingLayer = null;
    float originSaturation;

    void Start()
    {
        scoreMultiplier = 1;
        GetInputLevel();
        lives = livesCount;

        PPVolume.profile.TryGetSettings(out colorGradingLayer);
        originSaturation = colorGradingLayer.saturation.value;
    }

    void Awake()
    {
        au = gameObject.GetComponent<AudioSource>();
        mvPlayer = transform.GetComponent<ManagerMovePlayer>();
        mScore = transform.GetComponent<ManagerScore>();

        lastLaneMat = actulane.GetComponent<Renderer>().material;
        actulane.GetComponent<Renderer>().material = laneOnMaterial;
        ChangeOptionLevel();

    }

    public void GetInputLevel(){
        buttonLeft1 = InputsManager.IM.left1;
        buttonLeft2 = InputsManager.IM.left2;
        buttonRight1 = InputsManager.IM.right1;
        buttonRight2 = InputsManager.IM.right2;
    }

    public void ChangeOptionLevel()
    {
        infoLevel = GameObject.Find("InfoLevel");
        if (infoLevel != null)
        {
            levelInfomanager = infoLevel.GetComponent<info_managerscript>();

            float scoreChanger = 0;

            if (levelInfomanager.info_normal)
            { 
                scoreChanger += 0;

            }
            if (levelInfomanager.info_flawless)
            { 
                scoreChanger += 0;
                mvPlayer.ChangeModeFlawless();
                parentCheckPoint.SetActive(false);

            }
            if (levelInfomanager.info_speed75)
            { 
                scoreChanger += -0.3f;
                mvPlayer.caracLevel.bpmValue *= 0.75f;
                au.clip = Mus_75;
                //LD.position = new Vector3(LD.position.x, LD.position.y, speed75Offset);
            }
            if (levelInfomanager.info_speed100)
            { 
                scoreChanger += 0;
                mvPlayer.caracLevel.bpmValue *= 1f;
                au.clip = Mus_100;
                //LD.position = new Vector3(LD.position.x, LD.position.y, speed100Offset);
            }
            if (levelInfomanager.info_speed125)
            { 
                scoreChanger += 0.3f;
                mvPlayer.caracLevel.bpmValue *= 1.25f;
                au.clip = Mus_125;
                //LD.position = new Vector3(LD.position.x, LD.position.y, speed125Offset);
            }
            if (levelInfomanager.info_speed150)
            { 
                scoreChanger += 0.6f;
                mvPlayer.caracLevel.bpmValue *= 1.5f;
                au.clip = Mus_150;
                //LD.position = new Vector3(LD.position.x, LD.position.y, speed150Offset);
            }
            if (levelInfomanager.info_noNearLanes && !levelInfomanager.info_noFarLanes)
            {
                scoreChanger += 0.6f;
                originMaterial.SetFloat("_Near", noNear_Near);
                originMaterial.SetFloat("_Far", noNear_Far);
                originMaterial.SetFloat("_Smooth", noNear_Smooth);
                originFastMaterial.SetFloat("_Near", noNear_Near);
                originFastMaterial.SetFloat("_Far", noNear_Far);
                originFastMaterial.SetFloat("_Smooth", noNear_Smooth);

            }
            else if (levelInfomanager.info_noFarLanes && !levelInfomanager.info_noNearLanes)
            {
                scoreChanger += 0.3f;
                originMaterial.SetFloat("_Near", noFar_Near);
                originMaterial.SetFloat("_Far", noFar_Far);
                originMaterial.SetFloat("_Smooth", noFar_Smooth);
                originFastMaterial.SetFloat("_Near", noFar_Near);
                originFastMaterial.SetFloat("_Far", noFar_Far);
                originFastMaterial.SetFloat("_Smooth", noFar_Smooth);
            }
            else if (levelInfomanager.info_noFarLanes && levelInfomanager.info_noNearLanes)
            {
                scoreChanger += 0.9f;
                originMaterial.SetFloat("_Near", noNear_Near);
                originMaterial.SetFloat("_Far", noFar_Far);
                originMaterial.SetFloat("_Smooth", noFar_Smooth);
                originFastMaterial.SetFloat("_Near", noNear_Near);
                originFastMaterial.SetFloat("_Far", noFar_Far);
                originFastMaterial.SetFloat("_Smooth", noFar_Smooth);
            }
            else
            {
                originMaterial.SetFloat("_Near", base_Near);
                originMaterial.SetFloat("_Far", base_Far);
                originMaterial.SetFloat("_Smooth", base_Smooth);
                originFastMaterial.SetFloat("_Near", base_Near);
                originFastMaterial.SetFloat("_Far", base_Far);
                originFastMaterial.SetFloat("_Smooth", base_Smooth);
            }

            scoreMultiplier = 1 + scoreChanger;

            if (levelInfomanager.info_peaceful)
            { 
                scoreMultiplier = 0;
                parentCheckPoint.SetActive(false);
            }

            mScore.startscore(scoreMultiplier);
        }
    }

    public void SwitchDeath()
    {
        dead = false;
    }

    public void SwitchDeath2()
    {
        dead = true;
    }

    public bool test()
    {
        return dead;
    }

    private void CallDeadFunction()
    {
        dead = true;
        sphere.DOScale(0.2f, 0.3f);

        if (deadLane == null)
        {
            deadLane = actulane;
            lastDeadLaneMaterial = lastLaneMat;
        }
        else if (deadLane != actulane)
        {
            deadLane.GetComponent<Renderer>().material = lastDeadLaneMaterial;
            deadLane = actulane;
            lastDeadLaneMaterial = lastLaneMat;
        }

        actulane.GetComponent<Renderer>().material = laneDeadMaterial;

        mvPlayer.DeathFonction();
    }

    public void CheckPointScore()
    {
        for (int i = 0; i < mvPlayer.checkpointLane.Length; i++)
        {
            if (actulane == mvPlayer.checkpointLane[i])
            {
                mScore.SaveScoreCheckP();
            }
        }
    }

    public void MoveLeft()
    {
        float posMax = actulane.returnMaxL();
        float posMin = actulane.returnMinL();
        if ((posMax > transform.position.z && transform.position.z > posMin))
        {
            float perfectTemps = (posMax + posMin) / 2;
            float ecartTemps = posMax - posMin;

            mScore.goingLeft = true;
            mScore.ReturnScore(perfectTemps, ecartTemps);

            actulane.GetComponent<Renderer>().material = lastLaneMat;

            actulane = actulane.ChunckLeft;
            MoveLane(actulane.transform);

            lastLaneMat = actulane.GetComponent<Renderer>().material;
            actulane.GetComponent<Renderer>().material = laneOnMaterial;


            if (levelInfomanager.info_normal)
            {
                CheckPointScore();
            }
        }

        else if (!levelInfomanager.info_peaceful)
        {
            if (!invu)
            {
                if (lives != 0)
                {
                    StartCoroutine(SetInvulnerability());
                }
                else
                {
                    transform.DOLocalMove(transform.position - transform.right, 0.15f);
                    CallDeadFunction();
                }
            }
        }
    }

    public void MoveRight()
    {

        float posMax = actulane.returnMaxR();
        float posMin = actulane.returnMinR();
        if ((posMax > transform.position.z && transform.position.z > posMin))
        {
            float perfectTemps = (posMax + posMin) / 2;
            float ecartTemps = posMax - posMin;

            mScore.goingLeft = false;
            mScore.ReturnScore(perfectTemps, ecartTemps);

            actulane.GetComponent<Renderer>().material = lastLaneMat;

            actulane = actulane.ChunckRight;
            MoveLane(actulane.transform);

            lastLaneMat = actulane.GetComponent<Renderer>().material;
            actulane.GetComponent<Renderer>().material = laneOnMaterial;

            if (levelInfomanager.info_normal)
            {
                CheckPointScore();
            }


        }
        else if (!levelInfomanager.info_peaceful)
        {
            if (!invu)
            {
                if (lives != 0)
                {
                    StartCoroutine(SetInvulnerability());
                }
                else
                {
                    transform.DOLocalMove(transform.position + transform.right, 0.15f);
                    CallDeadFunction();
                }
            }
        }
    }

    public void ResetMatCheckPoint()
    {
        lastLaneMat = actulane.GetComponent<Renderer>().material;
    }

    public void MovePeaceful(){
        
        if (actulane.ChunckLeft != null)
        {

            if (actulane.transform.localScale.z > 10)
                ChangeLaneMaterial(originMaterial);
            else
                ChangeLaneMaterial(originFastMaterial);

            MoveLane(actulane.ChunckLeft.gameObject.transform);
            actulane = actulane.ChunckLeft;

            ChangeLaneMaterial(laneOnMaterial);
        }
        else if (actulane.ChunckRight != null)
        {

            if (actulane.transform.localScale.z > 10)
                ChangeLaneMaterial(originMaterial);
            else
                ChangeLaneMaterial(originFastMaterial);

            MoveLane(actulane.ChunckRight.gameObject.transform);
            actulane = actulane.ChunckRight;

            ChangeLaneMaterial(laneOnMaterial);

        }
        else
        {
            Debug.Log("Probleme");
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(buttonPause))
        {
            Pause();
        }

        if (!dead && !pauseGame)
        {
            if (transform.position.z > actulane.returnMaxL() && levelInfomanager.info_peaceful)
            {
                MovePeaceful();
            }
            else if (transform.position.z > actulane.returnMaxL() && !levelInfomanager.info_peaceful)
            {
                if (invu)
                    MovePeaceful();
                else if (lives != 0)
                {
                    MovePeaceful();
                    StartCoroutine(SetInvulnerability());
                }
                    
                else
                {
                    actulane.GetComponent<Renderer>().material = lastLaneMat;
                    CallDeadFunction();
                }
            }

            if (Input.GetKeyDown(buttonLeft1) || Input.GetKeyDown(buttonLeft2))
            {
                anim.Play("Left",-1,0);

                if (actulane.ChunckLeft != null)
                {
                    MoveLeft();
                }
                else if (!levelInfomanager.info_peaceful)
                {
                    if (!invu)
                    {
                        if (lives != 0)
                        {
                            StartCoroutine(SetInvulnerability());
                        }
                        else
                        {
                            CallDeadFunction();
                        }
                    } 
                }
            }

            if (Input.GetKeyDown(buttonRight1) || Input.GetKeyDown(buttonRight2))
            {
                anim.Play("Right", -1, 0);

                if (actulane.ChunckRight != null)
                {
                    MoveRight();
                }
                else if (!levelInfomanager.info_peaceful)
                {
                    if (!invu)
                    {
                        if (lives != 0)
                        {
                            StartCoroutine(SetInvulnerability());
                        }
                        else
                        {
                            CallDeadFunction();
                        }
                    }
                }
            }

        }

    }

    void MoveLane(Transform lane)
    {
        transform.DOLocalMoveX(lane.localPosition.x, 0.15f);
        transform.DORotate(new Vector3(0, 0, lane.transform.eulerAngles.z), 0.15f);
        transform.DOLocalMoveY(lane.localPosition.y, 0.15f);
    }

    public void ChangeLaneMaterial(Material newMat)
    {
        actulane.GetComponent<Renderer>().material = newMat;
    }

    public void Pause()
    {
        if (!pauseGame)
        {
            Cursor.visible = true;
            pauseContainer.gameObject.SetActive(true);

            EventSystem.current.SetSelectedGameObject(buttonPauseResume.gameObject);

            pauseContainer.DOKill();
            pauseContainer.transform.localPosition = Vector3.zero;
            pauseContainer.localScale = new Vector3(1, 1, 1);
            pauseScoreText.text = "" + mScore.score.ToString("0"); ;
            pauseGame = true;
            Time.timeScale = 0;
            au.Pause();

        }
        else
        {
            Cursor.visible = false;
            pauseContainer.DOKill();
            pauseContainer.DOLocalMoveY(500, 0.3f).OnComplete(() =>
               {
                   pauseContainer.localScale = new Vector3(1, 0, 1);
                   pauseContainer.transform.localPosition = Vector3.zero;
                   pauseContainer.gameObject.SetActive(false);
               });
            pauseGame = false;
            Time.timeScale = 1;
            au.volume = 0;
            au.Play();
            au.DOFade(1, 0.5f);
        }
    }
    IEnumerator SetInvulnerability()
    {
        lives--;
        invu = true;

        //Feedbacks
        sphere.DOScale(1, 0.4f).SetEase(Ease.InBounce);
        StartCoroutine(LoseLifeEffects());

        yield return new WaitForSeconds(invuDuration);
        invu = false;
    }

    IEnumerator LoseLifeEffects()
    {
        colorGradingLayer.saturation.value = lowSaturation;

        Debug.Log(colorGradingLayer.saturation.value);

        audioMixer.SetFloat("LowPassFrequency", lowPassValue);
        audioMixer.SetFloat("FlangerWetLevel", 0f);
        float value = 0;
        float time = 0;
        while (value < 22000f)
        {
            time += Time.deltaTime;
            audioMixer.GetFloat("LowPassFrequency", out value);

            audioMixer.SetFloat("LowPassFrequency", lowPassValue + (time/invuDuration)*(22000- lowPassValue));
            audioMixer.SetFloat("FlangerWetLevel", (time / invuDuration)*-80f);
            colorGradingLayer.saturation.value = lowSaturation + ((time / invuDuration) * Mathf.Abs(originSaturation - lowSaturation));
            yield return null;
        }
        Debug.Log(colorGradingLayer.saturation.value);

    }
    public void ResetLives()
    {
        lives = livesCount;
        sphere.DOKill();
        sphere.DOScale(1.5f, 0.4f);

        audioMixer.SetFloat("LowPassFrequency", 22000f);
        audioMixer.SetFloat("FlangerWetLevel", -80f);
        colorGradingLayer.saturation.value = originSaturation;
    }
}
