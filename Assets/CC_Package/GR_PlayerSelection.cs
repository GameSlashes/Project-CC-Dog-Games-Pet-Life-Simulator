using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Selection_Elements
{
    public GameObject LoadingScreen;

    [Header("Player Attributes")]
    public Image Speed_Bar;
    public Image Handling_Bar;
    public Image Acceleration_Bar;

    [Header("UI Buttons")]
    public Button PlayBtn;
    public GameObject NextBtn;
    public GameObject PrevBtn;
    public Button BuyButton;
    public Button buywithGems;
    public Button buywithWatchVideo;
    public Text playerPrice;
    public GameObject priceObject;
    public Image unlockAllPlayersBtn;
    public GameObject customizedButton;
    public GameObject buy_iap;
    public GameObject buy_watchvedio;
}

[System.Serializable]
public class PlayerAttributes
{
    public int playerID;
    public GameObject PlayerObject;
    [Range(0, 100)]
    public int Speed;
    [Range(0, 100)]
    public int Health;
    [Range(0, 100)]
    public int Acceleration;
    [Header("Unlocking")]
    public bool Locked;
    public bool UnlockThroughGems;
    public bool UnlockThroughWatchVideo;
    public bool UnlockThroughCoins;
    public int CoinsPrice;
    public string playerStats;
}


public class GR_PlayerSelection : MonoBehaviour
{
    public static GR_PlayerSelection instance;

    [Header("Scene Selection")]
    public Scenes PreviousScene;
    public Scenes NextScene;

    [Header("UI Elements")]
    public Selection_Elements Selection_UI;

    [Header("Player Attributes")]
    public PlayerAttributes[] Players;

    AsyncOperation async = null;
    [HideInInspector] public int current;
    [HideInInspector] public int currentHouse;

    [Header("Main Camera")]
    public GameObject mainCamera;
    public Transform cameraDefaultPosition;
    [HideInInspector] public bool defaultPosition;

    [Header("Customization")]
    public bool ifCustomization;
    public UnityEvent onCustomization;
    public UnityEvent backtoPlayerSelection;

    public GameObject env;
    public GameObject unsufficintCash;

    public void Awake()
    {
        instance = this;
        env.SetActive(true);
    }

    public void Start()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        GetPlayerInfo();

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
            if (PlayerPrefs.GetInt("loadRequest") == 5)
            {
                PlayerPrefs.SetInt("loadRequest", 1);
                FindObjectOfType<Handler>().LoadInterstitialAd();
            }
        }

        if (FindObjectOfType<Handler>())
        {
            FindObjectOfType<Handler>().ShowInterstitialAd();
        }
    }

    public void Update()
    {
        Selection_UI.Speed_Bar.fillAmount = Mathf.Lerp(Selection_UI.Speed_Bar.fillAmount, Players[current].Speed / 100.0f, Time.timeScale * 0.3f);
        Selection_UI.Handling_Bar.fillAmount = Mathf.Lerp(Selection_UI.Handling_Bar.fillAmount, Players[current].Health / 100.0f, Time.timeScale * 0.3f);
        Selection_UI.Acceleration_Bar.fillAmount = Mathf.Lerp(Selection_UI.Acceleration_Bar.fillAmount, Players[current].Acceleration / 100.0f, Time.timeScale * 0.3f);

        if (defaultPosition)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraDefaultPosition.position, Time.timeScale * 0.5f);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, cameraDefaultPosition.rotation, Time.timeScale * 0.5f);
        }

    }

    void GetPlayerInfo()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            if (i == current)
            {
                Players[i].PlayerObject.SetActive(true);
            }
            else
            if (i != current)
            {
                Players[i].PlayerObject.SetActive(false);
            }
        }

        if (Players[current].UnlockThroughCoins)
        {
            Selection_UI.BuyButton.gameObject.SetActive(true);
            Selection_UI.buywithGems.gameObject.SetActive(true);
            Selection_UI.buywithWatchVideo.gameObject.SetActive(true);
            Selection_UI.playerPrice.text = Players[current].CoinsPrice.ToString();
            //Selection_UI.buy_watchvedio.gameObject.SetActive(true);
            Selection_UI.buy_iap.gameObject.SetActive(true);
        }
        else if (Players[current].UnlockThroughGems)
        {
            Selection_UI.BuyButton.gameObject.SetActive(false);
            Selection_UI.buywithGems.gameObject.SetActive(true);
            Selection_UI.buywithWatchVideo.gameObject.SetActive(false);
            Selection_UI.playerPrice.text = Players[current].CoinsPrice.ToString();
        }
        else if (Players[current].UnlockThroughWatchVideo)
        {
            Selection_UI.BuyButton.gameObject.SetActive(false);
            Selection_UI.buywithGems.gameObject.SetActive(false);
            Selection_UI.buywithWatchVideo.gameObject.SetActive(true);
            Selection_UI.playerPrice.text = Players[current].CoinsPrice.ToString();
        }
        else
        {
            Selection_UI.BuyButton.gameObject.SetActive(false);
            Selection_UI.buywithGems.gameObject.SetActive(false);
            Selection_UI.buywithWatchVideo.gameObject.SetActive(false);
            Selection_UI.playerPrice.text = Players[current].CoinsPrice.ToString();
        }

        checkPlayerPurchasing();

        if (Players[current].Locked)
        {
            Selection_UI.PlayBtn.gameObject.SetActive(false);
            Selection_UI.unlockAllPlayersBtn.enabled = true;
            Selection_UI.unlockAllPlayersBtn.transform.GetChild(0).gameObject.SetActive(true);
            Selection_UI.priceObject.SetActive(true);
            if (ifCustomization)
                Selection_UI.customizedButton.SetActive(false);
        }
        else
        if (!Players[current].Locked)
        {
            Selection_UI.PlayBtn.gameObject.SetActive(true);
            Selection_UI.BuyButton.gameObject.SetActive(false);
            Selection_UI.unlockAllPlayersBtn.enabled = false;
            Selection_UI.unlockAllPlayersBtn.transform.GetChild(0).gameObject.SetActive(false);
            Selection_UI.buywithGems.gameObject.SetActive(false);
            Selection_UI.buywithWatchVideo.gameObject.SetActive(false);
            Selection_UI.priceObject.SetActive(false);
            if (ifCustomization)
                Selection_UI.customizedButton.SetActive(true);
        }

        if (current == 0)
        {
            Selection_UI.NextBtn.GetComponent<Button>().interactable = true;
            Selection_UI.PrevBtn.GetComponent<Button>().interactable = false;
            //Selection_UI.PrevBtn.SetActive(false);
            //Selection_UI.NextBtn.SetActive(true);
        }
        else
        if (current == Players.Length - 1)
        {
            Selection_UI.PrevBtn.GetComponent<Button>().interactable = true;
            Selection_UI.NextBtn.GetComponent<Button>().interactable = false;
            //Selection_UI.PrevBtn.SetActive(true);
            //Selection_UI.NextBtn.SetActive(false);
        }
        else
        {
            Selection_UI.PrevBtn.GetComponent<Button>().interactable = true;
            Selection_UI.NextBtn.GetComponent<Button>().interactable = true;
            Selection_UI.PrevBtn.SetActive(true);
            Selection_UI.NextBtn.SetActive(true);
        }
    }

    public void buyPlayer()
    {
        if (Players[current].UnlockThroughCoins)
        {
            if (GR_SaveData.Instance.Coins >= Players[current].CoinsPrice)
            {
                GR_SaveData.Instance.Coins -= Players[current].CoinsPrice;
                PlayerPrefs.SetString("UnlockedPlayer" + Players[current].playerID, "Purchased");
                Players[current].Locked = false;
                GetPlayerInfo();
                playBtnSound();

            }
            else
            {
                unsufficintCash.SetActive(true);
            }
        }
        else if (Players[current].UnlockThroughGems)
        {
            if (GR_SaveData.Instance.Gems >= Players[current].CoinsPrice)
            {
                GR_SaveData.Instance.Gems -= Players[current].CoinsPrice;
                PlayerPrefs.SetString("UnlockedPlayer" + Players[current].playerID, "Purchased");
                Players[current].Locked = false;
                GetPlayerInfo();
                playBtnSound();
            }
        }
        else if (Players[current].UnlockThroughWatchVideo)
        {
            if (FindObjectOfType<Handler>())
                FindObjectOfType<Handler>().ShowRewardedAdsBoth(buyWithWatchVideo);

        }
    }



    public void checkPlayerPurchasing()
    {
        for (int i = 1; i < Players.Length; i++)
        {
            if (PlayerPrefs.GetString("UnlockedPlayer" + Players[i].playerID) == "Purchased")
            {
                Players[i].Locked = false;
                Selection_UI.buywithWatchVideo.gameObject.SetActive(false);
                Selection_UI.buy_iap.gameObject.SetActive(false);
            }
            else
            {
                Players[i].Locked = true;
                Selection_UI.buywithWatchVideo.gameObject.SetActive(true);
                Selection_UI.buy_iap.gameObject.SetActive(true);
            }
        }
    }


    public void Previous()
    {
        current--;
        GetPlayerInfo();
        playBtnSound();
    }

    public void Next()
    {
        current++;
        GetPlayerInfo();
        playBtnSound();
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
        //Selection_UI.LoadingScreen.SetActive(true);
        //SceneManager.LoadScene(PreviousScene.ToString());
        
        PlayerPrefs.SetString("sceneName", PreviousScene.ToString());
        SceneManager.LoadScene("FakeLoading");
    }

    public void playBtnSound()
    {
        if (GR_SoundManager.instance)
            GR_SoundManager.instance.onButtonClickSound(GR_SoundManager.instance.buttonMainSound);
    }

    public void PlayLevel()
    {
        playBtnSound();
        GR_SaveData.instance.finalPlayer = current;
        GR_SaveData.instance.finalhouse = current;
        PlayerPrefs.SetInt("adShowing", 5);
        PlayerPrefs.SetString("sceneName", NextScene.ToString());
        SceneManager.LoadScene("FakeLoading");
        //Selection_UI.LoadingScreen.SetActive(true);
        //StartCoroutine(LevelStart());
    }

    IEnumerator LevelStart()
    {
        LoadScene.SceneName = NextScene.ToString();
        async = SceneManager.LoadSceneAsync(NextScene.ToString());
        yield return async;
    }

    public void buyplayer_watchvideo()
    {
        if (FindObjectOfType<Handler>())
        {
            FindObjectOfType<Handler>().ShowRewardedAdsBoth(buyWithWatchVideo);
        }
    }
    public void buyWithWatchVideo()
    {
        PlayerPrefs.SetString("UnlockedPlayer" + Players[current].playerID, "Purchased");
        Players[current].Locked = false;
        GetPlayerInfo();
        playBtnSound();
    }

    public void unlockAllPlayers()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            PlayerPrefs.SetString("UnlockedPlayer" + Players[i].playerID, "Purchased");
        }
        GetPlayerInfo();

    }

    public void cutmizationActive()
    {
        playBtnSound();
        onCustomization.Invoke();
    }
}
