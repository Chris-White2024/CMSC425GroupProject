using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHandler : MonoBehaviour
{
    ColorIndicator colorOnScreen; // Reference to the ColorIndicator component
    public List<string> colorStack = new List<string>(); // List to store colors
    public GameObject playerColor; // Reference to the GameObject representing player color
    ThirdPersonMovement ompoMovement; // Reference to the ThirdPersonMovement component

    void Start()
    {
        // Finding and assigning the ColorIndicator component on the Canvas GameObject
        colorOnScreen = GameObject.Find("Canvas").GetComponent<ColorIndicator>();
        // Finding and assigning the ThirdPersonMovement component on the player GameObject
        ompoMovement = GameObject.Find("Third Person Player 1").GetComponent<ThirdPersonMovement>();
    }

    void Update()
    {
        // Check for key press to pop the top color from the stack
        if (Input.GetKeyDown(KeyCode.E))
        {
            PopColor();
        }
    }

    // Method to add a color to the stack
    public void PushColor(string color)
    {
        colorStack.Add(color); // Adding the color to the stack
        colorOnScreen.UpdateColorBlocks(colorStack); // Updating the UI to reflect the color stack
    }

    // Method to remove the top color from the stack
    public void PopColor()
    {
        ResetAbilities(); // Resetting player abilities before applying the popped color

        if (colorStack.Count == 0)
        {
            // If the stack is empty, set player color to orange
            Color orange = new Color(1.0f, 0.5f, 0.0f);
            SetPlayerColor(orange);
            return;
        }

        string color = colorStack[0]; // Get the color at the top of the stack
        colorStack.RemoveAt(0); // Remove the top color from the stack

        // Applying effects based on the popped color
        switch (color)
        {
            case "blue":
                SetPlayerColor(Color.blue);
                break;
            case "green":
                SetPlayerColor(Color.green);
                ompoMovement.jumpHeight = 10.0f; // Increase jump height
                break;
            case "red":
                SetPlayerColor(Color.red);
                ompoMovement.speed = 15f; // Increase movement speed
                break;
            case "yellow":
                SetPlayerColor(Color.yellow);
                ompoMovement.canWallRun = true; // Enable wall running
                break;
        }

        colorOnScreen.UpdateColorBlocks(colorStack); // Update the UI to reflect the updated color stack
    }

    // Method to clear all colors from the stack
    public void popAll(){
        colorStack.Clear(); // Clearing the color stack
        colorOnScreen.UpdateColorBlocks(colorStack); // Updating the UI to reflect the cleared stack
    }

    // Method to reset player abilities to default values
    void ResetAbilities()
    {
        ompoMovement.speed = 8f; // Reset movement speed
        ompoMovement.jumpHeight = 5f; // Reset jump height
        ompoMovement.canWallRun = false; // Disable wall running
        ompoMovement.isWallRunning = false; // Set wall running state to false
    }

    // Method to set the player's color
    void SetPlayerColor(Color color)
    {
        playerColor.GetComponent<Renderer>().material.color = color; // Setting the player's color
    }
}
