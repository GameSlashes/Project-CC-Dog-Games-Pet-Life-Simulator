using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDirectionalMovement : MonoBehaviour
{
    public float Scrollx = .5f;
    public float Scrolly = .5f;
    void Update()
    {
        float offsetX = Time.time * Scrollx;
        float offsetY = Time.time * Scrolly;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector3(offsetX, offsetY);
    }
}
