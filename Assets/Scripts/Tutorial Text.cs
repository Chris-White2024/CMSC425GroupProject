using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{
    public TextMeshProUGUI instructionsText;
    public float displayTime = 3f;
    public string message;

    private bool playerInRange;
    private float displayTimer;

    void Start()
    {
        instructionsText.enabled = false;
    }

    void Update()
    {
        if (playerInRange)
        {
            // Display instructions
            instructionsText.enabled = true;

            // Countdown timer
            displayTimer -= Time.deltaTime;
            if (displayTimer <= 0f)
            {
                instructionsText.enabled = false;
                playerInRange = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            displayTimer = displayTime;

            // Set the message
            instructionsText.text = message;
        }
    }

}
