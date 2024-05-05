
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Vector3 velocity;
    public Transform cam;
    public Transform groundCheck;
    public LayerMask groundMask;

    public float speed = 10f;
    public float turnSmoothTime = .1f;
    float turnSmoothVelocity;
    public float gravity = -9.81f;
    public float groundDistance;
    public float jumpHeight = 5.0f;
    public float blinkDistance = 5.0f;
    public bool hasBoots = false;

    public bool isGrounded;
    public bool isWallRunning;
    public bool canWallRun = false;

    private AudioSource audioSource;
    public AudioClip jumpSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        MovementFromInput();
    }

    //Function to move the player based on input
    void MovementFromInput()
    {
        //Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isWallRunning = false;

        // Check if the player is touching a wall tagged with "WallRun"
        if (canWallRun && ((Physics.Raycast(transform.position, transform.right, out RaycastHit hitRight, 1.5f) && hitRight.collider.CompareTag("WallRun")) ||
            (Physics.Raycast(transform.position, -transform.right, out RaycastHit hitLeft, 1.5f) && hitLeft.collider.CompareTag("WallRun"))))
        {
            // Change the gravity direction to simulate walking on the wall
            isWallRunning = true;
        }

        //Get input from the player
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


        //Move the player based on input
        if (direction.magnitude >= .1f)
        {
            //Source for the following 4 lines of code: https://www.youtube.com/watch?v=4HpC--2iowE
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            // Apply wall running gravity if the player is wall running

            float verticalVelocity = isWallRunning ? 0 : velocity.y;
            controller.Move((moveDir * speed + Vector3.up * verticalVelocity) * Time.deltaTime);
        }

        // Apply regular gravity if not wall running
        if (isWallRunning && velocity.y < 0 && !isGrounded)
        {
            velocity.y += (1 / 4) * gravity * Time.deltaTime;
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

        //Check if the player wants to jump
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || isWallRunning)
            {
                audioSource.clip = jumpSound;
                audioSource.Play();
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }
        }

    }


}
