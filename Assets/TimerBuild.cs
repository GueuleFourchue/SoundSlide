using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;

public class TimerBuild : MonoBehaviour
{
    public int Day;
    public int Month;
    public int Year;

    void Awake()
    {
        CheckTime();
    }
    public void CheckTime()
    {
        try
        {
            var myHttpRequest = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
            var reponse = myHttpRequest.GetResponse();
            string dateSt = reponse.Headers["date"];

            var d2 = DateTime.Parse(dateSt).GetDateTimeFormats()[0];
            var dateSplit = d2.Split('/');
            Debug.Log("mois = " + dateSplit[0]);
            Debug.Log("jour = " + dateSplit[1]);
            Debug.Log("année = " + dateSplit[2]);

            int jour = int.Parse(dateSplit[1]);
            int mois = int.Parse(dateSplit[0]);
            int annee = int.Parse(dateSplit[2]);

            if (annee <= Year && mois <= Month && jour <= Day)
            {
                Debug.Log("ici");
            }
            else
            {
                Debug.Log("Quit");
                Application.Quit();
            }

        }
        catch
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}
