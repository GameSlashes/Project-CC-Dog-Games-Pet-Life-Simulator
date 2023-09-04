using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SplashPanel : MonoBehaviour
{
    [Header("Scene Selection")]
    public Scenes NextScene;

    //public GameObject adsScript;

    public Image filBar;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("changeScene");

        filBar.fillAmount = 0;
    }

    public void Update()
    {
        if (filBar.fillAmount < 1)
        {
            filBar.fillAmount += 0.3f * Time.deltaTime;
        }
    }

    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(3f);
        //adsScript.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(NextScene.ToString());
    }
}
