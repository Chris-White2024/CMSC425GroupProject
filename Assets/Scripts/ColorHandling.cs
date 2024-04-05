using System.Collections.Generic;
using UnityEngine;

public class ColorHandling : MonoBehaviour
{
    ColorIndicator colorOnScreen;
    List<string> colorStack = new List<string>();
    public GameObject playerBody;
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
        ompoMovement.ResetAbilities();

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
                ompoMovement.jumpHeight = 10f;
                break;
            case "red":
                SetPlayerColor(Color.red);
                ompoMovement.speed = 15f;
                break;
            case "yellow":
                ompoMovement.canWallRun = true;
                SetPlayerColor(Color.yellow);
                break;
        }

        colorOnScreen.UpdateColorBlocks(colorStack);
    }

    void SetPlayerColor(Color color)
    {
        playerBody.GetComponent<Renderer>().material.color = color;
    }
}
