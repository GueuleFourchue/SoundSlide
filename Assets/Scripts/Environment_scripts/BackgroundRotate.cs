using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackgroundRotate : MonoBehaviour {

	public float levelDuration;

	public int rotationX;
	public int rotationY;
	public int rotationZ;

	private Vector3 rot;

	// Use this for initialization
	void Start () 
	{
		rot = transform.localEulerAngles;
	}

	public void RotateBackground()
	{
		transform.DOLocalRotate (new Vector3 (rotationX, rotationY, rotationZ), levelDuration, RotateMode.FastBeyond360);
	}

	public void PosRotateBackground()
	{
        transform.DOKill();
        transform.localEulerAngles = rot;
	}
}
