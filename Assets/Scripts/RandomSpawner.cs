using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RandomSpawner : MonoBehaviour
{
    public GameObject cubePreFab;
    public GameObject plane;
    public int maxObject = 14;
    public float floatHeight = 1f;
    private Vector3 planeSize;
    public Color[] allColors;

    public TextMeshProUGUI targetColorText; // Reference to the TextMeshProUGUI component
    public GameObject panel;
    void Start()
    {
        Collider planeCollider = plane.GetComponent<Collider>();
        Color targetColor = allColors[Random.Range(0, allColors.Length)];

        Color[] incorrectColors = new Color[allColors.Length - 1];
        int incorrectIndex = 0;

        for (int i = 0; i < allColors.Length; i++)
        {
            if (allColors[i] != targetColor)
            {
                incorrectColors[incorrectIndex] = allColors[i];
                incorrectIndex++;
            }
        }

        if (planeCollider == null)
        {
            Debug.LogError("Plane does not have a collider.");
            return;
        }

        planeSize = planeCollider.bounds.size;

        int targetColorCubeCount = 0;

        for (int i = 0; i < maxObject; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(plane.transform.position.x - (planeSize.x / 2) + 1, plane.transform.position.x + (planeSize.x / 2) - 1),
                planeCollider.bounds.min.y + floatHeight,
                Random.Range(plane.transform.position.z - (planeSize.z / 2) + 1, plane.transform.position.z + (planeSize.z / 2) - 1)
            );

            GameObject spawnedObject = Instantiate(cubePreFab, randomPosition, Quaternion.identity);

            // Ensure at least three cubes are spawned with the target color
            if (targetColorCubeCount < maxObject/2)
            {
                spawnedObject.GetComponent<Renderer>().material.color = targetColor;
                targetColorCubeCount++;
            }
            else
            {
                Color incorrectColor = incorrectColors[Random.Range(0, incorrectColors.Length)];
                spawnedObject.GetComponent<Renderer>().material.color = incorrectColor;
            }
        }

        if (targetColorText != null)
        {
            
            targetColorText.text =  UnityEngine.ColorUtility.ToHtmlStringRGBA(targetColor);
            ChangeColor(targetColor);

        }
        else
        {
            Debug.LogError("Target Color Text is not assigned.");
        }
    }

    void ChangeColor(Color color)
    {
        if (panel != null)
        {
            // Ensure the panel is active
            panel.SetActive(true);

            // Ensure the color has full opacity (alpha = 1)
            color.a = 1f;

            // Get the Image component (ensure it exists)
            Image panelImage = panel.GetComponent<Image>();

            if (panelImage != null)
            {
                panelImage.color = color;  // Change the color of the panel
                Debug.Log("Panel color changed to: " + color);
            }
            else
            {
                Debug.LogError("The panel does not have an Image component attached.");
            }
        }
        else
        {
            Debug.LogError("Panel is not assigned.");
        }
    }


}
