using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackgroundMove : MonoBehaviour
{

    public float X;
    public float Y;
    public float Z;

    public float TravelTime;

    private Vector3 GoTo;
    private Vector3 myPosIni;


    void Start()
    {
        myPosIni = transform.localPosition;
    }

    public void Movement()
    {
		
		GoTo.Set(transform.position.x + X, transform.position.y + Y, transform.position.z + Z);
		transform.DOLocalMove(GoTo, TravelTime);
    }

    public void PosMovement()
    {
        transform.DOKill();
        transform.localPosition = myPosIni;
    }
}
