using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteorPrefab;  // Assign your meteor prefab in the Inspector
    public float dropInterval = 5f;  // Time between each meteor drop
    public float dropHeight = 0f;    // Height from which the meteors drop
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DropMeteors());  // Start the meteor dropping coroutine
    }

    public IEnumerator DropMeteors()
    {
        yield return new WaitForSeconds(5f);

        while (true)  // Infinite loop for continuous dropping
        {
            // Instantiate a meteor at a random position along the x-axis
            Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), dropHeight, 0);  // Adjust range as needed
            Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(dropInterval);  // Wait for the specified interval before dropping the next meteor
        }
    }


}
