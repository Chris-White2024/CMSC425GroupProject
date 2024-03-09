using System;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Vector3 velocity;
    public Transform cam;
    public Transform groundCheck;
    public LayerMask groundMask;
    public GameObject playerBody;
    public GameObject playerHead;

    public float speed = 6f;
    public float turnSmoothTime = .1f;
    float turnSmoothVelocity;
    public float gravity = -9.81f;
    public float groundDistance;
    public float jumpHeight = 5.0f;
    public float blinkDistance = 5.0f;

    public bool hasDoubleJump;
    public bool isGrounded;
    public bool isWallRunning;
    public bool canWallRun = false;

    ColorIndicator colorOnScreen;
    List<string> colorStack = new List<string>();

    void Start()
    {
        colorOnScreen = GameObject.Find("Canvas").GetComponent<ColorIndicator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PopColor();
        }
        MovementFromInput();
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
                jumpHeight = 10.0f;
                break;
            case "red":
                SetPlayerColor(Color.red);
                speed = 20f;
                break;
            case "yellow":
                SetPlayerColor(Color.yellow);
                canWallRun = true;
                break;
        }

        colorOnScreen.UpdateColorBlocks(colorStack);
    }

    void ResetAbilities()
    {
        speed = 6f;
        jumpHeight = 5.0f;
        canWallRun = false;
        isWallRunning = false;
    }

    void SetPlayerColor(Color color)
    {
        playerBody.GetComponent<Renderer>().material.color = color;
        playerHead.GetComponent<Renderer>().material.color = color;
    }

void MovementFromInput()
{
    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    isWallRunning = false;

    // Check if the player is touching a wall tagged with "WallRun"
    if (canWallRun && (Physics.Raycast(transform.position, transform.right, out RaycastHit hitRight, 1.5f) && hitRight.collider.CompareTag("WallRun")) ||
        (Physics.Raycast(transform.position, -transform.right, out RaycastHit hitLeft, 1.5f) && hitLeft.collider.CompareTag("WallRun")))
    {
        // Change the gravity direction to simulate walking on the wall
        isWallRunning = true;
    }

    float horizontal = Input.GetAxisRaw("Horizontal");
    float vertical = Input.GetAxisRaw("Vertical");
    Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

    if (direction.magnitude >= .1f)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        // Apply wall running gravity if the player is wall running
        float verticalVelocity = isWallRunning ? 0 : velocity.y;
        controller.Move((moveDir * speed + Vector3.up * verticalVelocity) * Time.deltaTime);
    }

    // Apply regular gravity if not wall running
    if (isWallRunning && velocity.y < 0 && !isGrounded){
        velocity.y += (1/4) * gravity * Time.deltaTime;
    }
    else
    {
        velocity.y += gravity * Time.deltaTime;
    }

    controller.Move(velocity * Time.deltaTime);

    if (isGrounded && velocity.y < 0)
    {
        velocity.y = -2f;
    }

    if (Input.GetButtonDown("Jump"))
    {
        if (isGrounded || hasDoubleJump || isWallRunning)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }
}

}
