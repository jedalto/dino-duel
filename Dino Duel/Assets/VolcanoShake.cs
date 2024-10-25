using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoShake : MonoBehaviour
{
    public float shakeDuration = 1.0f; // Duration of the shake
    public float shakeMagnitude = 0.1f; // How much it shakes

    private Vector3 originalPosition;
    private float shakeTimeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.localPosition;
        shakeTimeRemaining = shakeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTimeRemaining > 0)
        {
            AscendingShaking();
        } else
        {
            DescendingShaking();
        }
    }

    // once a time remaining is set, update will run until remaining time goes to 0 again
    public void StartShaking()
    {
        shakeTimeRemaining = shakeDuration;
    }

    // shaking magnitude increases as time goes on
    public void AscendingShaking()
    {
        if (shakeTimeRemaining > 0)
        {
            // z,x = 0 to keep volcano only shaking up and down
            Vector3 randomOffset = Random.insideUnitSphere * shakeMagnitude * (1 / shakeTimeRemaining);
            randomOffset.z = 0;
            randomOffset.x = 0;

            transform.localPosition = originalPosition + randomOffset;
            shakeTimeRemaining -= Time.deltaTime;

            if (shakeTimeRemaining <= 0)
            {
                transform.localPosition = originalPosition; // Reset position after shaking
            }
        }
    }

    // shaking magnitude decreases as time goes on
    public void DescendingShaking()
    {
        if (shakeDuration > 0)
        {
            Vector3 randomOffset = Random.insideUnitSphere * shakeMagnitude * (shakeDuration);
            randomOffset.z = 0;
            randomOffset.x = 0;

            transform.localPosition = originalPosition + randomOffset;
            shakeDuration -= Time.deltaTime;

            if (shakeDuration <= 0)
            {
                transform.localPosition = originalPosition; // Reset position after shaking
            }
        }
    }
}
