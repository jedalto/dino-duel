using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    private float explosionDuration;  // Duration for which the explosion exists

    void Start()
    {
        // Get the Meteor component from the parent GameObject
        Meteor meteor = FindObjectOfType<Meteor>();
        if (meteor != null)
        {
            explosionDuration = meteor.explosionDuration;  // Access explosionDuration from Meteor
        }
        else
        {
            explosionDuration = 1f;  // Fallback value
        }

        // Destroy the explosion game object after the explosionDuration time has passed
        Destroy(gameObject, explosionDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
