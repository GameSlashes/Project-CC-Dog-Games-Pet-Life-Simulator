using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fakeLoading : MonoBehaviour
{
    public Image fillBar;
    public bool onTime;
    public bool loadScene;
    public GameObject adLoad;


    public void Awake()
    {
        if (FindObjectOfType<Handler>())
        {
            FindObjectOfType<Handler>().LoadInterstitialAd();
            FindObjectOfType<Handler>().ShowMediumBanner(GoogleMobileAds.Api.AdPosition.BottomLeft);
        }

        fillBar.fillAmount = 0;
        onTime = false;

        if(PlayerPrefs.GetInt("adShowing") == 5)
        {
            adLoad.SetActive(false);
        }
    }

   
    public void Update()
    {
        fillBar.fillAmount += 0.2f * Time.deltaTime;

        if (fillBar.fillAmount >= 0.85f)
        {
            if (onTime == false)
            {
                if (FindObjectOfType<Handler>())
                    FindObjectOfType<Handler>().ShowInterstitialAd();
                offBB();
                onTime = true;

                adLoad.SetActive(false);
                PlayerPrefs.SetInt("adShowing", 0);
            }
        }

        if (fillBar.fillAmount >= 1f)
        {
            if (loadScene == false)
            {
                SceneManager.LoadScene(PlayerPrefs.GetString("sceneName"));
                loadScene = true;
            }
        }
    }

    public void offBB()
    {
        if (FindObjectOfType<Handler>())
        {
            FindObjectOfType<Handler>().HideMediumBannerEvent();
        }
    }
}
