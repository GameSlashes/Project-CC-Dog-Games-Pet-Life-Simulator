using System.Collections;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
    public float time;

    public void OnEnable()
    {
        StartCoroutine(delay());
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }
}
