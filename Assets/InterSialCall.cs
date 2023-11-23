using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterSialCall : MonoBehaviour
{
    public void OnEnable()
    {
        if (FindObjectOfType<Handler>())
        {
            FindObjectOfType<Handler>().ShowInterstitialAd();
        }
    }
    public void OnDisable()
    {
        if (FindObjectOfType<Handler>())
        {
            FindObjectOfType<Handler>().LoadInterstitialAd();
        }
    }
}
