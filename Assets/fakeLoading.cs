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


    public void Awake()
    {
        if (FindObjectOfType<Handler>())
        {
            FindObjectOfType<Handler>().ShowMediumBanner(GoogleMobileAds.Api.AdPosition.BottomLeft);
        }

        fillBar.fillAmount = 0;
        onTime = false;
    }

    public void Start()
    {
        if (FindObjectOfType<Handler>())
        {
            FindObjectOfType<Handler>().LoadInterstitialAd();
        }
    }
    public void Update()
    {
        fillBar.fillAmount += 0.2f * Time.deltaTime;

        if (fillBar.fillAmount >= 0.85f)
        {
            if (onTime == false)
            {
                offBB();
                onTime = true;
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
