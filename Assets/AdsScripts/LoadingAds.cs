using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingAds : MonoBehaviour
{
    public Handler handler;

    public static Handler.AfterLoading Notify;

    //public bool MediumBanner;
    //public bool SmallBanner;
    public bool IsTimeScaled;
    private void OnEnable()
    {
      
        Invoke(nameof(ShowInt), .2f);

    }

    void ShowInt()
    {

        handler.ShowInterstitialAd();
      
        {
            Invoke(nameof(ShowNextScreen), .1f);
        }

    }

    

    void ShowNextScreen()
    {
      
        if (IsTimeScaled)
        {
        this.gameObject.SetActive(false);

        }
        else
        {
            Invoke(nameof(DisableLoading), .1f);
        }

        if (Notify != null)
        {
        Notify();

        }
    }
    void DisableLoading()
    {
        this.gameObject.SetActive(false);
    }
}
