using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class rewardItem : MonoBehaviour
{
    public string itemName;
    public int itemID;

    public GameObject lockedBtn;

    public UnityEvent rewardedFunction;

    public void OnEnable()
    {
        lockedBtn = this.gameObject.transform.GetChild(2).gameObject;

        if(PlayerPrefs.GetInt(itemName + itemID) == 5)
        {
            lockedBtn.SetActive(false);
        }
    }

    public void showReward()
    {
        if (PlayerPrefs.GetInt(itemName + itemID) == 5)
        {
            rewardDone();
        }
        else
        {
            if (FindObjectOfType<Handler>())
                FindObjectOfType<Handler>().ShowRewardedAdsBoth(rewardDone);
        }

    }

    public void rewardDone()
    {
        PlayerPrefs.SetInt(itemName + itemID, 5);
        lockedBtn.SetActive(false);
        rewardedFunction.Invoke();
    }
}
