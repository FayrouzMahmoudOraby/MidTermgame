using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private float movementX;
    private float movementY;
    public float PlayerSpeed = 5;
    public TextMeshProUGUI countText;
    public float jumpForce = 5;
    private Rigidbody rb;
    private int count;
    private bool isGrounded = true; 

    
    public GameObject winTextObject;
    public TextMeshProUGUI targetColorText;
    public TextMeshProUGUI choice;

    public AudioSource source;
    public AudioSource source2;
    public AudioClip clip;
    public AudioClip clip2;

    public GameObject PowerUpSpeed;
    public GameObject ExtraTime;
    private TimeScript timeScript;

    

public Color[] allColors = new Color[] {};

    void Start()
    {
        if (targetColorText != null)
        {
            Color targetColor = allColors[Random.Range(0, allColors.Length)];
            
            // Set the target color in TextMeshPro with the correct format
            targetColorText.text = UnityEngine.ColorUtility.ToHtmlStringRGBA(targetColor);
            Debug.Log("Target color in Hex: " + targetColorText.text);  // Debugging to check the format
        }


        
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);

        
    }


    void Update()
    {
        movementX = Input.GetAxis("Horizontal");
        movementY = Input.GetAxis("Vertical");
        

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; 
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0, movementY);
        rb.AddForce(movement * PlayerSpeed);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Correct"))
        {
            string colorString = targetColorText.text;
            Debug.Log("Color string from targetColorText: " + colorString);

            Color targetColor;
            if (UnityEngine.ColorUtility.TryParseHtmlString("#" + colorString, out targetColor))
            {
                Renderer objectRenderer = other.gameObject.GetComponent<Renderer>();
                if (objectRenderer != null)
                {
                    Color collidedColor = objectRenderer.material.color;
                    string collidedColorHex = UnityEngine.ColorUtility.ToHtmlStringRGBA(collidedColor); 
                    Debug.Log("Collided object color in Hex: #" + collidedColorHex);
                    if (collidedColorHex.Equals(colorString, System.StringComparison.OrdinalIgnoreCase))
                    {
                        count += 1; 
                        source.PlayOneShot(clip);
                        choice.text = "";
                        Debug.Log("Correct color matched. Incrementing count.");
                    }
                    else
                    {
                        count -= 1; 
                        source2.PlayOneShot(clip2);
                        choice.text = "Wrong Choice";
                        Debug.Log("Incorrect color. Decrementing count.");
                    }
                }
                else
                {
                    Debug.LogError("No Renderer found on collided object.");
                }
                other.gameObject.SetActive(false);
                SetCountText(); 
            }
            
            {
            Debug.LogError("Invalid color string in targetColorText: " + colorString);
            }
        }
            
        else if (other.gameObject.CompareTag("Speed")) 
       {
            other.gameObject.SetActive(false);
            PlayerSpeed*=2;

       }

        
    }





    void SetCountText()
    {
        countText.text = "Count: " + count; // Display the count (assuming countText is a TextMeshProUGUI component)
        Debug.Log("Current Count: " + count);
    }



    

    private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Ground"))
    {
        isGrounded = true;
    }
}

private void OnCollisionExit(Collision collision)
{
    if (collision.gameObject.CompareTag("Ground"))
    {
        isGrounded = false;
    }
}


}