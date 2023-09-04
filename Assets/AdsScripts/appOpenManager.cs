
using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;


public class appOpenManager : MonoBehaviour
{
    public static appOpenManager instance;
    public bool TestAds;

    public string Admob_App_Open;

    private AppOpenAd ad;
    private bool isShowingAd = false;
    private DateTime loadTime;


    #region ----------------------- Start --------------------------
    private void Start()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        if (TestAds)
        {

#if UNITY_ANDROID
            Admob_App_Open = "ca-app-pub-3940256099942544/3419835294";
#elif UNITY_IOS
            Admob_App_Open = "ca-app-pub-3940256099942544/5662855259";
#else
            Admob_App_Open = "unexpected_platform";
#endif
        }

        #region AppOpen
        LoadAd();
        #endregion
    }
    #endregion

    #region -----------------AppOpenAdFunction--------------------

    private bool IsAdAvailable
    {
        get
        {
            // COMPLETE: Consider ad expiration
            return ad != null && (System.DateTime.UtcNow - loadTime).TotalHours < 4;
        }
    }

    public void LoadAd()
    {
        Debug.Log("load");

        var adRequest = new AdRequest();

        // Load an app open ad for portrait orientation
        AppOpenAd.Load(Admob_App_Open, adRequest, ((appOpenAd, error) =>
        {
            if (error != null)
            {
                // Handle the error.
                return;
            }

            // App open ad is loaded
            ad = appOpenAd;
            Debug.Log("App open ad loaded");

            // COMPLETE: Keep track of time when the ad is loaded.
            loadTime = DateTime.UtcNow;
        }));
    }

    public void ShowAdIfAvailable()
    {

        Debug.Log("show");

        if (!IsAdAvailable || isShowingAd)
        {
            return;
        }

        RegisterEventHandlers(ad);

        ad.Show();
    }
    private void RegisterEventHandlers(AppOpenAd ad)
    {
        ad.OnAdPaid += (AdValue adValue) =>
        {

        };

        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Displayed app open ad");
            isShowingAd = true;
            Debug.Log("Recorded ad impression");
        };

        ad.OnAdClicked += () =>
        {

        };

        ad.OnAdFullScreenContentOpened += () =>
        {

        };

        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Closed app open ad");
            // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
            ad = null;
            isShowingAd = false;
            LoadAd();
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
           
            // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
            ad = null;
            LoadAd();
        };
    }

    #endregion


    #region ------------------- Ad Calling Functions--------------------

    public void showAppOpen()
    {
        ShowAdIfAvailable();
    }

    #endregion


}
