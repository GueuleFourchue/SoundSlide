using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.SceneManagement;

public class SteamAchivements : MonoBehaviour
{
    public static SteamAchivements instance = null;

    [Header("Achievements")]
    public bool debugMode = true;
    public List<Achievement> Achievements = new List<Achievement>();

    private bool unlockTest = false;

    // Our GameID
    private CGameID m_GameID;

    protected Callback<UserStatsReceived_t> m_UserStatsReceived;
    protected Callback<UserStatsStored_t> m_UserStatsStored;
    protected Callback<UserAchievementStored_t> m_UserAchievementStored;

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

    void OnEnable()
    {
        if (!SteamManager.Initialized)
            return;

        if (SteamManager.Initialized)
        {
            string name = SteamFriends.GetPersonaName();
            Debug.Log(name);
        }

        Achievements.Clear();

        for (int i = 0; i < System.Enum.GetNames(typeof(AchievementID)).Length; i++)
        {
            Achievements.Add(new Achievement(((AchievementID)i).ToString()));
        }

        // Cache the GameID for use in the Callbacks
        m_GameID = new CGameID(SteamUtils.GetAppID());

        m_UserStatsReceived = Callback<UserStatsReceived_t>.Create(OnUserStatsReceived);
        m_UserStatsStored = Callback<UserStatsStored_t>.Create(OnUserStatsStored);
        m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);

        StartCoroutine(GetSteamData());
    }

    #region Steam Callbacks
    IEnumerator GetSteamData()
    {
        bool bSuccess = false;

        do
        {
            bSuccess = SteamUserStats.RequestCurrentStats();
            yield return new WaitForEndOfFrame();
        }
        while (!bSuccess);
    }

    IEnumerator StoreSteamData()
    {
        bool bSuccess = false;

        do
        {
            bSuccess = SteamUserStats.StoreStats();
            yield return new WaitForEndOfFrame();
        }
        while (!bSuccess);
    }

    //-----------------------------------------------------------------------------
    // Purpose: We have stats data from Steam. It is authoritative, so update
    //			our data with those results now.
    //-----------------------------------------------------------------------------
    void OnUserStatsReceived(UserStatsReceived_t pCallback)
    {
        if (!SteamManager.Initialized)
            return;

        // we may get callbacks for other games' stats arriving, ignore them
        if ((ulong)m_GameID == pCallback.m_nGameID)
        {
            if (EResult.k_EResultOK == pCallback.m_eResult)
            {
                Debug.Log("Received stats and achievements from Steam\n");

                // load achievements
                foreach (Achievement ach in Achievements)
                {
                    bool ret = SteamUserStats.GetAchievement(ach.achievementID.ToString(), out ach.achieved);

                    if (ret)
                    {
                        ach.name = SteamUserStats.GetAchievementDisplayAttribute(ach.achievementID.ToString(), "name");
                        ach.description = SteamUserStats.GetAchievementDisplayAttribute(ach.achievementID.ToString(), "desc");
                        SteamUserStats.GetAchievement(ach.achievementID.ToString(), out ach.achieved);
                    }
                    else
                    {
                        Debug.LogWarning("SteamUserStats.GetAchievement failed for Achievement " + ach.achievementID.ToString() + "\nIs it registered in the Steam Partner site?");
                    }
                }

                StartCoroutine(StoreSteamData());

            }
            else
            {
                Debug.Log("RequestStats - failed, " + pCallback.m_eResult);
            }
        }
    }

    //-----------------------------------------------------------------------------
    // Purpose: Our stats data was stored!
    //-----------------------------------------------------------------------------
    void OnUserStatsStored(UserStatsStored_t pCallback)
    {
        // we may get callbacks for other games' stats arriving, ignore them
        if ((ulong)m_GameID == pCallback.m_nGameID)
        {
            if (EResult.k_EResultOK == pCallback.m_eResult)
            {
                Debug.Log("StoreStats - success");
            }
            else if (EResult.k_EResultInvalidParam == pCallback.m_eResult)
            {
                // One or more stats we set broke a constraint. They've been reverted,
                // and we should re-iterate the values now to keep in sync.
                Debug.Log("StoreStats - some failed to validate");
                // Fake up a callback here so that we re-load the values.
                UserStatsReceived_t callback = new UserStatsReceived_t();
                callback.m_eResult = EResult.k_EResultOK;
                callback.m_nGameID = (ulong)m_GameID;
                OnUserStatsReceived(callback);
            }
            else
            {
                Debug.Log("StoreStats - failed, " + pCallback.m_eResult);
            }
        }
    }

    //-----------------------------------------------------------------------------
    // Purpose: An achievement was stored
    //-----------------------------------------------------------------------------
    void OnAchievementStored(UserAchievementStored_t pCallback)
    {
        // We may get callbacks for other games' stats arriving, ignore them
        if ((ulong)m_GameID == pCallback.m_nGameID)
        {
            if (0 == pCallback.m_nMaxProgress)
            {
                Debug.Log("Achievement '" + pCallback.m_rgchAchievementName + "' unlocked!");
            }
            else
            {
                Debug.Log("Achievement '" + pCallback.m_rgchAchievementName + "' progress callback, (" + pCallback.m_nCurProgress + "," + pCallback.m_nMaxProgress + ")");
            }
        }
    }
    #endregion

    public void UnlockSteamAchivement(string ID)
    {
        SteamUserStats.ClearAchievement(ID);
        TestSteamAchivement(ID);
        Debug.Log(unlockTest);
        if (!unlockTest)
        {
            Debug.Log("enter");
            SteamUserStats.SetAchievement(ID);
            SteamUserStats.StoreStats();
        }
    }

    public void UnlockAchievement(string achievementID)
    {
        if (!SteamManager.Initialized && !debugMode)
            return;

        bool achivementFound = false;

        foreach (var a in Achievements)
            if (a.achievementID.ToString() == achievementID)
            {
                achivementFound = true;

                if (a.achieved)
                {
                    if (debugMode)
                        Debug.Log("Already unlocked: " + a.achievementID.ToString());
                    return;
                }

                a.achieved = true;

                if (debugMode)
                {
                    Debug.Log("Unlocked: " + a.achievementID.ToString());
                    return;
                }
                break;
            }

        if (!achivementFound)
        {
            Debug.LogWarning("Achievement Not Found " + achievementID + " !");
            return;
        }

        // mark it down
        SteamUserStats.SetAchievement(achievementID);

        StartCoroutine(StoreSteamData());
    }

    public void SetUnlockAchivements(string nameLevel, bool normal, bool flawless, bool speed125, bool speed150, bool NoNear, bool NoFar)
    {
        string numAchivement = "00";

        if (nameLevel == "LEVEL_EGYPT")
        {
            if (normal)
            {
                numAchivement = "00"; UnlockAchievement("achivement_" + numAchivement);
                if (speed125) { numAchivement = "02"; UnlockAchievement("achivement_" + numAchivement); }
                if (speed150) { numAchivement = "03"; UnlockAchievement("achivement_" + numAchivement); }
                if (NoNear) { numAchivement = "04"; UnlockAchievement("achivement_" + numAchivement); }
                if (NoFar) { numAchivement = "05"; UnlockAchievement("achivement_" + numAchivement); }
            }
            else if (flawless) { numAchivement = "01"; UnlockAchievement("achivement_" + numAchivement); }
        }
        else if (nameLevel == "LEVEL_BRAZIL")
        {
            if (normal)
            {
                numAchivement = "06"; UnlockAchievement("achivement_" + numAchivement);
                if (speed125) { numAchivement = "08"; UnlockAchievement("achivement_" + numAchivement); }
                if (speed150) { numAchivement = "09"; UnlockAchievement("achivement_" + numAchivement); }
                if (NoNear) { numAchivement = "10"; UnlockAchievement("achivement_" + numAchivement); }
                if (NoFar) { numAchivement = "11"; UnlockAchievement("achivement_" + numAchivement); }
            }
            else if (flawless) { numAchivement = "07"; UnlockAchievement("achivement_" + numAchivement); }

        }
        else if (nameLevel == "LEVEL_INDIA")
        {
            if (normal)
            {
                numAchivement = "12"; UnlockAchievement("achivement_" + numAchivement);
                if (speed125) { numAchivement = "14"; UnlockAchievement("achivement_" + numAchivement); }
                if (speed150) { numAchivement = "15"; UnlockAchievement("achivement_" + numAchivement); }
                if (NoNear) { numAchivement = "16"; UnlockAchievement("achivement_" + numAchivement); }
                if (NoFar) { numAchivement = "17"; UnlockAchievement("achivement_" + numAchivement); }
            }
            else if (flawless) { numAchivement = "13"; UnlockAchievement("achivement_" + numAchivement); }

        }
        else if (nameLevel == "LEVEL_CHINA")
        {
            if (normal)
            {
                numAchivement = "18"; UnlockAchievement("achivement_" + numAchivement);
                if (speed125) { numAchivement = "20"; UnlockAchievement("achivement_" + numAchivement); }
                if (speed150) { numAchivement = "21"; UnlockAchievement("achivement_" + numAchivement); }
                if (NoNear) { numAchivement = "22"; UnlockAchievement("achivement_" + numAchivement); }
                if (NoFar) { numAchivement = "23"; UnlockAchievement("achivement_" + numAchivement); }
            }
            else if (flawless) { numAchivement = "19"; UnlockAchievement("achivement_" + numAchivement); }

        }

        ///Complete 1 level 150% Flawless NoFarLanes & NoNearLanes
        if (flawless && speed150 && NoNear && NoFar)
        {
            UnlockAchievement("achivement_32");
        }

        CompleteAllLevel();
        CompleteAllNoNearLanes();
        CompleteAllNoFarLanes();
        CompleteAll125();
        CompleteAll150();
    }

    #region SoundSlide
     public void CompleteTuto()
    {
        UnlockAchievement("achivement_24");
    }

    public void CompleteLanesCrossed()
    {
        UnlockAchievement("achivement_25");
    }

    public void CompleteDeaths()
    {
        UnlockAchievement("achivement_26");
    }

    ///Complete all level in normal mode
    void CompleteAllLevel()
    {
        bool successLvlEgypt = false;
        bool successLvlBrazil = false;
        bool successLvlIndia = false;
        bool successLvlChina = false;
        SteamUserStats.GetAchievement("achivement_00", out successLvlEgypt);
        SteamUserStats.GetAchievement("achivement_06", out successLvlBrazil);
        SteamUserStats.GetAchievement("achivement_12", out successLvlIndia);
        SteamUserStats.GetAchievement("achivement_18", out successLvlChina);

        if (successLvlEgypt && successLvlBrazil && successLvlIndia && successLvlChina)
        {
            UnlockAchievement("achivement_27");
        }
    }

    ///Complete all level in no near mode
    void CompleteAllNoNearLanes()
    {
        bool successLvlEgypt = false;
        bool successLvlBrazil = false;
        bool successLvlIndia = false;
        bool successLvlChina = false;
        SteamUserStats.GetAchievement("achivement_04", out successLvlEgypt);
        SteamUserStats.GetAchievement("achivement_10", out successLvlBrazil);
        SteamUserStats.GetAchievement("achivement_16", out successLvlIndia);
        SteamUserStats.GetAchievement("achivement_22", out successLvlChina);

        if (successLvlEgypt && successLvlBrazil && successLvlIndia && successLvlChina)
        {
            UnlockAchievement("achivement_31");
        }
    }

    ///Complete all level in no far mode
    void CompleteAllNoFarLanes()
    {
        bool successLvlEgypt = false;
        bool successLvlBrazil = false;
        bool successLvlIndia = false;
        bool successLvlChina = false;
        SteamUserStats.GetAchievement("achivement_05", out successLvlEgypt);
        SteamUserStats.GetAchievement("achivement_11", out successLvlBrazil);
        SteamUserStats.GetAchievement("achivement_17", out successLvlIndia);
        SteamUserStats.GetAchievement("achivement_23", out successLvlChina);

        if (successLvlEgypt && successLvlBrazil && successLvlIndia && successLvlChina)
        {
            UnlockAchievement("achivement_30");
        }
    }

    ///Complete all level in 125 mode
    void CompleteAll125()
    {
        bool successLvlEgypt = false;
        bool successLvlBrazil = false;
        bool successLvlIndia = false;
        bool successLvlChina = false;
        SteamUserStats.GetAchievement("achivement_02", out successLvlEgypt);
        SteamUserStats.GetAchievement("achivement_08", out successLvlBrazil);
        SteamUserStats.GetAchievement("achivement_14", out successLvlIndia);
        SteamUserStats.GetAchievement("achivement_20", out successLvlChina);

        if (successLvlEgypt && successLvlBrazil && successLvlIndia && successLvlChina)
        {
            UnlockAchievement("achivement_28");
        }
    }

    ///Complete all level in 150 mode
    void CompleteAll150()
    {
        bool successLvlEgypt = false;
        bool successLvlBrazil = false;
        bool successLvlIndia = false;
        bool successLvlChina = false;
        SteamUserStats.GetAchievement("achivement_03", out successLvlEgypt);
        SteamUserStats.GetAchievement("achivement_09", out successLvlBrazil);
        SteamUserStats.GetAchievement("achivement_15", out successLvlIndia);
        SteamUserStats.GetAchievement("achivement_21", out successLvlChina);

        if (successLvlEgypt && successLvlBrazil && successLvlIndia && successLvlChina)
        {
            UnlockAchievement("achivement_29");
        }
    }

    void TestSteamAchivement(string ID)
    {
        SteamUserStats.GetAchievement(ID, out unlockTest);
    }
    #endregion

    #region  Custom Classes
    [System.Serializable]
    public class Achievement
    {
        public AchievementID achievementID;
        public string name;
        public string description;
        public bool achieved;

        public Achievement(string achievementID, string name = "", string desc = "")
        {
            this.achievementID = (AchievementID)System.Enum.Parse(typeof(AchievementID), achievementID);
            this.name = name;
            description = desc;
            achieved = false;
        }
    }

    public enum AchievementID
    {
        achivement_00,
        achivement_01,
        achivement_02,
        achivement_03,
        achivement_04,
        achivement_05,
        achivement_06,
        achivement_07,
        achivement_08,
        achivement_09,
        achivement_10,
        achivement_11,
        achivement_12,
        achivement_13,
        achivement_14,
        achivement_15,
        achivement_16,
        achivement_17,
        achivement_18,
        achivement_19,
        achivement_20,
        achivement_21,
        achivement_22,
        achivement_23,
        achivement_24,
        achivement_25,
        achivement_26,
        achivement_27,
        achivement_28,
        achivement_29,
        achivement_30,
        achivement_31,
        achivement_32
    }
    #endregion
}

