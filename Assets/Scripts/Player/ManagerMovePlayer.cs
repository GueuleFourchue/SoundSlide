using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ManagerMovePlayer : MonoBehaviour
{

    [Header("Carac du niveau")]
    public LevelUp caracLevel;

    [Header("Debut de lane")]
    public float decalageLane = 3;

    [Header("CheckPoint")]
    public lineTree[] checkpointLane;

    [Header("Respawn au checkpoint")]
    public bool activeCheckpoint = false;
    public lineTree respawnCheckpoint;

    [Header("Score")]
    public ManagerScore mScore;

    [Header("Manager")]
    public Game_Manager gmanager;
    public CanvasManager cvManager;
    public EndLevel endLevel;

    [Header("Anim")]
    public Animator anim;

    [Header("Speed FX")]
    public ParticleSystem speedFX;

    [Header("Player")]
    public Material mSpeed;
    public Transform avatarMesh;
    public Camera laneCam;
    public SfxMenuInGame sfxScript;

    [Header("Audio Offset")]
    public float offsetMaster = 0;
    public float offset75;
    public float offset100;
    public float offset125;
    public float offset150;
    //public float unit75;
    //public float unit100;
    //public float unit125;
    //public float unit150;
    public float speed75;
    public float speed100;
    public float speed125;
    public float speed150;

    private Coroutine co;
    private Coroutine codead;
    private AudioSource au;
    private ManagerMoveLanes mmLanes;
    private lineTree savelANE;

    private Vector3 scaleplayer;

    private float MoveFinal;
    public float timeAudio = 0;
    private float originFOV;

    private bool flawless = false;
    private bool restartFast = false;

    private int nbPlayerDie = 0;

    private GameObject infoLevel;
    private info_managerscript levelInfomanager;

    private float offsetAudio;
    private float speedAudio = 1;
    private float particlesAnimDuration = 0.5f;

    void Start()
    {
        mmLanes = transform.GetComponent<ManagerMoveLanes>();
        au = gameObject.GetComponent<AudioSource>();
        savelANE = mmLanes.actulane;
        originFOV = Camera.main.fieldOfView;
        scaleplayer = transform.localScale;
        TriCheckPoint();
        savelANE = checkpointLane[0];

        mSpeed.SetFloat("_Scale", 0f);

        if (speedFX != null)
            speedFX.Stop();

        UnitByTimeOffset();
    }

    void Update()
    {
        if ((Input.GetKey(KeyCode.Space) && mmLanes.test() && !restartFast))
        {
            restartFast = true;
        }
    }

    public void TriCheckPoint()
    {
        bool Tryokc = false;
        while (!Tryokc)
        {
            Tryokc = true;
            for (int i = 0; i < checkpointLane.Length - 1; i++)
            {
                if (checkpointLane[i].gameObject.transform.position.z > checkpointLane[i + 1].gameObject.transform.position.z)
                {
                    lineTree c2 = checkpointLane[i + 1];
                    checkpointLane[i + 1] = checkpointLane[i];
                    checkpointLane[i] = c2;
                    Tryokc = false;
                }
            }
        }
    }

    public void ChangeModeFlawless()
    {
        flawless = true;
    }

    public void LastLaneCheckP()
    {
        if (flawless)
        {
            savelANE = checkpointLane[0];
        }
        else
        {

            savelANE = checkpointLane[0];

            if (activeCheckpoint && respawnCheckpoint != null)
            {
                savelANE = respawnCheckpoint;

            }
            else
            {
                for (int i = 0; i < checkpointLane.Length; i++)
                {
                    if (transform.position.z + decalageLane >= checkpointLane[i].transform.position.z)
                    {
                        savelANE = checkpointLane[i];
                    }
                }
            }
        }

    }

    public IEnumerator MoveDuringMusic(GameObject playerMove, Vector3 posFinal, float seconds)
    {
        float elapsedTime = 0;
        float startingPos = playerMove.transform.position.z;

        while (elapsedTime < seconds)
        {

            Vector3 newPosition = transform.position;
            newPosition.z = Mathf.Lerp(startingPos, posFinal.z, (elapsedTime / seconds));

            playerMove.transform.position = newPosition;

            elapsedTime += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        playerMove.transform.position = new Vector3(transform.position.x, transform.position.y, posFinal.z);

        mScore.SaveBestScore();
        cvManager.NumberOfPlayerDie(nbPlayerDie);
        endLevel.Activate();
        transform.DOMoveZ(transform.position.z + 100, 7);

        yield return null;
    }

    public IEnumerator EffectDead(float seconds2, Transform lane)
    {
        mmLanes.ResetLives();

        gmanager.EffectPlay();
        transform.parent = lane;
        transform.localPosition = new Vector3(0, 0.4f, transform.localPosition.z);
        transform.localEulerAngles = new Vector3(0, 0, 0);
        avatarMesh.localEulerAngles = new Vector3(0, 0, 0);
        transform.parent = null;
        transform.localScale = scaleplayer;

        float elapsedTime2 = 0;

        Vector3 pos;
        if (savelANE.transform.position.z - decalageLane < 0)
        {
            pos = new Vector3(transform.position.x, transform.position.y, 0);
        }
        else
        {
            pos = new Vector3(transform.position.x, transform.position.y, savelANE.transform.position.z - decalageLane);
        }


        while (elapsedTime2 < seconds2)
        {
            if (restartFast) { break; }

            Vector3 newPosition = transform.position;
            newPosition.z = Mathf.Lerp(transform.position.z, pos.z, (elapsedTime2 / seconds2));

            transform.position = newPosition;

            elapsedTime2 += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, pos.z);

        mmLanes.actulane = savelANE;
        mmLanes.ResetMatCheckPoint();

        gmanager.DeadMoment();
        yield return null;
    }

    public void PlayGame()
    {
        restartFast = false;
        float value = (caracLevel.nbTime * caracLevel.unitByT) / ((caracLevel.unitByT * (caracLevel.bpmValue / 60)));
        float tempsparcourut = savelANE.transform.position.z / (caracLevel.unitByT * (caracLevel.bpmValue / 60));
        float tempsrestant = value - tempsparcourut;

        MoveFinal = caracLevel.unitByT * (caracLevel.bpmValue / 60) * value;

        if (transform.gameObject.activeSelf)
            co = StartCoroutine(MoveDuringMusic(transform.gameObject, new Vector3(transform.position.x, transform.position.y, MoveFinal), tempsrestant));

        mmLanes.SwitchDeath();

        mSpeed.SetFloat("_Speed", 0.5f);
        mSpeed.DOFloat(0.05f, "_Scale", particlesAnimDuration);

        if (speedFX != null)
            speedFX.Play();
    }

    public void PlaySound()
    {
        timeAudio = 0;
        //timeAudio = transform.position.z / (caracLevel.unitByT * (caracLevel.bpmValue / 60));

        //To Fix the offset problem in the audio after checkpoint
        //timeAudio = transform.position.z / (10 * (caracLevel.bpmValue / 60));
        timeAudio = (transform.position.z / (10 * (caracLevel.bpmValue / 60))) - (offsetAudio + offsetMaster);

        if (timeAudio < 0)
            timeAudio = 0;

        au.time = timeAudio;
        au.pitch = 1;

        au.volume = 0;
        au.Play();
        au.DOFade(1, 1f);
    }

    public IEnumerator PlaySoundRoutine()
    {
        timeAudio = 0;
        //timeAudio = transform.position.z / (caracLevel.unitByT * (caracLevel.bpmValue / 60));

        //To Fix the offset problem in the audio after checkpoint
        //timeAudio = transform.position.z / (10 * (caracLevel.bpmValue / 60));

        if (transform.position.z == 0)
        {
            yield return new WaitForSecondsRealtime(offsetAudio);
        }

        timeAudio = (transform.position.z / (10 * (caracLevel.bpmValue / 60))) - (offsetAudio + offsetMaster);

        if (timeAudio < 0)
            timeAudio = 0;

        au.time = timeAudio;

        //au.pitch = 1;
        au.pitch = speedAudio;

        au.volume = 0;
        au.Play();
        au.DOFade(1, 1f);
    }

    public void DeathFonction()
    {
        nbPlayerDie++;
        mScore.ReturnScoreDead();

        cvManager.actualCombo = 0;
        cvManager.UpgradeCombo();

        Camera.main.DOFieldOfView(originFOV + 40, 1);
        laneCam.DOFieldOfView(originFOV + 40, 1);

        StopCoroutine(co);

        mSpeed.SetFloat("_Speed", 0.1f);
        au.DOFade(0, 0.5f);
        Camera.main.DOShakePosition(0.2f, 0.7f, 30, 50);
        laneCam.DOShakePosition(0.2f, 0.7f, 30, 50);

        mSpeed.DOFloat(0f, "_Scale", particlesAnimDuration);

        if (speedFX != null)
            speedFX.Stop();

        au.DOPitch(0.95f, 0.5f).OnComplete(() =>
            {
                au.Stop();
                au.pitch = 1;
                // mSpeed.SetFloat("_Scale", 0.01f);

                Camera.main.DOFieldOfView(originFOV, 1);
                laneCam.DOFieldOfView(originFOV, 1);

                LastLaneCheckP();

                StartCoroutine(EffectDead(1f, savelANE.transform));
            });

        anim.Play("Death");
    }

    void UnitByTimeOffset()
    {
        infoLevel = GameObject.Find("InfoLevel");
        if (infoLevel != null)
        {
            levelInfomanager = infoLevel.GetComponent<info_managerscript>();

            if (levelInfomanager.info_speed75)
            {
                //caracLevel.unitByT = unit75;
                offsetAudio = offset75;
                speedAudio = speed75;
            }
            if (levelInfomanager.info_speed100)
            {
                //caracLevel.unitByT = unit100;
                offsetAudio = offset100;
                speedAudio = speed100;
            }
            if (levelInfomanager.info_speed125)
            {
                //caracLevel.unitByT = unit125;
                offsetAudio = offset125;
                speedAudio = speed125;
            }
            if (levelInfomanager.info_speed150)
            {
                //caracLevel.unitByT = unit150;
                offsetAudio = offset150;
                speedAudio = speed150;
            }
        }
    }
}

[System.Serializable]
public struct LevelUp
{
    public float bpmValue;
    public float unitByT;
    public float nbTime;

}
