using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScore : MonoBehaviour
{
	[System.Serializable]
	public class MargeError
	{
		public float margeScore;
		public float pourcentScore;
		public float comboOrNot;
	}
		
	[Header("---------------------------------------------------------------")]

	[Header("Le Score du deplacement")]
	public CanvasManager cmanager;

	[Header("Le Score du deplacement")]
	public float scoreBySwitch = 100;


	[Header("---------------------------------------------------------------")]

	[Header("Les marges en battements et pourcentage")]

	[Header("1 = combo , 0 = rien et -1 = loose combo")]

	public float comboPlayer;
	public MargeError[] merror;

	public CombosFeedback combosFeedback;
	public TextInputPrecision textInputPrecision1;
	public TextInputPrecision textInputPrecision2;


	private float point = 0;
	public float score = 0;
	private float mycombo = 0;
	private float scoreChechpoint = 0;


    private int nbInput = 0;
    private int nbPerfectInput = 0;
    private int nbInputChechpoint = 0;
    private int nbPerfectInputChechpoint = 0;

	[HideInInspector]
	public bool goingLeft;

	public void startscore(float multi)
	{
		point = scoreBySwitch * multi;
	}

	void Start()
	{
		combosFeedback.originCombo = comboPlayer;
	}

	public void CheckCombo(int pos)
	{
        
        nbInput++;
        
		if (merror[pos].comboOrNot == 1)
		{
			mycombo = mycombo + comboPlayer;
			cmanager.actualCombo += 1;
			cmanager.UpgradeCombo ();
		}
		else if (merror[pos].comboOrNot == 0)
		{
			//mycombo = mycombo + 0;
			//mycombo = mycombo;
		}
		else if (merror[pos].comboOrNot == -1)
		{
			mycombo = 0;
			cmanager.actualCombo = 0;
			cmanager.UpgradeCombo ();
		}
	}

	public void ReturnScoreDead()
	{
		score = scoreChechpoint;
        nbInput = nbInputChechpoint;
        nbPerfectInput = nbPerfectInputChechpoint;

		mycombo = 0;
		combosFeedback.FeedBackOnTime(0);
        score = Mathf.Round(score * 0.9f);
		cmanager.UpgradeScoringCheckPoint (score);
	}

    public void SaveBestScore(){
        cmanager.SaveBestScoreAndCanvas(score);
        cmanager.InfoCombo(nbInput,nbPerfectInput);
    }

	public void SaveScoreCheckP(){
		scoreChechpoint = score;
        nbInputChechpoint = nbInput;
        nbPerfectInputChechpoint = nbPerfectInput;
	}

	public void ReturnScore(float timePerfect, float ecartLane)
	{
		float playerposZ = transform.position.z;
		float erreur = Mathf.Abs(playerposZ - timePerfect);
		float difMax = ecartLane / 2;
		float pourcentage = erreur / difMax;
		float PositionOnTime = playerposZ - timePerfect;

		for (int i = 0; i < merror.Length; i++)
		{
			if (pourcentage < merror[i].margeScore)
			{
				CheckCombo(i);

                if (merror [i].pourcentScore == .5f) 
				{
					if (goingLeft)
						textInputPrecision1.TextPop (textInputPrecision1.s5);
					else
						textInputPrecision2.TextPop (textInputPrecision2.s5);
				}
				else if (merror[i].pourcentScore == 1)
				{
					if (goingLeft)
						textInputPrecision1.TextPop (textInputPrecision1.s10);
					else
						textInputPrecision2.TextPop (textInputPrecision2.s10);
				}
					
				else if (merror[i].pourcentScore == 1.5f)
				{
					if (goingLeft)
						textInputPrecision1.TextPop (textInputPrecision1.s15);
					else
						textInputPrecision2.TextPop (textInputPrecision2.s15);
				}
					
				else if (merror[i].pourcentScore == 3)
				{
                    nbPerfectInput++;

					if (goingLeft)
						textInputPrecision1.TextPop (textInputPrecision1.s30);
					else
						textInputPrecision2.TextPop (textInputPrecision2.s30);
				}	
					

				score = score + (point * (merror[i].pourcentScore + mycombo));
				cmanager.UpgradeScoring (score);

				//Feedbacks
                if (i == 0)
				    combosFeedback.FeedbackPrecision();
                else if (i >= 2)
				    combosFeedback.FeedBackOnTime(PositionOnTime);

				break;
			}
		}
	}
}
