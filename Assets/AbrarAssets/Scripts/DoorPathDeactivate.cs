using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPathDeactivate : MonoBehaviour
{
    public GameObject DoorPath_1;
    public GameObject DoorPath_2;
    public bool LevelCom;
    public bool PlayerActivation;
    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Animal")
        {
            if (PlayerActivation)
            {
                GR_GameController.instance.PuppyControler.SetActive(true);
                GR_GameController.instance.TapBtn.SetActive(false);
                GR_GameController.instance.CharacterController.SetActive(false);
                DoorPath_1.SetActive(false);
                DoorPath_2.SetActive(true);
            }
            else if(LevelCom)
            {
                DoorPath_1.SetActive(false);
                GR_GameController.instance.levelCompleted();
            }
            else if (DoorPath_1)
            {
                DoorPath_1.SetActive(false);
            }
            if(DoorPath_2)
            {
                DoorPath_2.SetActive(true);
            }
        }
        else if (other.gameObject.tag == "Player")
        {
            if (PlayerActivation)
            {
                GR_GameController.instance.PuppyControler.SetActive(true);
                GR_GameController.instance.CharacterController.SetActive(false);
                DoorPath_1.SetActive(false);
                DoorPath_2.SetActive(true);
            }
            else if (LevelCom)
            {
                DoorPath_1.SetActive(false);
                GR_GameController.instance.levelCompleted();
            }
            else if (DoorPath_1)
            {
                DoorPath_1.SetActive(false);
            }
            if (DoorPath_2)
            {
                DoorPath_2.SetActive(true);
            }
        }
    }
}
