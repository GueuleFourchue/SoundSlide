using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AvatarMenu : MonoBehaviour {

	public GameOptionsMenu gameOptionsMenu;
    public CanvasGroup escape;

    [Header("ChinaAnims")]
    public Animator cAnim1;
    public Animator cAnim2;
    public Animator cAnim3;
    public Animator cAnim4;

    [Header("Button")]
	public KeyCode buttonLeft1;
	public KeyCode buttonLeft2;
	public KeyCode buttonRight1;
	public KeyCode buttonRight2;

	public Transform leftLane;
	public Transform rightLane;
	public Transform actualLane;

    public Animator anim;
    public AudioSource audioSource;

	bool canMove = true;

    public Button buttonSelectionPlay;

    [Header("SkyBox")]
    public ChangeSkyboxColor changeSkyboxColor;
    int laneIndex = 0;

    [Header("Start Positions")]
    public Vector3[] startPositions;
    public Vector3[] startRotations;

    private void Start()
    {
        if (PlayerPrefs.HasKey("PlayedLevelIndex"))
        {
            int index = PlayerPrefs.GetInt("PlayedLevelIndex");
            transform.position = startPositions[index];
            transform.eulerAngles = startRotations[index];
            if (PlayerPrefs.HasKey("SceneToLoad"))
                gameOptionsMenu.sceneToLoad = PlayerPrefs.GetString("SceneToLoad");
        }
    }

    void Update () 
	{
		if (((Input.GetKeyDown(InputsManager.IM.left1) || Input.GetKeyDown(InputsManager.IM.left2)) && leftLane != null && canMove))
		{
			if (gameOptionsMenu.levelOptionsUIenabled == false) 
			{
                laneIndex -= 1;
                anim.Play ("MoveLeft");
				MoveLane(leftLane);
                EventSystem.current.SetSelectedGameObject(buttonSelectionPlay.gameObject);
            }
		}
		if (((Input.GetKeyDown(InputsManager.IM.right1) || Input.GetKeyDown(InputsManager.IM.right2)) && rightLane != null && canMove))
		{
            if (gameOptionsMenu.levelOptionsUIenabled == false) 
			{
                laneIndex += 1;
                anim.Play ("MoveRight");
                MoveLane (rightLane);
                EventSystem.current.SetSelectedGameObject(buttonSelectionPlay.gameObject);
            }
		}

		if (Input.GetKeyDown (KeyCode.Space)) 
		{
            //
		}
	}

	void MoveLane(Transform lane)
	{
        audioSource.Play();
        escape.DOKill();
        escape.DOFade(0, 0.1f);

        AnimEnviro(false, actualLane);

        actualLane = lane;
        string scene = actualLane.GetComponent<LaneSceneHolder>().attachedScene;
        gameOptionsMenu.sceneToLoad = scene;
        if (scene != "OPTIONS" && scene != "CREDITS")
            PlayerPrefs.SetString("SceneToLoad", scene);
        PlayerPrefs.SetInt("PlayedLevelIndex", actualLane.GetComponent<LaneSceneHolder>().levelIndex);

        canMove = false;

		transform.DOKill ();

		transform.parent = lane;
		transform.DOLocalMoveX(0, 0.5f);
		transform.DOLocalMoveY(0.4f, 0.5f);
		transform.transform.DOLocalMoveZ (-11, 0.5f).OnComplete(() =>
        {
            escape.DOFade(1, 0.6f);
        });
		transform.DORotate(new Vector3(0, lane.transform.eulerAngles.y, 0), 0.5f);

		canMove = true;

        AnimEnviro(true, actualLane);
        changeSkyboxColor.ChangeColor(laneIndex);

    }


    void AnimEnviro(bool activated, Transform lane)
    {
        if (activated)
        {
            if (lane.parent.GetComponent<Animator>())
            {
                lane.parent.GetComponent<Animator>().enabled = true;
            }
            else if (lane.parent.name == "China")
            {
                cAnim1.enabled = true;
                cAnim2.enabled = true;
                cAnim3.enabled = true;
                cAnim4.enabled = true;
            }
            else if (lane.parent.GetChild(0).GetComponentInChildren<Animator>())
            {
                lane.parent.GetChild(0).GetComponentInChildren<Animator>().enabled = true;
            }
        }
        else
        {
            if (lane.parent.GetComponent<Animator>())
            {
                lane.parent.GetComponent<Animator>().enabled = false;
            }
            else if (lane.parent.name == "China")
            {
                cAnim1.enabled = false;
                cAnim2.enabled = false;
                cAnim3.enabled = false;
                cAnim4.enabled = false;
            }
            else if (lane.parent.GetChild(0).GetComponentInChildren<Animator>())
            {
                lane.parent.GetChild(0).GetComponentInChildren<Animator>().enabled = false;
            }
        }
    }
}
