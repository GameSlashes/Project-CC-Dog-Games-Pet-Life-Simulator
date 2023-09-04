using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WaypointsGizmo : MonoBehaviour
{
    void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, .5f);
        Gizmos.DrawWireCube(transform.position, this.transform.localScale);
        //Handles.Label(transform.position, "");
#endif
    }
}
