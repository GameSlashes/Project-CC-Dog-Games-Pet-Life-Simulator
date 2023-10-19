using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GR_LevelSelection : MonoBehaviour
{
    public static GR_LevelSelection instance;

    [Header("Scene Selection")]
    public Scenes PreviousScene;
    public Scenes NextScene;

    [Header("Settings")]
    public bool Locked;
    public int PlayableLevels = 6;

    [Header("UI Panels")]
    public GameObject LoadingScreen;
    public GameObject LevelsPanel;
    public GameObject ModeSelection;
    public GameObject LevelSelection;

    [Header("Mode Selection")]
    public GameObject mode3Locked;
    public GameObject mode4locked;

    private List<Button> LevelButtons = new List<Button>();
    AsyncOperation async = null;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        LoadingScreen.SetActive(false);
        CacheButtons();
        LevelsInit();
        checkMode();
        if (FindObjectOfType<Handler>())
        {
            FindObjectOfType<Handler>().LoadInterstitialAd();
          
        }


        if (FindObjectOfType<Handler>())
        {
            FindObjectOfType<Handler>().ShowInterstitialAd();
            PlayerPrefs.SetInt("loadRequest", 5);
        }
        Firebase.Analytics.FirebaseAnalytics.LogEvent("modeSelection_Screen_Open");
    }
    public void playBtnSound()
    {
        if (GR_SoundManager.instance)
            GR_SoundManager.instance.onButtonClickSound(GR_SoundManager.instance.buttonMainSound);
    }
    //void CacheButtons()
    //{
    //    Button[] levelButtons = LevelsPanel.transform.GetComponentsInChildren<Button>();
    //    for (int i = 0; i < levelButtons.Length; i++)
    //    {
    //        LevelButtons.Add(levelButtons[i]);
    //    }
    //    LevelButtons = LevelButtons.OrderBy(x => Int32.Parse(x.gameObject.name)).ToList();
    //    for (int i = 0; i < LevelButtons.Count; i++)
    //    {
    //        int LevelIndex = i;
    //        LevelButtons[i].onClick.AddListener(() => PlayLevel(LevelIndex));
    //        //if (GR_SoundManager.instance)
    //        //    GR_SoundManager.instance.onButtonClickSound(GR_SoundManager.instance.buttonMainSound);
    //    }
    //}
    void CacheButtons()
    {
        Button[] levelButtons = LevelsPanel.transform.GetComponentsInChildren<Button>();
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (levelButtons[i].gameObject.name != "AD")
            {
                if (levelButtons[i].gameObject.name != "LOCKED")
                    LevelButtons.Add(levelButtons[i]);
            }

        }
        LevelButtons = LevelButtons.OrderBy(x => Int32.Parse(x.gameObject.name)).ToList();
        for (int i = 0; i < LevelButtons.Count; i++)
        {
            int LevelIndex = i;
            LevelButtons[i].onClick.AddListener(() => PlayLevel(LevelIndex));
            if (GR_SoundManager.instance)
                GR_SoundManager.instance.onButtonClickSound(GR_SoundManager.instance.buttonMainSound);
        }
    }

    void LevelsInit()
    {
        ModeSelection.SetActive(true);
        LevelSelection.SetActive(false);

        if (!Locked)
        {
            for (int i = 0; i < LevelButtons.Count; i++)
            {
                if (i < PlayableLevels)
                {
                    LevelButtons[i].interactable = true;
                    LevelButtons[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    LevelButtons[i].interactable = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < LevelButtons.Count; i++)
            {
                LevelButtons[i].interactable = false;
                LevelButtons[i].transform.GetChild(1).gameObject.SetActive(true);
            }

            for (int i = 0; i < LevelButtons.Count; i++)
            {
                if (i < GR_SaveData.Instance.Level && i < PlayableLevels)
                {
                    LevelButtons[i].interactable = true;
                    LevelButtons[i].transform.GetChild(1).gameObject.SetActive(false);
                }
            }
        }
    }

    public void PlayLevel(int level)
    {
        GR_SaveData.Instance.CurrentLevel = level;
        PlayerPrefs.SetString("sceneName", NextScene.ToString());
        SceneManager.LoadScene("FakeLoading");
        //LoadingScreen.SetActive(true);
        //LoadScene.SceneName = NextScene.ToString();
        Firebase.Analytics.FirebaseAnalytics.LogEvent("PlayLevel_" + GR_SaveData.Instance.CurrentLevel + "current Mode");
    }

    public void BackBtn()
    {
        if (FindObjectOfType<Handler>())
        {
            if (PlayerPrefs.GetInt("loadRequest") == 5)
            {
                PlayerPrefs.SetInt("loadRequest", 1);
                FindObjectOfType<Handler>().LoadInterstitialAd();
            }
        }

        PlayerPrefs.SetInt("showAds", 1);

        //LoadingScreen.SetActive(true);
        //LoadScene.SceneName = PreviousScene.ToString();
        PlayerPrefs.SetString("sceneName", PreviousScene.ToString());
        SceneManager.LoadScene("FakeLoading");
    }

    public void checkMode()
    {
        if (PlayerPrefs.GetInt("UnlockMode3") == 2)
            mode3Locked.SetActive(false);
        else
            mode3Locked.SetActive(true);

        if (PlayerPrefs.GetInt("UnlockMode4") == 2)
            mode4locked.SetActive(false);
        else
            mode4locked.SetActive(true);
    }

    public void unlockMode(int modeNumber)
    {
        if (modeNumber == 3)
        {
            mode3Locked.SetActive(false);
            PlayerPrefs.SetInt("UnlockMode3", 2);
        }
        else if (modeNumber == 4)
        {
            mode4locked.SetActive(false);
            PlayerPrefs.SetInt("UnlockMode4", 2);
        }
    }

    public void playMode(int modeID)
    {
        GR_SaveData.instance.ModeID = modeID;

        if (modeID == 1)
        {
            ModeSelection.SetActive(false);
            LevelSelection.SetActive(true);
            PlayerPrefs.SetInt("Mode", 1);
        }
        else if (modeID == 2)
        {
            ModeSelection.SetActive(false);
            LoadingScreen.SetActive(true);
            PlayLevel(0);
            PlayerPrefs.SetInt("Mode", 2);
        }
        else if (modeID == 3)
        {
            if (PlayerPrefs.GetInt("UnlockMode3") == 2)
            {
                ModeSelection.SetActive(false);
                LoadingScreen.SetActive(true);
                PlayLevel(0);
                PlayerPrefs.SetInt("Mode", 2);
            }
            else
            {
            }

        }
        else if (modeID == 4)
        {
            if (PlayerPrefs.GetInt("UnlockMode4") == 2)
            {
                ModeSelection.SetActive(false);
                LoadingScreen.SetActive(true);
                PlayLevel(0);
                PlayerPrefs.SetInt("Mode", 4);
            }
            else
            {
            }

        }

        if (GR_SoundManager.instance)
            GR_SoundManager.instance.onButtonClickSound(GR_SoundManager.instance.buttonMainSound);


        if (FindObjectOfType<Handler>())
        {
            if (PlayerPrefs.GetInt("loadRequest") == 5)
            {
                PlayerPrefs.SetInt("loadRequest", 1);
                FindObjectOfType<Handler>().LoadInterstitialAd();
            }
        }
    }

    public void showInt()
    {
        if (FindObjectOfType<Handler>())
        {
            FindObjectOfType<Handler>().ShowInterstitialAd();
            PlayerPrefs.SetInt("loadRequest", 5);
        }
    }


}
