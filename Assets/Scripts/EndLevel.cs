using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EndLevel : MonoBehaviour {

    string menu = "MAIN_MENU";
    public Transform container;
    public ManagerScore managerScore;
    public Text scoreText;
    public Text scoreAboveText;
    public CameraFollow cameraFollow;
    
	public Animator camera1Anim;
	public Animator camera2Anim;

    public AudioSource audioSource;

	GameObject infoLevel;

	public Button buttonReplay;
	public GameObject avatar;

    public Material newRecordMaterial;

	[Header("LoadingScreen")]
	public GameObject loadingScreen;
	public Slider loadingSlider;

    [Header("PlayerStats")]
    public Text deathText;
    public Text perfectInputsText;
    public Text bestComboText;

    public void Activate()
    {
        Cursor.visible = true;
        audioSource.Play();
        audioSource.DOFade(0.9f, 3f);

        cameraFollow.enabled = false;
        container.DOScaleY(1, 0.2f);
        scoreText.text = "" + managerScore.score.ToString("0");
		StartCoroutine(AnimScoreText ());
        camera1Anim.Play("AnimCameraFin");
		camera2Anim.Play("AnimCameraFin");

		
		EventSystem.current.SetSelectedGameObject (buttonReplay.gameObject);
		avatar.SetActive (false);
    }

    public void newBestScore()
    {
        scoreAboveText.text = "NEW RECORD";
        scoreAboveText.material = newRecordMaterial;
        scoreText.material = newRecordMaterial;
    }

    public void PlayerStatsPerfect(float percentage)
    {
        perfectInputsText.text = percentage.ToString("F0") + "%";
    }
    public void PlayerStatsDeaths(int deathsCount)
    {
        deathText.text = deathsCount.ToString();
    }
    public void PlayerStatsCombo(int combo)
    {
        bestComboText.text = combo.ToString() + "x";
    }

    public IEnumerator AnimScoreText()
	{
		scoreText.transform.DOKill ();
		scoreText.transform.DOScale (1.15f, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
			{
				scoreText.transform.DOScale (1, 0.5f).SetEase(Ease.Linear);
			});

		yield return new WaitForSeconds(1f);
		StartCoroutine(AnimScoreText());
	}

    public void Restart(string sceneName)
    {
        if (Time.timeScale != 1)
            Time.timeScale = 1;
        
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    public void MainMenu()
    {
        infoLevel = GameObject.Find("InfoLevel");
        Destroy(infoLevel);
        Time.timeScale = 1;

        StartCoroutine(LoadAsynchronously(menu));
    }

    IEnumerator LoadAsynchronously(string scene)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync (scene);

		loadingScreen.SetActive (true);

		while (!operation.isDone) 
		{
			float progress = Mathf.Clamp01 (operation.progress / 0.9f);
			loadingSlider.value = Mathf.Lerp(loadingSlider.value, progress, 0.1f); ;

			yield return null;
		}
	}
}
