using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseSelector : MonoBehaviour
{
    public GameObject[] house;
    private void Start()
    {
        for (int i = 0; i < GR_GameController.instance.Houses.Length; i++)
        {
            if (i == GR_SaveData.instance.finalhouse)
            {
                house[i].SetActive(true);
              
            }
               
        }
    }
}
