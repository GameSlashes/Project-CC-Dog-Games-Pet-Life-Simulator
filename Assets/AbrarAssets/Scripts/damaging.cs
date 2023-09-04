using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Exploder.Utils;
using Exploder;

public class damaging : MonoBehaviour
{
    public static damaging instance;

    public void Awake()
    {
        instance = this;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ai")
        {
            ExploderSingleton.Instance.ExplodeObject(other.gameObject);
        }
    }
}
