using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation_play : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void anim()
    {
        animator.SetBool("Dance", true);
        StartCoroutine(delay());
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(5f);
        animator.SetBool("Dance", false);
    }
}