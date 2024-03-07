using System.Collections;
using UnityEngine;
using System.Collections.Generic;


public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    Vector3 velocity;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = .1f;
    float turnSmoothVelocity;
    float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    public float jumpHeight = 5.0f;
    public GameObject playerBody;
    public GameObject playerHead;
    public float blinkDistance = 5.0f;

    //Make a queue to store the colors so its first in first out
    List<string> colorStack = new List<string>();    


    bool isGrounded;
    bool hasUmbrella = false;

    // Update is called once per frame
    void Update()
    {
        //When player presses e, pop the color from the stack and change the color of the player
        if (Input.GetKeyDown(KeyCode.E)){
            popColor();
        }
        if(Input.GetKeyDown(KeyCode.Q)){
            popFromEnd();
        }
        if(velocity.y != -2 && velocity.y < 0 && hasUmbrella){
            gravity = -2.5f;

        }
        else{
            gravity = -9.81f;
        }
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
        if(Input.GetButtonDown("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }
    public void pushColor(string color){
        //Push color to the front of the stack
        colorStack.Add(color);
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
    }
    void resetAbilities(){
        speed = 6f;
        jumpHeight = 5.0f;
    }
    void popFromEnd(){
        resetAbilities();
        if (colorStack.Count == 0){
            playerBody.GetComponent<Renderer>().material.color = Color.white;
            playerHead.GetComponent<Renderer>().material.color = Color.white;
            return;
        }
        string color = colorStack[colorStack.Count - 1]; // Accessing last element using index notation
        colorStack.RemoveAt(colorStack.Count - 1);
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
    }
    public void pickUpGlider(){
        hasUmbrella = true;
    }
}
