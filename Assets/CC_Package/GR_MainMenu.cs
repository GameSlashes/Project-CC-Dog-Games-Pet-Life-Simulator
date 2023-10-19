using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GR_MainMenu : MonoBehaviour
{
    [Header("Scene Selection")]
    public Scenes NextScene;

    [Header("UI Panels")]
    public GameObject SettingDialogue;
    public GameObject ExitDialogue;
    public GameObject Shop;
    public GameObject LoadingScreen;
    public Text totalCoins;

    [Header("Settings")]
    public GameObject soundOnBtn;
    public GameObject soundOffBtn;
    public GameObject MusicOnBtn;
    public GameObject MusicOffBtn;
    public Slider slider;
    public GameObject env;
    private void Awake()
    {
        env.SetActive(true);
    }

    void Start()
    {
        Time.timeScale = 1;
        InitializeUI();
        checkSound();
        gamePlaySoundOn();
        GR_SoundManager.instance.playMainMenuSound();

        if (FindObjectOfType<Handler>())
        {
            FindObjectOfType<Handler>().Show_SmallBanner1();
            FindObjectOfType<Handler>().Show_SmallBanner2();
        }

        if (PlayerPrefs.GetInt("showAds") == 1)
        {
            if (FindObjectOfType<Handler>())
            {
                FindObjectOfType<Handler>().ShowInterstitialAd();
                PlayerPrefs.SetInt("loadRequest", 5);
            }

            PlayerPrefs.SetInt("showAds", 0);
        }

        if (FindObjectOfType<Handler>())
        {
            FindObjectOfType<Handler>().LoadInterstitialAd();
        }
        Firebase.Analytics.FirebaseAnalytics.LogEvent("mainMenu_Screen_Open");
    }
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                ExitDialogue.SetActive(true);
            }
        }
        //AudioListener.volume = slider.value;
    }

    void InitializeUI()
    {
        SettingDialogue.SetActive(false);
        ExitDialogue.SetActive(false);
        Shop.SetActive(false);
        LoadingScreen.SetActive(false);
    }
    public void dailyReward()
    {
        RewardTimer.Instance.rewardPanel.enabled = true;
    }
    public void PlayBtn()
    {
     
        playBtnSound();
        //LoadingScreen.SetActive(true);
        //SceneManager.LoadScene(NextScene.ToString());
        PlayerPrefs.SetString("sceneName", NextScene.ToString());
        SceneManager.LoadScene("FakeLoading");

    }
    public void reloadads()
    {
        if (FindObjectOfType<Handler>())
        {
            Debug.LogError("loadmainmenu");
            FindObjectOfType<Handler>().LoadInterstitialAd();
        }
    }
    public void RemoveAds()
    {
        playBtnSound();
        GR_SaveData.instance.checkAd = 1;
    }

    public void openLink(string link)
    {
        playBtnSound();
        Application.OpenURL(link);
    }

    public void DialogueOpen(GameObject dialogue)
    {
        playBtnSound();
        dialogue.SetActive(true);
    }

    public void Exit()
    {
        playBtnSound();
        Application.Quit();
    }

    public void UnlockEverything()
    {
        GR_SaveData.instance.checkAd = 1;
    }

    public void playBtnSound()
    {
        if (GR_SoundManager.instance)
            GR_SoundManager.instance.onButtonClickSound(GR_SoundManager.instance.buttonMainSound);
    }


    public void soundOn()
    {
        AudioListener.volume = 1;
        soundOffBtn.SetActive(true);
        soundOnBtn.SetActive(false);
        PlayerPrefs.SetString("sound", "on");
        playBtnSound();
    }

    public void soundOff()
    {
        AudioListener.volume = 0;
        soundOffBtn.SetActive(false);
        soundOnBtn.SetActive(true);
        PlayerPrefs.SetString("sound", "off");
        playBtnSound();
    }
    public void MusicOn()
    {
        if (GR_SoundManager.instance.gamePlaySound)
        {
            GR_SoundManager.instance.gamePlaySound.GetComponent<AudioSource>().volume = 1f;
            GR_SoundManager.instance.gamePlaySound.GetComponent<AudioSource>().Play();
        }
        if (GR_SoundManager.instance.mainMenuSound)
        {
            GR_SoundManager.instance.mainMenuSound.GetComponent<AudioSource>().volume = 1f;
            GR_SoundManager.instance.mainMenuSound.GetComponent<AudioSource>().Play();
        }
        MusicOffBtn.SetActive(true);
        MusicOnBtn.SetActive(false);
        PlayerPrefs.SetString("Music", "on");
        PlayerPrefs.SetInt("Music", 1);
        playBtnSound();
    }
    public void MusicOff()
    {
        if (GR_SoundManager.instance.gamePlaySound)
        {
            GR_SoundManager.instance.gamePlaySound.GetComponent<AudioSource>().volume = 0f;
            GR_SoundManager.instance.gamePlaySound.GetComponent<AudioSource>().Stop();
        }
        if (GR_SoundManager.instance.mainMenuSound)
        {
            GR_SoundManager.instance.mainMenuSound.GetComponent<AudioSource>().volume = 0f;
            GR_SoundManager.instance.mainMenuSound.GetComponent<AudioSource>().Stop();
        }
        MusicOffBtn.SetActive(false);
        MusicOnBtn.SetActive(true);
        PlayerPrefs.SetString("Music", "off");
        PlayerPrefs.SetInt("Music", 2);
        playBtnSound();
    }
    public void gamePlaySoundOn()
    {
        if (!PlayerPrefs.HasKey("Music"))
        {
            if (GR_SoundManager.instance)
            {
                GR_SoundManager.instance.playGamePlaySound();
                MusicOffBtn.SetActive(true);
                MusicOnBtn.SetActive(false);
                GR_SoundManager.instance.gamePlaySound.GetComponent<AudioSource>().volume = 1f;
            }
        }
        else if (PlayerPrefs.GetInt("Music") == 1)
        {
            if (GR_SoundManager.instance)
            {
                GR_SoundManager.instance.playGamePlaySound();
                MusicOffBtn.SetActive(true);
                MusicOnBtn.SetActive(false);
                GR_SoundManager.instance.gamePlaySound.GetComponent<AudioSource>().volume = 1f;
            }
        }
        else if (PlayerPrefs.GetInt("Music") == 2)
        {
            if (GR_SoundManager.instance)
            {
                GR_SoundManager.instance.gamePlaySound.GetComponent<AudioSource>().volume = 0f;
                MusicOffBtn.SetActive(false);
                MusicOnBtn.SetActive(true);
            }
        }
    }
    public void checkSound()
    {
        if (!PlayerPrefs.HasKey("sound"))
        {
            soundOn();
        }
        else if (PlayerPrefs.GetString("sound") == "on")
        {
            soundOn();
        }
        else if (PlayerPrefs.GetString("sound") == "off")
        {
            soundOff();
        }
    }

}
