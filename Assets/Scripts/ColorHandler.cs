using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHandler : MonoBehaviour
{
    ColorIndicator colorOnScreen;
    List<string> colorStack = new List<string>();
    public GameObject playerColor;
    ThirdPersonMovement ompoMovement;


    void Start()
    {
        colorOnScreen = GameObject.Find("Canvas").GetComponent<ColorIndicator>();
        ompoMovement = GameObject.Find("Third Person Player 1").GetComponent<ThirdPersonMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PopColor();
        }
    }

    public void PushColor(string color)
    {
        colorStack.Add(color);
        colorOnScreen.UpdateColorBlocks(colorStack);
    }

    public void PopColor()
    {
        ResetAbilities();

        if (colorStack.Count == 0)
        {
            SetPlayerColor(Color.white);
            return;
        }

        string color = colorStack[0];
        colorStack.RemoveAt(0);

        switch (color)
        {
            case "blue":
                SetPlayerColor(Color.blue);
                break;
            case "green":
                SetPlayerColor(Color.green);
                ompoMovement.jumpHeight = 10.0f;
                break;
            case "red":
                SetPlayerColor(Color.red);
                ompoMovement.speed = 15f;
                break;
            case "yellow":
                SetPlayerColor(Color.yellow);
                ompoMovement.canWallRun = true;
                break;
        }

        colorOnScreen.UpdateColorBlocks(colorStack);
    }

    void ResetAbilities()
    {
        ompoMovement.speed = 8f;
        ompoMovement.jumpHeight = 5f;
        ompoMovement.canWallRun = false;
        ompoMovement.isWallRunning = false;
    }

    void SetPlayerColor(Color color)
    {
        playerColor.GetComponent<Renderer>().material.color = color;
    }
}
