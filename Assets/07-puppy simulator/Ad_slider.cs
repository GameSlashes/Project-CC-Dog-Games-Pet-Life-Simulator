using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Ad_slider : MonoBehaviour
{
    public GameObject handle;
    bool onetime;
    bool rewardComplete;

    public GameObject btn;

    public bool x1, x2, x3, x4;

    public void Start()
    {
        handle.FindComponent<DOTweenAnimation>();

    }
    public void onclick()
    {
        handle.FindComponent<DOTweenAnimation>().DOPause();
        handle.FindComponent<Collider2D>().enabled = true;
        rewardComplete = true;
        btn.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("x_1"))
        {
            x1 = true;
            x2 = false;
            x3 = false;
            x4 = false;
        }
        else if (other.CompareTag("x_3"))
        {
            x1 = false;
            x2 = true;
            x3 = false;
            x4 = false;

        }
        else if (other.CompareTag("x_7"))
        {
            x1 = false;
            x2 = false;
            x3 = true;
            x4 = false;
        }
        else if (other.CompareTag("x_10"))
        {
            x1 = false;
            x2 = false;
            x3 = false;
            x4 = true;
        }
    }


    public void Update()
    {
        if(rewardComplete == true)
        {
            if (x1)
            {
                if (onetime == false)
                {
                    onetime = true;
                    GR_GameController.instance.doubleTheReward();
                }
            }
            else if (x2)
            {
                if (onetime == false)
                {
                    onetime = true;
                    GR_GameController.instance.doubleTheReward2();
                }
            }
            else if (x3)
            {
                if (onetime == false)
                {
                    onetime = true;
                    GR_GameController.instance.doubleTheReward3();
                }
            }
            else if (x4)
            {
                if (onetime == false)
                {
                    onetime = true;
                    GR_GameController.instance.doubleTheReward4();
                }
            }
        }
        
    }


    public void rewardads()
    {
        if (FindObjectOfType<Handler>())
            FindObjectOfType<Handler>().ShowRewardedAdsBoth(onclick);
    }
}
