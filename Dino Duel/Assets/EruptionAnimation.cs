using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EruptionAnimation : MonoBehaviour
{
    public float delay = 2f;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false; // Disable animator at start
        Invoke("PlayAnimation", delay);
    }

    void PlayAnimation()
    {
        animator.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
