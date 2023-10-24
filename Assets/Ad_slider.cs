using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Ad_slider : MonoBehaviour
{
   public GameObject x_1;
   public GameObject x_3;
   public GameObject x_5;
   public GameObject x_10;
   public Image fillamount;
    public GameObject handle;
    bool onetime;

    public void Start()
    {
        handle.FindComponent<DOTweenAnimation>();
     
    }
    public void onclick()
    {
        handle.FindComponent<DOTweenAnimation>().DOPause();
        handle.FindComponent<Collider2D>().enabled = true;

    }
       public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("x_1"))
        {
            if (onetime == false)
            {
                fillamount.fillAmount = 0.25f;
                onetime = true;
                GR_GameController.instance.levels[GR_SaveData.instance.CurrentLevel].coinReward *= 2;
                GR_GameController.instance.doubleTheReward();

            }
        }
        else if (collision.CompareTag("x_3"))
        {
            if (onetime == false)
            {
                fillamount.fillAmount = 0.5f;
                onetime = true;
                GR_GameController.instance.levels[GR_SaveData.instance.CurrentLevel].coinReward *= 3;
                GR_GameController.instance.doubleTheReward2();
  
            }
        }
        else if (collision.CompareTag("x_7"))
        {
            if (onetime == false)
            {
                fillamount.fillAmount = 0.75f;
                onetime = true;
                GR_GameController.instance.levels[GR_SaveData.instance.CurrentLevel].coinReward *= 5;
                GR_GameController.instance.doubleTheReward3();
            }
        }
        else if (collision.CompareTag("x_10"))
        {
            if (onetime == false)
            {
                fillamount.fillAmount = 1f;
                onetime = true;
                GR_GameController.instance.levels[GR_SaveData.instance.CurrentLevel].coinReward *= 7;
                GR_GameController.instance.doubleTheReward4();
     
            }
        }
         
    }
  
    public void rewardads()
    {
        if (FindObjectOfType<Handler>())
            FindObjectOfType<Handler>().ShowRewardedAdsBoth(onclick);
    }
}
