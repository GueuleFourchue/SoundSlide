using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChangeSkyboxColor : MonoBehaviour {

    //
    public float colorLerp;
    public Color creditsColor;
    public Color optionsColor;
    public Color egyptColor;
    public Color brazilColor;
    public Color indiaColor;
    public Color chinaColor;

    Material skyMat;
    bool isLerping;
    Color newColor;
    float currentTime;

    private void Start()
    {
        skyMat = RenderSettings.skybox;
        if (PlayerPrefs.GetInt("PlayedLevelIndex") < 0)
            skyMat.SetColor("_Color2", egyptColor);
        else
            ChangeColor(PlayerPrefs.GetInt("PlayedLevelIndex"));
    }

    private void Update()
    {
        if (isLerping)
        {
            if (currentTime <= colorLerp)
            {
                currentTime += Time.deltaTime;
                skyMat.SetColor("_Color2", Color.Lerp(skyMat.GetColor("_Color2"), newColor, currentTime / colorLerp));
            }
            else 
            {
                isLerping = !isLerping;
            }
        }
    }

    public void ChangeColor(int index)
    {
        if (index == -2)
            newColor = creditsColor;
        if (index == -1)
            newColor = optionsColor;
        if (index == 0)
            newColor = egyptColor;
        if (index == 1)
            newColor = brazilColor;
        if (index == 2)
            newColor = indiaColor;
        if (index == 3)
            newColor = chinaColor;

        currentTime = 0;
        isLerping = true;
    }
}
