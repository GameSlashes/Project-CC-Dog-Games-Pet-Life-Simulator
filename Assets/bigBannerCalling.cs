using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigBannerCalling : MonoBehaviour
{
    public bool interstitial;

    public void OnEnable()
    {
        if(interstitial)
        {
            if (FindObjectOfType<Handler>())
            {
                FindObjectOfType<Handler>().ShowInterstitialAd();
                PlayerPrefs.SetInt("loadRequest", 5);
            }
                
        }

        if (FindObjectOfType<Handler>())
            FindObjectOfType<Handler>().ShowMediumBanner(GoogleMobileAds.Api.AdPosition.BottomLeft);
    }

    public void offBanner()
    {
        if (FindObjectOfType<Handler>())
            FindObjectOfType<Handler>().HideMediumBannerEvent();

        if(interstitial)
        {
            if (FindObjectOfType<Handler>())
            {
                if(PlayerPrefs.GetInt("loadRequest") == 5)
                {
                    PlayerPrefs.SetInt("loadRequest", 1);
                    FindObjectOfType<Handler>().LoadInterstitialAd();
                }
            }
                
        }
    }
}
