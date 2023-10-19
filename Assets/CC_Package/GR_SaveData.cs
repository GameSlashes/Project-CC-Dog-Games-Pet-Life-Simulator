using System;
using UnityEngine;
using UnityEngine.UI;

public enum Scenes
{
    GR_Splash,
    GR_MainMenu,
    GR_PlayerSelection,
    GR_LevelSelection,
    GR_Gameplay
}

[System.Serializable]
public class Game_Dialogues
{
    public GameObject levelCompleteText;
    public GameObject LevelComplete;

    public GameObject levelFailedText;
    public GameObject LevelFailed;

    public Text levelCompleteGem;
    public Text levelCompleteReward;

    public GameObject gameCompleteRewardCoinsObj;
    public GameObject gameCompleteRewardGemsObj;

    public GameObject doubleRewardBtn;

    public GameObject PauseMenu;
    public GameObject LoadingScreen;
    public GameObject Timer;
    public Text Timer_txt;

    public GameObject objectivePanel;
    public Text objectiveText;
    public AudioSource audio;


}

[System.Serializable]
public class Level_Data
{
    public GameObject LevelObject;
    [Header("Player Spawn")]
    public Transform SpawnPoint;
    [Header("SpawnGirl")]
    public Transform SpawnGirl;
    [Header("SpawnGirlActivation")]
    public bool SpawnGirlActivation;
    [Header("StartSceneTimer")]
    public float StartSceneTimer;  
    [Header("StartScene")]
    public GameObject StartScene;
    [Header("ActiveStartScene")]
    public bool ActiveStartScene;
    [Header("EndSceneTimer")]
    public float EndSceneTimer;
    [Header("EndScene")]
    public GameObject EndScene;
    [Header("ActiveEndScene")]
    public bool ActiveEndScene;
    [Header("OnTrigerDoorLevelComplete")]
    public bool LevelComplete;
    [Header("Audio")]
    public AudioClip Audio;
    [Header("MilkBowlPick")]
    public GameObject MilkBowlPick;
    [Header("MilkBowlInHand")]
    public GameObject MilkBowlInHand;
    [Header("Tasks")]
    public string Objectives;
    [Tooltip("Level Time will not be considered if this field is unchecked.")]
    [Header("Level Time")]
    public bool isTimeBased;
    [Range(0, 60)]
    public int Minutes;
    [Range(10, 60)]
    public int Seconds;
    public bool isRewardBase;
    public int coinReward;
    public int gemReward;
}

[System.Serializable]
public class TyresModel
{
    public string name;
    public GameObject[] Models;
}

public class GR_SaveData
{
    public static GR_SaveData instance = new GR_SaveData();
    public static GR_SaveData Instance => instance;
    public static event Action allCashUpdate;
    public static event Action allGemsUpdate;

    public int CurrentLevel = 0;
    public int CurrentPlayer = 0;

    public int Coins
    {
        get
        {
            return PlayerPrefs.GetInt("coins");
        }
        set
        {
            PlayerPrefs.SetInt("coins", value);
            if (allCashUpdate != null)
            {
                allCashUpdate();
            }
        }
    }

    public int Gems
    {
        get
        {
            return PlayerPrefs.GetInt("gems");
        }
        set
        {
            PlayerPrefs.SetInt("gems", value);
            if (allGemsUpdate != null)
            {
                allGemsUpdate();
            }
        }
    }

    public int checkAd
    {
        get
        {
            return PlayerPrefs.GetInt("removeads");

        }
        set
        {
            PlayerPrefs.SetInt("removeads", value);
            //AdCalls.instance.RemoveBanner();
        }
    }


    public int finalPlayer
    {
        get
        {
            return PlayerPrefs.GetInt("selectedPlayer", 0);
        }
        set
        {
            PlayerPrefs.SetInt("selectedPlayer", value);
        }
    }

    public int Level
    {
        get
        {
            return PlayerPrefs.GetInt("Level", 1);
        }
        set
        {
            PlayerPrefs.SetInt("Level", value);
        }
    }

    public int ModeID
    {
        get
        {
            return PlayerPrefs.GetInt("playMode", 1);
        }
        set
        {
            PlayerPrefs.SetInt("playMode", value);
        }
    }
}
