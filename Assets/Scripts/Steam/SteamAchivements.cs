using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.SceneManagement;

public class SteamAchivements : MonoBehaviour {

    public static SteamAchivements instance = null;

    private bool unlockTest = false;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void UnlockSteamAchivement(string ID)
    {
        TestSteamAchivement(ID);
        if (!unlockTest)
        {
            SteamUserStats.SetAchievement(ID);
            SteamUserStats.StoreStats();
        }
    }

    public void ReceiveInfoLevel(string nameLevel, bool normal, bool flawless, bool speed125, bool speed150, bool NoNear, bool NoFar)
    {
        string numAchivement = "00";

        if (nameLevel == "LEVEL_EGYPT")
        {
            if (normal)
            {
                numAchivement = "00"; UnlockSteamAchivement("achivement_" + numAchivement);
                if (speed125)
                {
                    numAchivement = "02"; UnlockSteamAchivement("achivement_" + numAchivement);
                    if (speed150) { numAchivement = "03"; UnlockSteamAchivement("achivement_" + numAchivement); }
                    if (NoNear) { numAchivement = "04"; UnlockSteamAchivement("achivement_" + numAchivement); }
                    if (NoFar) { numAchivement = "05"; UnlockSteamAchivement("achivement_" + numAchivement); }
                }
                else if (flawless) { numAchivement = "01"; UnlockSteamAchivement("achivement_" + numAchivement); }
            }
        }
        else if (nameLevel == "LEVEL_BRAZIL")
        {
            if (normal)
            {
                numAchivement = "06"; UnlockSteamAchivement("achivement_" + numAchivement);
                if (speed125) { numAchivement = "08"; UnlockSteamAchivement("achivement_" + numAchivement); }
                if (speed150) { numAchivement = "09"; UnlockSteamAchivement("achivement_" + numAchivement); }
                if (NoNear) { numAchivement = "10"; UnlockSteamAchivement("achivement_" + numAchivement); }
                if (NoFar) { numAchivement = "11"; UnlockSteamAchivement("achivement_" + numAchivement); }
            }
            else if (flawless) { numAchivement = "07"; UnlockSteamAchivement("achivement_" + numAchivement); }

        }
        else if (nameLevel == "LEVEL_INDIA")
        {
            if (normal)
            {
                numAchivement = "12"; UnlockSteamAchivement("achivement_" + numAchivement);
                if (speed125) { numAchivement = "14"; UnlockSteamAchivement("achivement_" + numAchivement); }
                if (speed150) { numAchivement = "15"; UnlockSteamAchivement("achivement_" + numAchivement); }
                if (NoNear) { numAchivement = "16"; UnlockSteamAchivement("achivement_" + numAchivement); }
                if (NoFar) { numAchivement = "17"; UnlockSteamAchivement("achivement_" + numAchivement); }
            }
            else if (flawless) { numAchivement = "13"; UnlockSteamAchivement("achivement_" + numAchivement); }

        }
        else if (nameLevel == "LEVEL_CHINA")
        {
            if (normal)
            {
                numAchivement = "18"; UnlockSteamAchivement("achivement_" + numAchivement);
                if (speed125) { numAchivement = "20"; UnlockSteamAchivement("achivement_" + numAchivement); }
                if (speed150) { numAchivement = "21"; UnlockSteamAchivement("achivement_" + numAchivement); }
                if (NoNear) { numAchivement = "22"; UnlockSteamAchivement("achivement_" + numAchivement); }
                if (NoFar) { numAchivement = "23"; UnlockSteamAchivement("achivement_" + numAchivement); }
            }
            else if (flawless) { numAchivement = "19"; UnlockSteamAchivement("achivement_" + numAchivement); }

        }
        ///Complete Full Hard Level
        else if (flawless && speed150 && NoNear && NoFar)
        {
            UnlockSteamAchivement("achivement_31");
        }

            CompleteAllLevel();
            CompleteAllNoNearLanes();
            CompleteAllNoFarLanes();
            CompleteAll125();
            CompleteAll150();
    }

    ///Complete all level in normal mode
    void CompleteAllLevel()
    {
        bool successLvlEgypt = true;
        bool successLvlBrazil = true;
        bool successLvlIndia = true;
        bool successLvlChina = true;
        SteamUserStats.GetAchievement("achivement_00", out successLvlEgypt);
        SteamUserStats.GetAchievement("achivement_06", out successLvlBrazil);
        SteamUserStats.GetAchievement("achivement_12", out successLvlIndia);
        SteamUserStats.GetAchievement("achivement_18", out successLvlChina);
      
        if(successLvlEgypt && successLvlBrazil && successLvlIndia && successLvlChina)
        {
            UnlockSteamAchivement("achivement_26");
        } 
    }

    ///Complete all level in no near mode
    void CompleteAllNoNearLanes()
    {
        bool successLvlEgypt = true;
        bool successLvlBrazil = true;
        bool successLvlIndia = true;
        bool successLvlChina = true;
        SteamUserStats.GetAchievement("achivement_04", out successLvlEgypt);
        SteamUserStats.GetAchievement("achivement_10", out successLvlBrazil);
        SteamUserStats.GetAchievement("achivement_16", out successLvlIndia);
        SteamUserStats.GetAchievement("achivement_22", out successLvlChina);

        if (successLvlEgypt && successLvlBrazil && successLvlIndia && successLvlChina)
        {
            UnlockSteamAchivement("achivement_27");
        }
    }

    ///Complete all level in no far mode
    void CompleteAllNoFarLanes()
    {
        bool successLvlEgypt = true;
        bool successLvlBrazil = true;
        bool successLvlIndia = true;
        bool successLvlChina = true;
        SteamUserStats.GetAchievement("achivement_05", out successLvlEgypt);
        SteamUserStats.GetAchievement("achivement_11", out successLvlBrazil);
        SteamUserStats.GetAchievement("achivement_17", out successLvlIndia);
        SteamUserStats.GetAchievement("achivement_23", out successLvlChina);

        if (successLvlEgypt && successLvlBrazil && successLvlIndia && successLvlChina)
        {
            UnlockSteamAchivement("achivement_28");
        }
    }

    ///Complete all level in 125 mode
    void CompleteAll125()
    {
        bool successLvlEgypt = true;
        bool successLvlBrazil = true;
        bool successLvlIndia = true;
        bool successLvlChina = true;
        SteamUserStats.GetAchievement("achivement_02", out successLvlEgypt);
        SteamUserStats.GetAchievement("achivement_08", out successLvlBrazil);
        SteamUserStats.GetAchievement("achivement_14", out successLvlIndia);
        SteamUserStats.GetAchievement("achivement_20", out successLvlChina);

        if (successLvlEgypt && successLvlBrazil && successLvlIndia && successLvlChina)
        {
            UnlockSteamAchivement("achivement_29");
        }
    }

    ///Complete all level in 150 mode
    void CompleteAll150()
    {
        bool successLvlEgypt = true;
        bool successLvlBrazil = true;
        bool successLvlIndia = true;
        bool successLvlChina = true;
        SteamUserStats.GetAchievement("achivement_03", out successLvlEgypt);
        SteamUserStats.GetAchievement("achivement_09", out successLvlBrazil);
        SteamUserStats.GetAchievement("achivement_15", out successLvlIndia);
        SteamUserStats.GetAchievement("achivement_21", out successLvlChina);

        if (successLvlEgypt && successLvlBrazil && successLvlIndia && successLvlChina)
        {
            UnlockSteamAchivement("achivement_30");
        }
    }

    void TestSteamAchivement(string ID)
    {
        SteamUserStats.GetAchievement(ID, out unlockTest);
    }
}
