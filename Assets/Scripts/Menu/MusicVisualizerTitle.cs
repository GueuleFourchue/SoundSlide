using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MusicVisualizerTitle : MonoBehaviour {

	public GameObject cube01;
	public GameObject cube02;
	public GameObject cube03;
	public GameObject cube04;
	public GameObject cube05;
	public GameObject cube06;
	public GameObject cube07;
	public GameObject cube08;
	public GameObject cube09;
	public GameObject cube10;


	public float juice = 20f;
	public float speed = 0.5f;

	public float[] spec;

	public float specMag01;
	public float specMag02;
	public float specMag03;
	public float specMag04;
	public float specMag05;
	public float specMag06;
	public float specMag07;
	public float specMag08;
	public float specMag09;
	public float specMag10;



	void FixedUpdate () {
		spec = AudioListener.GetOutputData (64,0);  // this gives much  better values.

		specMag01 = spec[2] + spec[6];
		specMag02 = spec[6] + spec[12];
		specMag03 = spec[12] + spec[18];
		specMag04 = spec[18] + spec[24];
		specMag05 = spec[24] + spec[30];
		specMag06 = spec[30] + spec[36];
		specMag07 = spec[36] + spec[42];
		specMag08 = spec[42] + spec[48];
		specMag09 = spec[48] + spec[54];
		specMag10 = spec[54] + spec[60];


		transform.DOKill ();

		Scale (cube01, specMag01);
		Scale (cube02, specMag02);
		Scale (cube03, specMag03);
		Scale (cube04, specMag04);
		Scale (cube05, specMag05);
		Scale (cube06, specMag06);
		Scale (cube07, specMag07);
		Scale (cube08, specMag08);
		Scale (cube09, specMag09);
		Scale (cube10, specMag10);



	}

	void Scale(GameObject cube, float specMag)
	{
		if (specMag>0.05f)
			cube.gameObject.transform.DOScaleY (specMag*juice,speed);
	}
}﻿

