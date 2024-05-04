using UnityEngine;

public class FanBlow : MonoBehaviour
{
    public float blowForce = 10f;

    void OnTriggerStay(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            ThirdPersonMovement playerMovement = other.GetComponent<ThirdPersonMovement>();
            if (playerMovement != null)
            {
                // Get the direction the fan is facing
                Vector3 fanDirection = transform.forward.normalized;

                // Calculate the force vector to be applied
                Vector3 fanBlowForce = fanDirection * blowForce;

                // Apply the force to the player's position
                playerMovement.velocity += fanBlowForce * Time.deltaTime;
            }
        }
    }

}