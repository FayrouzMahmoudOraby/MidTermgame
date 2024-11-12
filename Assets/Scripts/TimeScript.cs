using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject panel;
    [SerializeField] float remainingTime; 
    private PlayerController playerController; 
    
    void Start()
    {
        panel.SetActive(false);
        playerController = FindObjectOfType<PlayerController>(); 
    }
 
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime; 
            if (remainingTime <= 0) // When time runs out
            {
                remainingTime = 0;
                panel.SetActive(true);

                // If the playerController is found, set PlayerSpeed to 0
                if (playerController != null)
                {
                    playerController.PlayerSpeed = 0f; // Stop player movement
                    Debug.Log("Player speed has been set to 0.");
                }
            }

            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}