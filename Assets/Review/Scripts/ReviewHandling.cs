using System.Collections;
using System.Collections.Generic;
using Google.Play.Review;
using UnityEngine;
using UnityEngine.UI;

public class ReviewHandling : MonoBehaviour
{
    public GameObject Hand, ReviewPannel;
    public GameObject ReviewObject;
    public GameObject[] AllStars;

    public void FillStars(float fillAmount)
    {
        for (int i = 0; i < fillAmount * 5; i++)
        {
            AllStars[i].SetActive(true);
        }

        if (fillAmount >= 0.8f)
        {
            ReviewObject.SetActive(true);
            PlayerPrefs.SetInt("RateUsStatus", 5);
            StartCoroutine(delay());
        }
        else
        {
            LetterClick();
            StartCoroutine(delay());
        }
        Hand.SetActive(false);
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.5f);
        ReviewPannel.SetActive(false);
    }
   
    public void LetterClick()
    {

        PlayerPrefs.SetInt("RateUsStatus", 5);

        if (GR_SoundManager.instance)
            GR_SoundManager.instance.onButtonClickSound(GR_SoundManager.instance.buttonMainSound);
        
    }
}
