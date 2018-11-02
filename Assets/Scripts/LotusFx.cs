using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusFx : MonoBehaviour {

    public ParticleSystem Particule;
    private bool IsOk = false;
    private float CanStart;
    public float Timer;

	// Use this for initialization
	void Start () {

        CanStart = Random.Range(1.0f, 3.0f);
	}
	
	// Update is called once per frame
	void Update () {

        CanStart = CanStart + Time.deltaTime;

        if (CanStart >= Timer)
        {
            IsOk = true;
        }

        if(IsOk == true)
        {
            StartCoroutine(ParticuleStart());
            IsOk = false;
            CanStart = 0;
        }
    }

    public IEnumerator ParticuleStart ()
    {
        Particule.Play();
        yield return new WaitForSeconds(2.1f);
        Particule.Stop();
    }
}
