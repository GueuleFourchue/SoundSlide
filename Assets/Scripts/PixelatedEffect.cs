using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PixelatedEffect : MonoBehaviour {

    public ImageFilter imageFilter;
    public Material pixelMat;
    public float animBaseSpeed;
    public float animUpSpeed;

    float scale;
    
    // Use this for initialization
	void Start ()
    {
        pixelMat.SetFloat("_Scale", 0);
    }

    private void Update()
    {
        scale = pixelMat.GetFloat("_Scale");
        if (scale <= 60)
        {
            pixelMat.SetFloat("_Scale", scale += Time.deltaTime * animBaseSpeed);
        }
        else if (scale > 60 && scale < 1500)
        {
            pixelMat.SetFloat("_Scale", scale += Time.deltaTime * animUpSpeed);
        }
        else if (scale > 1500)
        {
            imageFilter.enabled = false;
            this.enabled = false;
        }
    }
}
