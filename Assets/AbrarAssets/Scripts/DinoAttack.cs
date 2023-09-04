using UnityEngine;

public class DinoAttack : MonoBehaviour
{
    public void ontrigerUp()
    {

        //GR_GameController.instance.action[0].SetActive(true);
        //GR_GameController.instance.action[1].SetActive(true);
        //GR_GameController.instance.action[2].SetActive(true);
    }
    public void ontrigerDown()
    {

        //GR_GameController.instance.action[0].SetActive(false);
        //GR_GameController.instance.action[1].SetActive(false);
        //GR_GameController.instance.action[2].SetActive(false);
    }
    public void ontrigerDown1()
    {
        //GR_GameController.instance.action[0].SetActive(false);
        //GR_GameController.instance.action[1].SetActive(false);
        //GR_GameController.instance.action[2].SetActive(false);
    }
    public void playBtnSound()
    {
        if (GR_SoundManager.instance)
            GR_SoundManager.instance.onButtonClickSound(GR_SoundManager.instance.LevelComplete);
    }
}
