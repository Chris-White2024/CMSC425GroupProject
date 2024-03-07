using UnityEngine;

public class movePlatform : MonoBehaviour
{
    public Transform platformToMove; // Reference to the platform to move
    public float moveSpeed = 2f; // Speed of movement
    public float oscillationFrequency = 1f; // Frequency of oscillation
    public float maxDistance = 5f; // Max distance to move away from the original position

    private Vector3 originalPosition; // Original position of the platform

    void Start()
    {
        // Store the original position of the platform
        originalPosition = platformToMove.position;
    }

    void Update()
    {
        // Calculate the horizontal offset based on sine function
        float offsetX = Mathf.Sin(Time.time * oscillationFrequency) * maxDistance;

        // Calculate the target position for the platform
        Vector3 targetPosition = originalPosition + Vector3.back * offsetX;

        // Move the platform towards the target position
        platformToMove.position = Vector3.MoveTowards(platformToMove.position, targetPosition, moveSpeed * Time.deltaTime);
        
    }
    
}
