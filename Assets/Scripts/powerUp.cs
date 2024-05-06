using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{
    // Called when a collider enters the trigger zone of the power-up
    void OnTriggerEnter(Collider other){
        // Check if the collider belongs to the player
        if (other.CompareTag("Player")){
            // Call the pickup method passing the player collider
            pickup(other);
        }
    }

    // Method to handle picking up the power-up
    void pickup(Collider player){

        string color = ""; // Variable to store the color of the power-up
        
        // Get the ColorHandler component attached to the player
        ColorHandler playerMaterials = player.GetComponent<ColorHandler>();

        // Check if the player has reached the maximum number of power-ups
        if (playerMaterials.colorStack.Count == 3){
            // Find the Instructions UI GameObject that is a child of the player
            GameObject canvas = player.transform.Find("Canvas").gameObject;
            GameObject instructions = canvas.transform.Find("Instructions").gameObject;
            // Get the text component of the instructions
            TMPro.TextMeshProUGUI text = instructions.GetComponent<TMPro.TextMeshProUGUI>();
            // Change the text to inform the player about reaching the maximum power-ups
            StartCoroutine(fullPowerup(text));
            return;
        }
        
        // Determine the color of the power-up based on its name
        if (this.name.Contains("Blink")){
            color = "blue";
        }
        else if (this.name.Contains("Speed")){
            color = "red";
        }
        else if (this.name.Contains("Jump")){
            color = "green";
        }
        else if (this.name.Contains("WallRunning")){
            color = "yellow";
        }

        // Push the color onto the stack of the ColorHandler component
        playerMaterials.PushColor(color);
        
        // Remove the power-up object from the scene
        GameObject tmp = gameObject;
        Vector3 pos = tmp.transform.position; // Store the transform position of the object
        tmp.transform.position = new Vector3(0, -100, 0); // Move the object far away
        StartCoroutine(Respawn(tmp, pos)); // Wait for 5 seconds and then respawn the object
        return;
    }

    // Coroutine to respawn the power-up object after a delay
    IEnumerator Respawn(GameObject obj, Vector3 pos){
        yield return new WaitForSeconds(5); // Wait for 5 seconds
        obj.transform.position = pos; // Move the object back to its original position    
    }

    // Coroutine to display a message when the player has reached the maximum number of power-ups
    IEnumerator fullPowerup(TMPro.TextMeshProUGUI text){
        yield return new WaitUntil(() => text.text == ""); // Wait until the text is empty
        text.text = "You have reached the maximum number of powerups"; // Set the text
        yield return new WaitForSeconds(5); // Wait for 5 seconds
        text.text = ""; // Clear the text
    }
}
