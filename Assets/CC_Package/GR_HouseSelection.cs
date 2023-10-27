using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Selection_ElementsHouse
{
    public GameObject LoadingScreen;
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
    public GameObject iapButtons;
    public GameObject watchVideoBtn;


}
[System.Serializable]
public class HouseAttributes
{
    public int HouseID;
    public GameObject HouseObject;

    [Header("Unlocking")]
    public bool Locked;
    public bool UnlockThroughGems;
    public bool UnlockThroughWatchVideo;
    public bool UnlockThroughCoins;
    public int CoinsPrice;
    public string HouseStats;
}


public class GR_HouseSelection : MonoBehaviour
{
    public static GR_HouseSelection instance;

    [Header("Scene Selection")]
    public Scenes PreviousScene;
    public Scenes NextScene;

    [Header("UI Elements")]
    public Selection_ElementsHouse Selection_UI;

    [Header("House Attributes")]
    public HouseAttributes[] House;

    AsyncOperation async = null;
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
        if (defaultPosition)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraDefaultPosition.position, Time.timeScale * 0.5f);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, cameraDefaultPosition.rotation, Time.timeScale * 0.5f);
        }

    }
    void GetHouseInfo()
    {

        for (int i = 0; i < House.Length; i++)
        {
            if (i == currentHouse)
            {
                House[i].HouseObject.SetActive(true);
            }
            else
            if (i != currentHouse)
            {
                House[i].HouseObject.SetActive(false);
            }
        }


        if (House[currentHouse].UnlockThroughCoins)
        {
            Selection_UI.BuyButton.gameObject.SetActive(true);
            Selection_UI.buywithGems.gameObject.SetActive(true);
            Selection_UI.playerPrice.text = House[currentHouse].CoinsPrice.ToString();
            Selection_UI.watchVideoBtn.SetActive(true);
            Selection_UI.iapButtons.SetActive(true);
        }
        else if (House[currentHouse].UnlockThroughGems)
        {
            Selection_UI.BuyButton.gameObject.SetActive(false);
            Selection_UI.buywithGems.gameObject.SetActive(true);
            Selection_UI.buywithWatchVideo.gameObject.SetActive(false);
            Selection_UI.playerPrice.text = House[currentHouse].CoinsPrice.ToString();
        }
        else if (House[currentHouse].UnlockThroughWatchVideo)
        {
            Selection_UI.BuyButton.gameObject.SetActive(false);
            Selection_UI.buywithGems.gameObject.SetActive(false);
            Selection_UI.buywithWatchVideo.gameObject.SetActive(true);
            Selection_UI.playerPrice.text = House[currentHouse].CoinsPrice.ToString();
        }
        else
        {
            Selection_UI.BuyButton.gameObject.SetActive(false);
            Selection_UI.buywithGems.gameObject.SetActive(false);
            Selection_UI.buywithWatchVideo.gameObject.SetActive(false);
            Selection_UI.playerPrice.text = House[currentHouse].CoinsPrice.ToString();
        }


        checkHousePurchasing();

        if (House[currentHouse].Locked)
        {
            Selection_UI.PlayBtn.gameObject.SetActive(false);
            Selection_UI.unlockAllPlayersBtn.enabled = true;
            Selection_UI.unlockAllPlayersBtn.transform.GetChild(0).gameObject.SetActive(true);
            Selection_UI.priceObject.SetActive(true);
            if (ifCustomization)
                Selection_UI.customizedButton.SetActive(false);


        }
        else
        if (!House[currentHouse].Locked)
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

            Selection_UI.watchVideoBtn.SetActive(false);
            Selection_UI.iapButtons.SetActive(false);
        }

        if (currentHouse == 0)
        {
            Selection_UI.PrevBtn.SetActive(false);
            Selection_UI.NextBtn.SetActive(true);
        }
        else
        if (currentHouse == House.Length - 1)
        {
            Selection_UI.PrevBtn.SetActive(true);
            Selection_UI.NextBtn.SetActive(false);
        }
        else
        {
            Selection_UI.PrevBtn.SetActive(true);
            Selection_UI.NextBtn.SetActive(true);
        }
    }
    public void buyHouse()
    {
        if (House[currentHouse].UnlockThroughCoins)
        {
            if (GR_SaveData.Instance.Coins >= House[currentHouse].CoinsPrice)
            {
                GR_SaveData.Instance.Coins -= House[currentHouse].CoinsPrice;
                PlayerPrefs.SetString("UnlockedHouse" + House[currentHouse].HouseID, "Purchased");
                House[currentHouse].Locked = false;
                GetHouseInfo();
                playBtnSound();

            }
            else
            {
                unsufficintCash.SetActive(true);
            }
        }
        else if (House[currentHouse].UnlockThroughGems)
        {
            if (GR_SaveData.Instance.Gems >= House[currentHouse].CoinsPrice)
            {
                GR_SaveData.Instance.Gems -= House[currentHouse].CoinsPrice;
                PlayerPrefs.SetString("UnlockedHouse" + House[currentHouse].HouseID, "Purchased");
                House[currentHouse].Locked = false;

                playBtnSound();
            }
        }
        else if (House[currentHouse].UnlockThroughWatchVideo)
        {


            if (FindObjectOfType<Handler>())
                FindObjectOfType<Handler>().ShowRewardedAdsBoth(buyWithWatchVideo);



        }
    }

    public void buyhouse_watchvideo()
    {
        Debug.Log("lock");
        if (FindObjectOfType<Handler>())
        {
            FindObjectOfType<Handler>().ShowRewardedAdsBoth(buyWithWatchVideo);
        }
    }
    public void checkHousePurchasing()
    {
        for (int i = 1; i < House.Length; i++)
        {
            if (PlayerPrefs.GetString("UnlockedHouse" + House[i].HouseID) == "Purchased")
            {
                House[i].Locked = false;
                Selection_UI.buywithWatchVideo.gameObject.SetActive(false);
                Selection_UI.watchVideoBtn.SetActive(false);
                Selection_UI.iapButtons.SetActive(false);
            }
            else
            {
                House[i].Locked = true;
                Selection_UI.buywithWatchVideo.gameObject.SetActive(true);
                Selection_UI.watchVideoBtn.SetActive(true);
                Selection_UI.iapButtons.SetActive(true);
            }
        }
    }

    public void Previous()
    {

        currentHouse--;
        GetHouseInfo();
        playBtnSound();
    }

    public void Next()
    {
        currentHouse++;
        GetHouseInfo();
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
        GR_SaveData.instance.finalPlayer = currentHouse;
        GR_SaveData.instance.finalhouse = currentHouse;
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


    public void buyWithWatchVideo()
    {
        Debug.LogError(House[currentHouse].HouseID);

        PlayerPrefs.SetString("UnlockedHouse" + House[currentHouse].HouseID, "Purchased");
        House[currentHouse].Locked = false;
        GetHouseInfo();
        playBtnSound();
    }

    public void unlockAllHOuse()
    {
        for (int i = 0; i < House.Length; i++)
        {
            PlayerPrefs.SetString("UnlockedHouse" + House[i].HouseID, "Purchased");
        }
        GetHouseInfo();

    }

    public void cutmizationActive()
    {
        playBtnSound();
        onCustomization.Invoke();
    }

}


