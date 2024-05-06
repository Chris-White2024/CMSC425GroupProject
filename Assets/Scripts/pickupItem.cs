using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    GameObject ompo; // Reference to the player GameObject
    ThirdPersonMovement ompoMovement; // Reference to the ThirdPersonMovement component
    Vector3 offSet; // Offset for positioning the picked up item
    bool hasGlider = false; // Flag indicating if the player has a glider
    Vector3 originalPosition; // Original position of the item
    Quaternion originalRotation; // Original rotation of the item

    void Start()
    {
        // Finding and assigning the player GameObject with the "Player" tag
        ompo = GameObject.FindWithTag("Player");
        // Getting the ThirdPersonMovement component attached to the player
        ompoMovement = ompo.GetComponent<ThirdPersonMovement>();
        // Storing the original position and rotation of the item
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void OnTriggerEnter(Collider other)
    {
        // Setting the parent of the item to the player GameObject
        this.transform.parent = ompo.transform;
        
        // Checking if the item is a glider
        if (this.name.Contains("Glider"))
        {
            // Setting the flag to indicate the player has a glider
            hasGlider = true;
            // Setting offset and rotation for the glider
            offSet = new Vector3(0f, -1.2f, 0f);
            transform.localRotation = Quaternion.identity;
        }
        
        // Checking if the item is boots
        if (this.name.Contains("Boots"))
        {
            // Setting the flag to indicate the player has boots
            ompoMovement.hasBoots = true;
            // Setting offset and rotation for the boots
            offSet = new Vector3(.4f, -2.55f, .5f);
            transform.localRotation = Quaternion.Euler(0f, 180.0f, 0.0f);
        }
        // Setting the position of the item relative to the player with the calculated offset
        transform.localPosition = Vector3.zero + offSet;
    }

    void Update()
    {
        // Check for key press to drop the item
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItem();
            // Iterate over items to remove
            foreach (GameObject obj in GetItemsToRemove())
            {
                // If the item is currently parented to the player, detach it
                if (obj.transform.parent == ompo.transform)
                {
                    transform.parent = null;
                    // Reset the item's position and rotation to original values
                    transform.position = originalPosition;
                    transform.rotation = originalRotation;
                }
            }
        }
        // Apply glider gravity effect
        GliderGravity();
    }

    // Method to apply glider gravity effect
    void GliderGravity()
    {
        // If the player has a glider and is falling
        if (ompoMovement.velocity.y != -2 && ompoMovement.velocity.y < -1 && hasGlider)
        {
            // Reduce falling speed when using a glider
            ompoMovement.velocity.y *= .95f;
        }
    }

    // Method to drop the item
    void DropItem()
    {
        // Reset flags indicating if the player has a glider or boots
        hasGlider = false;
        ompoMovement.hasBoots = false;
    }

    // Method to get a list of items to remove
    List<GameObject> GetItemsToRemove()
    {
        // Find all GameObjects in the scene
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        // List to store GameObjects with PickupItem component
        List<GameObject> gameObjectsWithScript = new List<GameObject>();

        // Iterate over all GameObjects
        foreach (GameObject obj in allObjects)
        {
            // If the GameObject has a PickupItem component, add it to the list
            if (obj.GetComponent<PickupItem>() != null)
            {
                gameObjectsWithScript.Add(obj);
            }
        }
        // Return the list of GameObjects with PickupItem component
        return gameObjectsWithScript;
    }
}
