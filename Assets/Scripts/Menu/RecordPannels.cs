using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPannels : MonoBehaviour {

    public TextMesh textEgypt;
    public TextMesh textBrazil;
    public TextMesh textChina;
    public TextMesh textIndia;

    void Start ()
    {
        textEgypt.text = PlayerPrefs.GetFloat("BestScore" + "LEVEL_EGYPT", 0).ToString("F0");
        textBrazil.text = PlayerPrefs.GetFloat("BestScore" + "LEVEL_BRAZIL", 0).ToString("F0");
        textChina.text = PlayerPrefs.GetFloat("BestScore" + "LEVEL_CHINA", 0).ToString("F0");
        textIndia.text = PlayerPrefs.GetFloat("BestScore" + "LEVEL_INDIA", 0).ToString("F0");
    }
}
