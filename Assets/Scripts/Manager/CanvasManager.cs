using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CanvasManager : MonoBehaviour
{

    private ManagerMoveLanes mnLanes;
    public ManagerMovePlayer mmPlayer;
    private Text startGame;

    public Text playText;
    public Text bestScoreText;
    public Text scoreText;
	public Text comboText;
	public Text comboTextBG;
    public Image playImage;
    public Image bestScoreImage;
    public Image scoreImage;
	public Image comboImage;
    public EndLevel endLevel;

	public int actualCombo = 0;
    private int bestcombo = 0;

    public bool playLevelTextactive = false;

	private Scene scene;

    void Start()
    {
		scene = SceneManager.GetActiveScene();
        bestScoreText.text = "BEST SCORE: " + PlayerPrefs.GetFloat("BestScore"+scene.name, 0).ToString("F0");
    }

	public void UpgradeScoring(float scorePlayer)
	{
		scoreText.text = "SCORE: " + scorePlayer.ToString("0");

		scoreText.transform.DOScale (new Vector3 (1.1f, 1.1f, 1), 0.1f).OnComplete (() => {
			scoreText.transform.DOScale (new Vector3 (1f, 1f, 1), 0.2f).SetEase (Ease.OutBack);
		});
	}

    public void InfoCombo(int nbInput,int nbInputPerfect){
        
        float percentPerfectCombo = ((float)nbInputPerfect / (float)nbInput * 100f);

        if (PlayerPrefs.GetFloat("BestPercentPerfectCombo" + scene.name, 0) < percentPerfectCombo)
        {
            PlayerPrefs.SetFloat("BestPercentPerfectCombo" + scene.name, percentPerfectCombo);
        }

        endLevel.PlayerStatsPerfect(percentPerfectCombo);
        endLevel.PlayerStatsCombo(bestcombo);
    }

    public void NumberOfPlayerDie(int nbDie){
        endLevel.PlayerStatsDeaths(nbDie);
    }

    public void SaveBestScoreAndCanvas(float scorePlayer)
    {
        if (PlayerPrefs.GetFloat("BestScore" + scene.name, 0) < scorePlayer)
        {
            PlayerPrefs.SetFloat("BestScore" + scene.name, scorePlayer);
            endLevel.newBestScore();
        }
    }


	public void UpgradeCombo()
	{
		comboText.text = "" + actualCombo.ToString("0");

		comboText.transform.DOScale (new Vector3 (1.1f, 1.1f, 1), 0.1f).OnComplete (() => {
			comboText.transform.DOScale (new Vector3 (1f, 1f, 1), 0.2f).SetEase (Ease.OutBack);
		});

        if(actualCombo > bestcombo){
            bestcombo = actualCombo;
        }
	}

	public void UpgradeScoringCheckPoint(float scorePlayer){
		scoreText.text = "SCORE: " + scorePlayer.ToString("0");
	}
		
    public void StartToGoOn()
    {
        playLevelTextactive = true;
    }

    public void StartToGoOff()
    {
        playLevelTextactive = false;

        playText.gameObject.transform.DOScale(new Vector3(1, 0, 0), 0.1f);
        bestScoreText.gameObject.transform.DOScale(new Vector3(1, 0, 0), 0.1f);
        scoreText.gameObject.transform.DOScale(new Vector3(1, 1, 1), 0.1f);
		comboText.gameObject.transform.DOScale(new Vector3(1, 1, 1), 0.1f);
		comboTextBG.gameObject.transform.DOScale(new Vector3(1, 1, 1), 0.1f);


        playImage.gameObject.transform.DOScale(new Vector3(1, 0, 0), 0.1f);
        bestScoreImage.gameObject.transform.DOScale(new Vector3(1, 0, 0), 0.1f);
        scoreImage.gameObject.transform.DOScale(new Vector3(-1, -1, -1), 0.1f);
		comboImage.gameObject.transform.DOScale(new Vector3(-1, -1, -1), 0.1f);
    }

    public void ScalePlay()
    {
		comboTextBG.gameObject.transform.DOScale(new Vector3(1, 0, 0), 0.1f);
		comboText.gameObject.transform.DOScale(new Vector3(1, 0, 0), 0.1f);
		comboImage.gameObject.transform.DOScale(new Vector3(1, 0, 0), 0.1f);

		scoreText.gameObject.transform.DOScale(new Vector3(1, 0, 0), 0.1f);
		scoreImage.gameObject.transform.DOScale(new Vector3(1, 0, 0), 0.1f);

		bestScoreText.text = "BEST SCORE: " + Mathf.Round(PlayerPrefs.GetFloat("BestScore"+scene.name, 0)).ToString("F0");

        playText.gameObject.transform.DOScale(new Vector3(1, 1, 1), 0.1f);
        bestScoreText.gameObject.transform.DOScale(new Vector3(1, 1, 1), 0.1f);

        playImage.gameObject.transform.DOScale(new Vector3(1, 1, 1), 0.1f);
        bestScoreImage.gameObject.transform.DOScale(new Vector3(1, 1, 1), 0.1f);
    }
}