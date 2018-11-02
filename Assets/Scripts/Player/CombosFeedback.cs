using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CombosFeedback : MonoBehaviour {
    /*
	[Header("Renderers")]
	public Renderer CubeLeft;
	public Renderer CubeRight;

	[Header("Precision")]
	public Material M_Origin;
	public Material M_mError0;
	public Material M_mError1;
	public Material M_mError2;
	public Material M_mError3;
    */

    [HideInInspector]
    public float originCombo;
    [HideInInspector]
    public float combo;

    public ParticleSystem Perfect;

    [Header("Timing feedback")]
    public Transform timingFeedbackSprite;
    public SpriteRenderer timingFeedbackSpriteRenderer;
    public Vector3 tooSlowPosition;
    public Vector3 tooFastPosition;

    /*
	[Header("Combo")]
	public ParticleSystem particle1;
	public ParticleSystem particle2;
	public ParticleSystem particle3;

    [Header("Score+")]
    public GameObject scorePopPrefab;
    */

    //int comboIndex = 0;

    public SpriteRenderer perfectSprite;

    void start()
    {
        Perfect.Stop();
    }

    public void FeedbackPrecision ()
	{
        StartCoroutine(PefectInput());
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(PefectInput());
        }
    }

    public void FeedbackCombo (float actualCombo)
	{
		combo = actualCombo;
        /*
		if (actualCombo <= originCombo) 
		{
			particle1.Stop ();
			particle2.Stop ();
			particle3.Stop ();
			comboIndex = 0;
		}

		if (actualCombo >= originCombo * 1 && actualCombo <= originCombo * 10) 
		{
			particle1.Play ();
			comboIndex = 1;
		}
		if (actualCombo >= originCombo * 20 && actualCombo <= originCombo * 50) 
		{
			particle1.Stop ();
			particle2.Play ();
			comboIndex = 2;
		}
		if (actualCombo >= originCombo * 50) 
		{
			particle2.Stop ();
			particle3.Play ();
			comboIndex = 3;
		}
        */
	}


	public IEnumerator PefectInput()
    {
        perfectSprite.DOKill();
        perfectSprite.DOFade(1f, 0.15f).OnComplete(() =>
         {
             perfectSprite.DOFade(0f, 0.15f);
         });

        Perfect.Clear();
        Perfect.Play();
        yield return null;
    }

    public void ScorePop(float score)
    {
		/*
		GameObject scorePop = Instantiate(scorePopPrefab, this.transform);
		scorePop.transform.localPosition = transform.up;
        scorePop.GetComponent<TextMesh>().text = "" + score;

		if (score > 100) 
		{
			Color color = new Color (0, 255, 0);
			scorePop.GetComponent<TextMesh> ().color = color;
		}

			
		
		int random = Random.Range (0, 2);
		if (random == 0)
		{
			scorePop.transform.position = new Vector3(Random.Range(-4,-2), transform.position.y, transform.position.z + 4);
		} 
		else 
		{
			scorePop.transform.position = new Vector3(Random.Range(2, 4), transform.position.y, transform.position.z + 4);
		}
		*/
    }

	public void FeedBackOnTime (float PositionOnTime)
	{  
        //Too fast
		if (PositionOnTime < 0)
		{
            timingFeedbackSprite.localPosition = tooFastPosition;
            timingFeedbackSprite.localEulerAngles = new Vector3(10, 0, 0);
        }
        //Too slow
        else if (PositionOnTime > 0)
        {
            timingFeedbackSprite.localPosition = tooSlowPosition;
            timingFeedbackSprite.localEulerAngles = new Vector3(10, 0, 180);
        }

        StartCoroutine(FeedbackFade());
    }

    IEnumerator FeedbackFade()
    {
        timingFeedbackSpriteRenderer.DOKill();
        timingFeedbackSpriteRenderer.DOFade(0.85f, 0.15f);
        yield return new WaitForSeconds(0.3f);
        timingFeedbackSpriteRenderer.DOFade(0, 0.25f);
    }
}
