using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigBannerCall : MonoBehaviour
{
    public void OnEnable()
    {
        if (FindFirstObjectByType<Handler>())
        {
            FindFirstObjectByType<Handler>().ShowMediumBanner(GoogleMobileAds.Api.AdPosition.BottomLeft);
        }
    }
    public void OnDisable()
    {
        if (FindFirstObjectByType<Handler>())
        {
            FindFirstObjectByType<Handler>().HideMediumBannerEvent();
        }
    }
       
 }

