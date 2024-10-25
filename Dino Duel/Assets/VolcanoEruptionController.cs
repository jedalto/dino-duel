using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoEruptionController : MonoBehaviour
{
    public Animator volcanoAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // Start the volcano eruption animation when the game begins
        volcanoAnimator.SetTrigger("StartEruption");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
