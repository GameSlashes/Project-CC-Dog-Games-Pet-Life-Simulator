using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TigerBreathingTimer : MonoBehaviour
{
    public GameObject image;
    public GameObject ImageFiller;
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Animal")
        {
            image.SetActive(true);
            ImageFiller.GetComponent<Image>().fillAmount += 0.0001f;
            if (ImageFiller.GetComponent<Image>().fillAmount >= 1)
            {
                GR_GameController.instance.levelfail();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Animal")
        {
            image.SetActive(false);
            ImageFiller.GetComponent<Image>().fillAmount = 0f;
        }
    }
}
