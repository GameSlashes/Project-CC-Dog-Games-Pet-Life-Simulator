using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class videoLevel : MonoBehaviour
{
    public int ID;
    public Button btn;

    public void OnEnable()
    {
        if(PlayerPrefs.GetInt("ID" + ID) == 5)
        {
            btn.interactable = true;
            this.gameObject.SetActive(false);
        }

        this.gameObject.GetComponent<Button>().onClick.AddListener(() => watchVideo());
    }

    public void watchVideo()
    {
        if (FindObjectOfType<Handler>())
            FindObjectOfType<Handler>().ShowRewardedAdsBoth(rewardReturn);
    }

    public void rewardReturn()
    {
        PlayerPrefs.SetInt("ID" + ID, 5);
        btn.interactable = true;
        this.gameObject.SetActive(false);
    }
}
