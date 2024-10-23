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

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dino"))  // when hits Dino plyer --> presumabley friendly fire?
        {
            Debug.Log("Hit player!");
            

            DinoHealth health = collision.GetComponent<DinoHealth>();  // Reference to DinoHealth script
            health.TakeDamage(10);
            //if (health != null)
            //{
            //    health.TakeDamage(10);  // Apply damage when hit by LaserBullet
            //}

            Destroy(gameObject); // Destroy the bullet after impact
        }

        if(collision.CompareTag("Rock")){ // when bullet hits rock
            Destroy(gameObject); //Destroy the bullet after impact
        }
    }  
}
