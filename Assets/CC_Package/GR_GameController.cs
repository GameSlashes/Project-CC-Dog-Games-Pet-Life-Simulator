using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine; 

public class GR_GameController : MonoBehaviour
{
    public static GR_GameController instance;

    [Header("--------------------Scene Selection--------------------")]
    public Scenes PreviousScene;
    public Scenes MainMenu;
    [Header("--------------------Players-----------------------------")]
    public GameObject[] players;
    [Header("--------------------GirlPlayer-----------------------------")]
    public GameObject GirlPlayer;
    [Header("--------------------Levels-----------------------------")]
    public int playableLevels;
    public Level_Data[] levels;
    [Header("--------------------Game Dialogues--------------------")]
    public Game_Dialogues Game_Elements;
    [Header("--------------------Events--------------------")]
    public UnityEvent LevelCompleteEvent;
    public UnityEvent LevelFailedEvent;
    public UnityEvent LevelStartEvent;

    int minutes;
    int seconds;
    string time;

    public bool isTimerEnabled;
    public bool TimerPaused = false;
    public float LevelTime = 0.0f;
    public GameObject env;
    [Header("Settings")]
    public GameObject soundOnBtn;
    public GameObject soundOffBtn;  
    public GameObject MusicOnBtn;
    public GameObject MusicOffBtn;
    public CinemachineFreeLook freeLookCamera;
    public GameObject TapBtn;
    public GameObject CharacterController;
    public GameObject PuppyControler;
    public GameObject Sichue;
    public GameObject fadeimage;
    public string ATP;
    public Animator Anim;
    public GameObject victorypanal;
    private void _anim()
    {
       Anim = GR_GameController.instance.players[GR_SaveData.instance.finalPlayer].GetComponent<Animator>();
    }
    public void Awake()
    {
        instance = this;
        env.SetActive(true);

        if (GR_SaveData.instance.CurrentLevel == 4)
        {
            Sichue.SetActive(false);
            fadeimage.SetActive(true);
        }

        if (FindObjectOfType<Handler>())
        {
            if (PlayerPrefs.GetInt("loadRequest") == 5)
            {
                PlayerPrefs.SetInt("loadRequest", 1);
                FindObjectOfType<Handler>().LoadInterstitialAd();
            }
        }
    }

    IEnumerator  Start()
    {
        yield return new WaitForEndOfFrame();
        mCam();
        //ActivePlayer();
        ActiveLevel();
        LevelStartEvent.Invoke();
        checkModeStats();
        checkSound();
        if (isTimerEnabled)
            InvokeRepeating("GameTimer", 0, 1);
        if (GR_SoundManager.instance)
        {
            GR_SoundManager.instance.allSoundsOff();
        }
        Firebase.Analytics.FirebaseAnalytics.LogEvent("gamePlay_Mode_Level" + GR_SaveData.Instance.CurrentLevel + "play");
    }

    public void mCam()
    {
        for (int i = 0; i < freeLookCamera.m_Orbits.Length; i++)
        {
            freeLookCamera.GetRig(i).GetCinemachineComponent<CinemachineComposer>().m_VerticalDamping = 1f;
            freeLookCamera.GetRig(i).GetCinemachineComponent<CinemachineComposer>().m_HorizontalDamping =1f;
        }
    }
    public void OTDO()
    {
        if (OntrigerDoorOpen.Instance.animdoor_1 && levels[GR_SaveData.instance.CurrentLevel].ActiveEndScene)
        {
            if (levels[GR_SaveData.instance.CurrentLevel].LevelComplete)
            {
                if (GR_SoundManager.instance)
                {
                    GR_SoundManager.instance.allSoundsOff();
                }
                levelCompleted();
            }
            anim();
        }
        else if(OntrigerDoorOpen.Instance.animdoor_1 && levels[GR_SaveData.instance.CurrentLevel].LevelComplete)
        {
            if (GR_SoundManager.instance)
            {
                GR_SoundManager.instance.allSoundsOff();
            }
            levelCompleted();
            anim();
        }
        else if (levels[GR_SaveData.instance.CurrentLevel].ActiveEndScene && OntrigerDoorOpen.Instance.animdoor_1 == null)
        {
            if (levels[GR_SaveData.instance.CurrentLevel].MilkBowlPick)
            {
                if (levels[GR_SaveData.instance.CurrentLevel].MilkBowlPick.activeInHierarchy)
                {
                    TapBtn.SetActive(false);
                    OntrigerDoorOpen.Instance.enb.enabled = false;
                    levels[GR_SaveData.instance.CurrentLevel].MilkBowlInHand.SetActive(true);
                    Anim.SetBool("Carrying", true);
                    levels[GR_SaveData.instance.CurrentLevel].MilkBowlPick.SetActive(false);
                }
                else if(levels[GR_SaveData.instance.CurrentLevel].MilkBowlInHand.activeInHierarchy)
                {
                    if (GR_SoundManager.instance)
                    {
                        GR_SoundManager.instance.allSoundsOff();
                    }
                    OntrigerDoorOpen.Instance.enb.enabled = false;
                    TapBtn.SetActive(false);
                    levelCompleted();
                }
            }
            else
            {
                if (GR_SoundManager.instance)
                {
                    GR_SoundManager.instance.allSoundsOff();
                }
                TapBtn.SetActive(false);
                levelCompleted();
            }
        }
        else if(OntrigerDoorOpen.Instance.animdoor_1)
        {

            anim();
        }
        else if(levels[GR_SaveData.instance.CurrentLevel].MilkBowlPick)
        {
           
            TapBtn.SetActive(false);
            OntrigerDoorOpen.Instance.enb.enabled = false;
            levels[GR_SaveData.instance.CurrentLevel].MilkBowlInHand.SetActive(true);
            levels[GR_SaveData.instance.CurrentLevel].MilkBowlPick.SetActive(false);
        }
    }
    public void anim()
    {
        OntrigerDoorOpen.Instance.enb.enabled = false;
        TapBtn.SetActive(false);
        OntrigerDoorOpen.Instance.animdoor_1.DORestartById(ATP);
        OntrigerDoorOpen.Instance.animdoor_2.DORestartById(ATP);
    }
    #region mySounds
    public void allSoundsOff()
    {

        if (PlayerPrefs.GetInt("Music") == 1)
        {
            if (GR_SoundManager.instance)
            {
                GR_SoundManager.instance.gamePlaySound.GetComponent<AudioSource>().volume = .2f;
            }
        }
    }
    public void gamePlaySoundOn()
    {
        if (!PlayerPrefs.HasKey("Music"))
        {
            if (GR_SoundManager.instance)
            {
                if (Time.timeScale == 1)
                {
                    GR_SoundManager.instance.playGamePlaySound();
                    MusicOffBtn.SetActive(true);
                    MusicOnBtn.SetActive(false);
                    GR_SoundManager.instance.gamePlaySound.GetComponent<AudioSource>().volume = 1f;
                }
                else
                {
                    GR_SoundManager.instance.playGamePlaySound();
                    MusicOffBtn.SetActive(true);
                    MusicOnBtn.SetActive(false);
                    GR_SoundManager.instance.gamePlaySound.GetComponent<AudioSource>().volume = .3f;
                }
            }
        }
        else if (PlayerPrefs.GetInt("Music") == 1)
        {
            if (GR_SoundManager.instance)
            {
                if (Time.timeScale == 1)
                {
                    GR_SoundManager.instance.playGamePlaySound();
                    MusicOffBtn.SetActive(true);
                    MusicOnBtn.SetActive(false);
                    GR_SoundManager.instance.gamePlaySound.GetComponent<AudioSource>().volume = 1f;
                }
                else
                {
                    GR_SoundManager.instance.playGamePlaySound();
                    MusicOffBtn.SetActive(true);
                    MusicOnBtn.SetActive(false);
                    GR_SoundManager.instance.gamePlaySound.GetComponent<AudioSource>().volume = .3f;
                }
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
    public void playerPrefSet()
    {
    }
    public void PlayMainMenueSound()
    {
        if (GR_SoundManager.instance)
        {
            GR_SoundManager.instance.playMainMenuSound();

        }
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


    public void playBtnSound()
    {
        if (GR_SoundManager.instance)
            GR_SoundManager.instance.onButtonClickSound(GR_SoundManager.instance.buttonMainSound);
    }
    public void checkModeStats()
    {
        if (PlayerPrefs.GetInt("Mode") == 1)
        {

        }
        else if (PlayerPrefs.GetInt("Mode") == 2)
        {

        }
        else if (PlayerPrefs.GetInt("Mode") == 3)
        {

        }
        else if (PlayerPrefs.GetInt("Mode") == 4)
        {

        }
    }
    #endregion

    void activeLevelObjective()
    {
        if (levels[GR_SaveData.instance.CurrentLevel].Objectives != "")
        {
            Game_Elements.objectivePanel.SetActive(true);
            Game_Elements.audio.clip = levels[GR_SaveData.instance.CurrentLevel].Audio;
            Game_Elements.audio.Play();
            Game_Elements.objectiveText.text = levels[GR_SaveData.instance.CurrentLevel].Objectives;
            Invoke("activeLevelObjectiveDeactivate", 5f);
        }
    }
    public void activeLevelObjectiveDeactivate()
    {
        Game_Elements.objectivePanel.SetActive(false);
        gamePlaySoundOn();
        playerPrefSet();
    }

    void ActiveLevel()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (i == GR_SaveData.instance.CurrentLevel)
            {
                //if(GR_SaveData.instance.CurrentLevel== 5)
                //{
                //    levels[i].LevelObject.SetActive(true);
                //    if (levels[GR_SaveData.instance.CurrentLevel].StartScene)
                //    {
                //        levels[GR_SaveData.instance.CurrentLevel].StartScene.SetActive(true);
                //        Invoke("ActivePlayer", levels[GR_SaveData.instance.CurrentLevel].StartSceneTimer);
                //    }
                //}
                levels[i].LevelObject.SetActive(true);
                if (levels[GR_SaveData.instance.CurrentLevel].StartScene)
                {
                    levels[GR_SaveData.instance.CurrentLevel].StartScene.SetActive(true);
                    Invoke("ActivePlayer", levels[GR_SaveData.instance.CurrentLevel].StartSceneTimer);
                }
                else
                {
                    ActivePlayer();
                }
            }
            else
            {
                Destroy(levels[i].LevelObject);
            }
        }

        if (levels[GR_SaveData.instance.CurrentLevel].isTimeBased)
        {
            isTimerEnabled = true;
            Game_Elements.Timer.SetActive(true);
        }
        else
        {
            isTimerEnabled = false;
            Game_Elements.Timer.SetActive(false);
        }

        LevelTime = (levels[GR_SaveData.instance.CurrentLevel].Minutes * 60) + levels[GR_SaveData.instance.CurrentLevel].Seconds;
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    void GameTimer()
    {
        if (!TimerPaused)
        {
            if (LevelTime >= 0.0f)
                LevelTime -= 1;

            minutes = ((int)LevelTime / 60);
            seconds = ((int)LevelTime % 60);
            time = minutes.ToString("00") + ":" + seconds.ToString("00");
            Game_Elements.Timer_txt.text = time;
            if (LevelTime <= 15.0f && LevelTime > 0.0f)
            {
                Game_Elements.Timer_txt.color = Color.red;
            }
            else
            if (LevelTime == 0.0f)
            {
                Game_Elements.levelFailedText.SetActive(true);
                LevelFailedEvent.Invoke();
                StartCoroutine(failedDialogue());
            }
        }
    }
    public void levelfail()
    {
        Game_Elements.levelFailedText.SetActive(true);
        LevelFailedEvent.Invoke();
        StartCoroutine(failedDialogue());
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Failed_currentMode_Level" + GR_SaveData.Instance.CurrentLevel+ "Fail");
    }
    IEnumerator failedDialogue()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
        Game_Elements.levelFailedText.SetActive(false);
        Game_Elements.LevelFailed.SetActive(true);
    }
    public void ActivePlayer()
    {
        for (int i = 0; i < players.Length; i++)
        {

            if (i == GR_SaveData.instance.finalPlayer)
            {

                //players[i].SetActive(true);
                if (levels[GR_SaveData.instance.CurrentLevel].SpawnGirlActivation)
                {

                    activeLevelObjective();
                    CharacterController.SetActive(true);
                    setGirlPosition(GirlPlayer, levels[GR_SaveData.instance.CurrentLevel].SpawnGirl);
                    SetPlayerPosition(players[i], levels[GR_SaveData.instance.CurrentLevel].SpawnPoint);
                    if (levels[GR_SaveData.instance.CurrentLevel].StartScene)
                    {
                        levels[GR_SaveData.instance.CurrentLevel].StartScene.SetActive(false);
                    }
                }
                else
                {

                    activeLevelObjective();
                    PuppyControler.SetActive(true);
                    SetPlayerPosition(players[i], levels[GR_SaveData.instance.CurrentLevel].SpawnPoint);
                    if (levels[GR_SaveData.instance.CurrentLevel].StartScene)
                    {
                        levels[GR_SaveData.instance.CurrentLevel].StartScene.SetActive(false);
                    }
                }
            }
            //else
            //{
            //    Destroy(players[i]);
            //}
        }
    }

    void SetPlayerPosition(GameObject Player, Transform Position)
    {
        Player.transform.position = Position.position;
        Player.transform.rotation = Position.rotation;
    }
    void setGirlPosition(GameObject GirlPlayer, Transform GirlPosition)
    {
        GirlPlayer.transform.position = GirlPosition.position;
        GirlPlayer.transform.rotation = GirlPosition.rotation;
    }

    void checkLevelReward()
    {
        if (levels[GR_SaveData.instance.CurrentLevel].isRewardBase)
        {
            if (levels[GR_SaveData.instance.CurrentLevel].coinReward > 0)
            {
                Game_Elements.levelCompleteReward.text = levels[GR_SaveData.instance.CurrentLevel].coinReward.ToString();
                GR_SaveData.instance.Coins += levels[GR_SaveData.instance.CurrentLevel].coinReward;
            }
            else
            {
                Game_Elements.gameCompleteRewardCoinsObj.SetActive(false);
            }

            if (levels[GR_SaveData.instance.CurrentLevel].gemReward > 0)
            {
                if (Game_Elements.levelCompleteGem)
                {
                    Game_Elements.levelCompleteGem.text = levels[GR_SaveData.instance.CurrentLevel].gemReward.ToString();
                    GR_SaveData.instance.Gems += levels[GR_SaveData.instance.CurrentLevel].gemReward;
                }
            }
            else
            {
                Game_Elements.gameCompleteRewardGemsObj.SetActive(false);
            }

        }
        else
        {
            Game_Elements.gameCompleteRewardCoinsObj.SetActive(false);
            Game_Elements.gameCompleteRewardGemsObj.SetActive(false);
            Game_Elements.doubleRewardBtn.SetActive(false);
        }
    }

    public void levelCompleted()
    {
        checkLevelReward();
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Completed_currentMode_Level" + GR_SaveData.Instance.CurrentLevel+ "complete");
        if (GR_SaveData.instance.Level == GR_SaveData.instance.CurrentLevel + 1 && GR_SaveData.instance.Level <= playableLevels)
        {
            GR_SaveData.instance.Level += 1;
        }
        if (levels[GR_SaveData.instance.CurrentLevel].EndScene)
        {
            PuppyControler.SetActive(false);
            CharacterController.SetActive(false);
            levels[GR_SaveData.instance.CurrentLevel].EndScene.SetActive(true);
            LevelCompleteEvent.Invoke();
            Invoke("levelComp", levels[GR_SaveData.instance.CurrentLevel].EndSceneTimer);
        }
        else
        {
            PuppyControler.SetActive(false);
            CharacterController.SetActive(false);
            LevelCompleteEvent.Invoke();
            levelComp();
        }
    }
    public void levelComp()
    {
        StartCoroutine(completeDialogue());
        if (levels[GR_SaveData.instance.CurrentLevel].EndScene)
        {
            levels[GR_SaveData.instance.CurrentLevel].EndScene.SetActive(false);
        }
        if (GR_SoundManager.instance)
        {
            GR_SoundManager.instance.allSoundsOff();
            GR_SoundManager.instance.LevelCompeleteSoundON();
        }
        TapBtn.SetActive(false);
        Game_Elements.levelCompleteText.SetActive(true);
    }
    IEnumerator completeDialogue()
    {
        yield return new WaitForSeconds(4f);
        //Time.timeScale = 0;
        Game_Elements.levelCompleteText.SetActive(false);
        victorypanal.SetActive(true);
        yield return new WaitForSeconds(7f);
        victorypanal.SetActive(false);
        Game_Elements.LevelComplete.SetActive(true);
        if (GR_SoundManager.instance)
        {
            GR_SoundManager.instance.LevelCompeleteSoundOff();
            GR_SoundManager.instance.playMainMenuSound();
        }
    }

    public void NextBtn()
    {
        if (GR_SoundManager.instance)
            GR_SoundManager.instance.onButtonClickSound(GR_SoundManager.instance.buttonMainSound);

        if (GR_SaveData.instance.CurrentLevel < playableLevels - 1 && GR_SaveData.instance.Level < playableLevels)
            GR_SaveData.instance.CurrentLevel += 1;
        else
            GR_SaveData.instance.CurrentLevel = Random.Range(0, playableLevels - 1);

        Game_Elements.LoadingScreen.SetActive(true);
        LoadScene.SceneName = SceneManager.GetActiveScene().name;
    }

    public void mainMenu()
    {
        if (GR_SoundManager.instance)
            GR_SoundManager.instance.onButtonClickSound(GR_SoundManager.instance.buttonMainSound);

        Game_Elements.LoadingScreen.SetActive(true);
        LoadScene.SceneName = MainMenu.ToString();

    }

    public void restart()
    {
        if (GR_SoundManager.instance)
            GR_SoundManager.instance.onButtonClickSound(GR_SoundManager.instance.buttonMainSound);

        Game_Elements.LoadingScreen.SetActive(true);
        LoadScene.SceneName = SceneManager.GetActiveScene().name;
    }

    public void gamePause()
    {
        Time.timeScale = 0;
        Game_Elements.PauseMenu.SetActive(true);
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Pause_currentMode_Level" + GR_SaveData.Instance.CurrentLevel + "Pause");
    }

    public void resume()
    {
        Time.timeScale = 1;
        Game_Elements.PauseMenu.SetActive(false);
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Resume_currentMode_Level" + GR_SaveData.Instance.CurrentLevel+ "Resume");
    }

    public void doubleRewardBtn()
    {
        if (FindObjectOfType<Handler>())
            FindObjectOfType<Handler>().ShowRewardedAdsBoth(doubleTheReward);
    }

    public void doubleTheReward()
    {
        levels[GR_SaveData.instance.CurrentLevel].coinReward *= 2;
        levels[GR_SaveData.instance.CurrentLevel].gemReward *= 2;
        Game_Elements.levelCompleteReward.text = levels[GR_SaveData.instance.CurrentLevel].coinReward.ToString();
        GR_SaveData.instance.Coins += levels[GR_SaveData.instance.CurrentLevel].coinReward / 2;

        Game_Elements.levelCompleteGem.text = levels[GR_SaveData.instance.CurrentLevel].gemReward.ToString();
        GR_SaveData.instance.Gems += levels[GR_SaveData.instance.CurrentLevel].gemReward / 2;

        Game_Elements.doubleRewardBtn.SetActive(false);
    }
}

