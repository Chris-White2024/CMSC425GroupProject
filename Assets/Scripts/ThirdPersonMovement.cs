using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;


public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Vector3 velocity;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = .1f;
    float turnSmoothVelocity;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    public float jumpHeight = 5.0f;
    public GameObject playerBody;
    public GameObject playerHead;
    public float blinkDistance = 5.0f;
    ColorIndicator colorOnScreen;
    public bool hasDoubleJump;

    //Make a queue to store the colors so its first in first out
    List<string> colorStack = new List<string>();    


    public bool isGrounded;


    void Start(){
        colorOnScreen = GameObject.Find("Canvas").GetComponent<ColorIndicator>();
    }

    // Update is called once per frame
    void Update()
    {
        //When player presses e, pop the color from the stack and change the color of the player
        if (Input.GetKeyDown(KeyCode.E)){
            popColor();
        }
        movementFromInput();
    }
    public void pushColor(string color){

        //Push color to the front of the stack
        colorStack.Add(color);
        colorOnScreen.UpdateColorBlocks(colorStack);
    }
    public void popColor(){
        //Pop color from the end of the stack

        resetAbilities();

        if (colorStack.Count == 0){
            playerBody.GetComponent<Renderer>().material.color = Color.white;
            playerHead.GetComponent<Renderer>().material.color = Color.white;
            return;
        }
        //Using proper syntax with list types get the first element of the list
        string color = colorStack[0]; // Accessing first element using index notation
        colorStack.RemoveAt(0);
        if (color == "blue"){
            playerBody.GetComponent<Renderer>().material.color = Color.blue;
            playerHead.GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (color == "green"){
            playerBody.GetComponent<Renderer>().material.color = Color.green;
            playerHead.GetComponent<Renderer>().material.color = Color.green;
            jumpHeight = 10.0f;
        }
        else if (color == "red"){
            playerBody.GetComponent<Renderer>().material.color = Color.red;
            playerHead.GetComponent<Renderer>().material.color = Color.red;
            speed = 20f;
        }
        colorOnScreen.UpdateColorBlocks(colorStack);
    }
    void resetAbilities(){
        speed = 6f;
        jumpHeight = 5.0f;
    }
    public List<string> getColors(){
        return colorStack;
    }

    public float getMagnitude(){
        return Math.Abs(velocity.x + velocity.z);
    }
    void movementFromInput(){
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= .1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            controller.Move(moveDir * speed * Time.deltaTime);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }
        if(Input.GetButtonDown("Jump") && (isGrounded || hasDoubleJump)){
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }
}
