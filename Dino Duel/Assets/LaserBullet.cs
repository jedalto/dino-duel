using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Dino"))  // when hits Dino plyer --> presumabley friendly fire?
        {
            // TODO:: subtract health from the player or trigger another event
            Debug.Log("Hit player!");

            // Eventually, can reduce health --> something like:
            // PlayerHealth playerHealth = hitInfo.GetComponent<PlayerHealth>();
            // playerHealth.TakeDamage(damageAmount);

            Destroy(gameObject); // Destroy the bullet after impact
        }
    }
}
