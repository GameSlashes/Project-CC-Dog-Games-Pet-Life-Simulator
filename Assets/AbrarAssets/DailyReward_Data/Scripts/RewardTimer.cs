using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RewardTimer : MonoBehaviour
{
    public static RewardTimer Instance;

    public Canvas rewardPanel;
    public GameObject congratesPanel;

    public GameObject[] ClaimBtns, readyReward, Claimed;


    public float timeRemaining;
    public Text timeText;

    DateTime currentDate;
    DateTime LastClaimed;
    DateTime oldDate;
    private int Day = 0;
    private bool collected;


    private void Awake()
    {
        Instance = this;

        if (Instance == null)
        {
            Instance = FindObjectOfType<RewardTimer>();
        }
    }


    void Start()
    {
        timeRemaining = timeRemaining - PlayerPrefs.GetFloat("Timepassed");
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("LastClaimed")))
        {
            oldDate = System.DateTime.Parse(PlayerPrefs.GetString("LastClaimed"));
        }
       
        Check();
        ClaimedRewardDays();
        activeCalimedOnes();
    }
     public void reloadads()
    {
        if (FindObjectOfType<Handler>())
        {
            FindObjectOfType<Handler>().LoadInterstitialAd();
        }
    }
    public void back()
    {
        if (GR_SoundManager.instance)
            GR_SoundManager.instance.onButtonClickSound(GR_SoundManager.instance.buttonMainSound);
        if (FindObjectOfType<Handler>())
        {
            FindObjectOfType<Handler>().ShowInterstitialAd();
        }
        rewardPanel.enabled = (false);
    }

    private void activeCalimedOnes()
    {
        for (int i = 0; i < 7; i++)
        {
            if (PlayerPrefs.GetInt("Day" + i) == 1)
            {
                Claimed[i].SetActive(true);
            }


        }
    }
    private void Check()
    {
        currentDate = System.DateTime.Now;
        System.TimeSpan TimeDiff = currentDate.Subtract(oldDate);
        //   float seconds = Convert.ToSingle(TimeDiff.TotalSeconds);

        timeRemaining = (float)(86400 - TimeDiff.TotalSeconds);

        if (timeRemaining > 0)
        {

            timeRemaining -= Time.deltaTime;
            //  PlayerPrefs.SetFloat("Timepassed", seconds);
            DisplayTime(timeRemaining);
        }

        if (PlayerPrefs.GetInt("Day0") != 1)
        {
            if (TimeDiff.Days >= 1 || TimeDiff.Hours >= 24/*TimeDiff.Seconds >= 5*/)
            {
                ClaimBtns[PlayerPrefs.GetInt("lastClaimedDay")].SetActive(true);
                //   aCTIVATEDrEWARDiMG[PlayerPrefs.GetInt("lastClaimedDay")].SetActive(true);

                readyReward[PlayerPrefs.GetInt("lastClaimedDay")].SetActive(true);

                //ClaimedImgs[PlayerPrefs.GetInt("lastClaimedDay")].SetActive(true);
            }

        }
        else
        {
            if (TimeDiff.Days >= 1 || TimeDiff.Hours >= 24/*TimeDiff.Seconds >= 5*/)
            {
                // Debug.Log("here1");
                timeRemaining = 0;
                if (PlayerPrefs.GetInt("clicked") != 1)
                {

                    PlayerPrefs.SetInt("lastClaimedDay", PlayerPrefs.GetInt("lastClaimedDay") + 1);
                    if (PlayerPrefs.GetInt("lastClaimedDay") < 6)
                    {
                        ClaimBtns[PlayerPrefs.GetInt("lastClaimedDay")].SetActive(true);

                        readyReward[PlayerPrefs.GetInt("lastClaimedDay")].SetActive(true);

                        //ClaimedImgs[PlayerPrefs.GetInt("lastClaimedDay")].SetActive(true);
                    }
                    else
                    if (PlayerPrefs.GetInt("lastClaimedDay") == 6)
                    {
                        ClaimBtns[PlayerPrefs.GetInt("lastClaimedDay")].SetActive(true);

                        readyReward[PlayerPrefs.GetInt("lastClaimedDay")].SetActive(true);

                        // ClaimedImgs[PlayerPrefs.GetInt("lastClaimedDay")].SetActive(true);
                    }

                    PlayerPrefs.SetInt("clicked", 1);
                }
            }
            else
            {

            }
        }
    }
    private void ClaimedRewardDays()
    {
        currentDate = System.DateTime.Now;
        System.TimeSpan TimeDiff = currentDate.Subtract(oldDate);
        if (PlayerPrefs.GetInt("lastClaimedDay") < 6)
        {
            if (TimeDiff.Days >= 1 || TimeDiff.Hours >= 24/*TimeDiff.Seconds >= 5*/)
            {

                if (PlayerPrefs.GetInt("lastClaimedDay") < 6)
                {
                    ClaimBtns[PlayerPrefs.GetInt("lastClaimedDay")].SetActive(true);
                    readyReward[PlayerPrefs.GetInt("lastClaimedDay")].SetActive(true);
                    rewardPanel.enabled = (true);
                    timeText.enabled = false;
                }
         
            }
        }
    }
    //void OnApplicationQuit()
    //{

    //    PlayerPrefs.SetString("LeaveTimeData", System.DateTime.Now.ToBinary().ToString());

    //}

    private void FixedUpdate()
    {
        Check();
    }

    private void ClaimedCheck(int x)
    {

        oldDate = System.DateTime.Now;
        // ClaimBtns[x].GetComponent<Button>().interactable = false;   
        ClaimBtns[x].SetActive(false);
    }


    private void DisplayTime(float timeToDisplay)
    {

        //timeToDisplay += 1;
        float hours = Mathf.FloorToInt((timeToDisplay / 3600) % 48);
        float minutes = Mathf.FloorToInt((timeToDisplay / 60) % 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);


        string time = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");

        timeText.text = time;
        // timeText2.text = time;
    }



    public void ClaimReward(int i)
    {
        PlayerPrefs.SetInt("Day" + i, 1);

        readyReward[i].SetActive(false);

        Claimed[i].SetActive(true);

        PlayerPrefs.SetInt("clicked", 0);
        LastClaimed = System.DateTime.Now;
        PlayerPrefs.SetString("LastClaimed", LastClaimed.ToString());
        timeText.enabled = true;
        timeRemaining = 10;

        if (PlayerPrefs.GetInt("lastClaimedDay") >= 6)
        {
            PlayerPrefs.SetInt("lastClaimedDay", 0);
            for (int j = 0; j <= 6; j++)
            {
                PlayerPrefs.SetInt("Day" + j, 0);
            }
        }
        else
        {
            PlayerPrefs.SetInt("lastClaimedDay", i);
        }

        ClaimedCheck(i);

        congratesPanel.SetActive(true);

        switch (i)
        {
            case 0:
                GR_SaveData.instance.Coins += 100;
                GR_CashUpdate.instance.updateCash();
                if (GR_SoundManager.instance)
                    GR_SoundManager.instance.onButtonClickSound(GR_SoundManager.instance.buttonMainSound);
                break;
            case 1:
                GR_SaveData.instance.Coins += 300;
                GR_CashUpdate.instance.updateCash();
                if (GR_SoundManager.instance)
                    GR_SoundManager.instance.onButtonClickSound(GR_SoundManager.instance.buttonMainSound);

                break;
            case 2:
                GR_SaveData.instance.Coins += 500;
                GR_CashUpdate.instance.updateCash();
                if (GR_SoundManager.instance)
                    GR_SoundManager.instance.onButtonClickSound(GR_SoundManager.instance.buttonMainSound);
                break;
            case 3:
                GR_SaveData.instance.Coins += 800;
                GR_CashUpdate.instance.updateCash();
                if (GR_SoundManager.instance)
                    GR_SoundManager.instance.onButtonClickSound(GR_SoundManager.instance.buttonMainSound);
                break;
            case 4:
                GR_SaveData.instance.Coins += 1200;
                GR_CashUpdate.instance.updateCash();
                if (GR_SoundManager.instance)
                    GR_SoundManager.instance.onButtonClickSound(GR_SoundManager.instance.buttonMainSound);
                break;
            case 5:
                GR_SaveData.instance.Coins += 1500;
                GR_CashUpdate.instance.updateCash();
                if (GR_SoundManager.instance)
                    GR_SoundManager.instance.onButtonClickSound(GR_SoundManager.instance.buttonMainSound);
                break;
            case 6:
                GR_SaveData.instance.Coins += 2000;
                GR_CashUpdate.instance.updateCash();
                if (GR_SoundManager.instance)
                    GR_SoundManager.instance.onButtonClickSound(GR_SoundManager.instance.buttonMainSound);
                break;
        }

        StartCoroutine(delay());
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(2f);
        congratesPanel.SetActive(false);
    }
}
