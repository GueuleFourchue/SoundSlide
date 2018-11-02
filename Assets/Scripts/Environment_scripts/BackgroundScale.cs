using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackgroundScale : MonoBehaviour
{

    public float levelDuration;

	public float scaleX;
	public float scaleY;
	public float scaleZ;

    private Vector3 scale;

    // Use this for initialization
    void Start()
    {
        scale = transform.localScale;
    }

    public void ScaleBackground()
    {
        transform.DOScale(new Vector3(scaleX, scaleY, scaleZ), levelDuration);
    }

    public void PosScaleBackground()
    {
        transform.DOKill();
        transform.localScale = scale;
    }
}
