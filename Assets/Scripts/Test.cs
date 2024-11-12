using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject prefab; // Reference to the prefab
    public Vector2 spawnArea; // Define the area to spawn the prefab
    public Color[] possibleTargetColors; // Array of possible target colors
    public Color[] incorrectColors; // Array of incorrect colors

    void Start()
    {
        // Spawn multiple prefabs and set one with a random target color
        int numberOfPrefabs = 5; // You can change this value to how many prefabs you want to spawn
        SpawnPrefabs(numberOfPrefabs);
    }

    void SpawnPrefabs(int numberOfPrefabs)
    {
        // Randomly select a target color from the possible target colors array
        Color targetColor = possibleTargetColors[Random.Range(0, possibleTargetColors.Length)];

        for (int i = 0; i < numberOfPrefabs; i++)
        {
            // Generate random position within the spawn area
            Vector3 randomPosition = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), Random.Range(-spawnArea.y, spawnArea.y), 0);

            // Instantiate the prefab at the random position
            GameObject spawnedObject = Instantiate(prefab, randomPosition, Quaternion.identity);

            // Randomly determine if this prefab gets the target color or an incorrect color
            if (i == 0) // Ensure only one prefab gets the target color (or randomize which one gets it)
            {
                spawnedObject.GetComponent<Renderer>().material.color = targetColor;
            }
            else
            {
                // Randomly pick one of the incorrect colors
                Color incorrectColor = incorrectColors[Random.Range(0, incorrectColors.Length)];
                spawnedObject.GetComponent<Renderer>().material.color = incorrectColor;
            }
        }
    }
}
