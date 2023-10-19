using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static System.Net.WebRequestMethods;

public class OntrigerDoorOpen : MonoBehaviour
{
    public static OntrigerDoorOpen Instance;
    public DOTweenAnimation animdoor_1;
    public DOTweenAnimation animdoor_2;
    public BoxCollider enb;
    public GameObject btn_sound;
    private void Awake()
    {
        enb = gameObject.GetComponent<BoxCollider>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Instance = this;
            GR_GameController.instance.TapBtn.SetActive(true);
        }
        if (other.gameObject.tag == "Animal")
        {
            GR_GameController.instance.TapBtn.SetActive(true);
            Instance = this;
        }
        GR_GameController.instance.ATP = gameObject.name;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GR_GameController.instance.TapBtn.SetActive(false);
        }
        if (other.gameObject.tag == "Animal")
        {
            GR_GameController.instance.TapBtn.SetActive(false);
        }
    }
}
