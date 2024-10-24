using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public GameObject explosion;
    public float explosionDuration = 1f;
    
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
            health.TakeDamage(50);

            Destroy(gameObject);// Destroy fire boulder object
            
            Instantiate(explosion, transform.position, Quaternion.identity); //change fire boulder into explosion animation
            Destroy(explosion); // Destroy the explosion object after impact
           
        }

        if(collision.CompareTag("Rock")){ // when fire boulder hits rock
            
            Destroy(gameObject);// Destroy fire boulder object
            
            Instantiate(explosion, transform.position, Quaternion.identity); //change fire boulder into explosion animation
            Destroy(explosion); // Destroy the explosion object after impact
            
        }

        if(collision.CompareTag("Ground")){ // when bullet hits rock
            
            Destroy(gameObject);// Destroy fire boulder object
            
            Instantiate(explosion, transform.position, Quaternion.identity); //change fire boulder into explosion animation
            Destroy(explosion); // Destroy the explosion object after impact
        }

        if(collision.CompareTag("Platform")){ // when bullet hits rock
            
            Destroy(gameObject);// Destroy fire boulder object
            
            Instantiate(explosion, transform.position, Quaternion.identity); //change fire boulder into explosion animation
            Destroy(explosion); // Destroy the explosion object after impact
        }
    }  
}

