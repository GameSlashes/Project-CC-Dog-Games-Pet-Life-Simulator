using UnityEngine;

public class GR_SoundManager : MonoBehaviour
{
    public static GR_SoundManager instance;

    public AudioClip buttonMainSound;
    public AudioClip LevelComplete;
    public GameObject ballHit;
    public AudioSource soundSource;

    public GameObject mainMenuSound;
    public GameObject gamePlaySound;
    public GameObject LevelCompelete;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void Start()
    {
        playMainMenuSound();
    }

    public void onButtonClickSound(AudioClip Sound)
    {
        if(soundSource != null)
        {
            soundSource.PlayOneShot(Sound);
        }
    }

    public void playMainMenuSound()
    {
        mainMenuSound.SetActive(true);
        gamePlaySound.SetActive(false);
    }
    public void allSoundsOff()
    {
        gamePlaySound.SetActive(false);
        mainMenuSound.SetActive(false);
    }  
    public void LevelCompeleteSoundON()
    {
        LevelCompelete.SetActive(true);
    }   
    public void LevelCompeleteSoundOff()
    {
        LevelCompelete.SetActive(false);
    }
    public void playGamePlaySound()
    {
        mainMenuSound.SetActive(false);
        gamePlaySound.SetActive(true);
    }
}
