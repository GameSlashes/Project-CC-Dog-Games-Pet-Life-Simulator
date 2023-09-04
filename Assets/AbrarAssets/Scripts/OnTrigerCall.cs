using System.Collections;
using UnityEngine;

public class OnTrigerCall : MonoBehaviour
{
    public static OnTrigerCall instance;
    public GameObject Player;
    public int EnemyCount = 0;
    public bool win = false;

    public int LevelCompleteMission;

    public void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (EnemyCount >= LevelCompleteMission && win)
        {
            win = false;
            Player.SetActive(false);
            StartCoroutine(Delay());

        }
        IEnumerator Delay()
        {
            yield return new WaitForSeconds(3f);
            if (GR_SoundManager.instance)
            {
                GR_SoundManager.instance.mainMenuSound.SetActive(true);
                GR_SoundManager.instance.gamePlaySound.SetActive(false);
            }
            GR_GameController.instance.levelCompleted();
        }
    }
}


