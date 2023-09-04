using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showAD : MonoBehaviour
{
  
    public void showBanner()
    {
        if (FindObjectOfType<Handler>())
        {
            FindObjectOfType<Handler>().Show_SmallBanner1();
            FindObjectOfType<Handler>().Show_SmallBanner2();
        }
    }

    public void loadInter()
    {
        if (FindObjectOfType<Handler>())
        {
            FindObjectOfType<Handler>().LoadInterstitialAd();
        }
    }

    public void showInt()
    {
        if (FindObjectOfType<Handler>())
        {
            FindObjectOfType<Handler>().ShowInterstitialAd();
        }
    }

    public void showReward()
    {
        if (FindObjectOfType<Handler>())
        {
            FindObjectOfType<Handler>().ShowRewardedAdsBoth(rewardDone);
        }
    }

    public GameObject reward;

     void rewardDone()
    {
        reward.SetActive(true);
    }

    public void bigBanner()
    {
        if (FindObjectOfType<Handler>())
        {
            FindObjectOfType<Handler>().ShowMediumBanner(GoogleMobileAds.Api.AdPosition.Center);
        }
    }
}
