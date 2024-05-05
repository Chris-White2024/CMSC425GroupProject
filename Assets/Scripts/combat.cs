using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class combat : MonoBehaviour
{
    public int health = 5; // Current health of the character
    public ThirdPersonMovement playerMovement; // Reference to the ThirdPersonMovement component of the player
    public float pushForce = 5f; // Force applied when pushed
    public GameObject ompoMesh; // Reference to the mesh of the character

    void Start()
    {
        // Finding and assigning the ThirdPersonMovement component of the player
        playerMovement = GameObject.Find("Third Person Player 1").GetComponent<ThirdPersonMovement>();
    }

    // Method to decrease health when taking damage
    public void TakeDamage(int damage)
    {
        health -= damage; // Decrease health by the specified amount
        // If the character is the player, update the health display
        if (this.gameObject.tag == "Player"){
            this.GetComponent<displayHealth>().updateHealth();
        }
    }

    // Method to increase health when healing
    public void Heal(int heal)
    {
        health += heal; // Increase health by the specified amount
        // If the character is the player, update the health display
        if (this.gameObject.tag == "Player"){
            this.GetComponent<displayHealth>().updateHealth();
        }
    }

    // Called when the character collides with another object
    public void OnTriggerEnter(Collider other)
    {
        // If the collision is with an enemy, push back and take damage
        if (other.gameObject.tag == "Enemy")
        {
            pushBack(other);
            TakeDamage(1);
            StartCoroutine(flashMesh()); // Flash the character's mesh to indicate damage
        }
        // If the collision is with a hazard, push up and take damage
        else if (other.gameObject.tag == "Hazard")
        {
            pushUp();
            TakeDamage(2);
            StartCoroutine(flashMesh()); // Flash the character's mesh to indicate damage
        }
        // If the collision is with another player and the player has boots, take damage and push up
        else if (other.gameObject.tag == "Player" && playerMovement.hasBoots == true)
        {
            TakeDamage(1);
            pushUpOmpo(other);
            StartCoroutine(flashMesh()); // Flash the character's mesh to indicate damage
        }
    }

    // Method to push up the player
    public void pushUpOmpo(Collider other)
    {
        Vector3 upWards = other.transform.up.normalized;
        // Reset the player's vertical velocity to prevent falling back down
        other.GetComponent<ThirdPersonMovement>().velocity.y = 0f;
        other.GetComponent<ThirdPersonMovement>().controller.Move(upWards * pushForce);
    }

    // Method to push back the player or enemy
    public void pushBack(Collider other)
    {
        Vector3 pushDirection = other.transform.forward.normalized;
        // Angle slightly upwards to avoid pushing the character into the ground
        pushDirection.y = 0.5f;
        // Apply force in the opposite direction for a short time
        playerMovement.controller.Move(pushDirection * pushForce); 
    }

    // Method to push up the player
    public void pushUp()
    {
        Vector3 upWards = this.transform.up.normalized;
        // Apply upward force to the player
        playerMovement.controller.Move(upWards * pushForce); 
        playerMovement.velocity.y = 0f; // Reset vertical velocity to prevent falling back down
    }

    // Update is called once per frame
    void Update()
    {
        // If health drops to or below zero
        if (health <= 0)
        {
            // If the character is the player, load the death screen scene
            if (this.gameObject.tag == "Player"){
                SceneManager.LoadScene("deathScreen");
            }
            else{
                // Destroy the enemy and all parent objects
                Destroy(this.gameObject.transform.parent.gameObject);
            }
        }
    }

    // Coroutine to flash the character's mesh to indicate damage
    IEnumerator flashMesh(){
        MeshRenderer mesh = ompoMesh.GetComponent<MeshRenderer>(); // Get the mesh renderer of the character
        for (int i = 0; i < 5; i++)
        {
            mesh.enabled = false; // Disable the mesh renderer
            yield return new WaitForSeconds(0.1f); // Wait for a short time
            mesh.enabled = true; // Enable the mesh renderer
            yield return new WaitForSeconds(0.1f); // Wait for a short time
        }
    }
}
