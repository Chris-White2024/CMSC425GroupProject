using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
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
            if (playerBody.GetComponent<Renderer>().material.color == Color.green){
                jumpHeight = 10f;
            }
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            if (playerBody.GetComponent<Renderer>().material.color == Color.green){
                jumpHeight = 2f;
                playerBody.GetComponent<Renderer>().material.color = Color.red;
                playerHead.GetComponent<Renderer>().material.color = Color.red;
            }
        }
        if(Input.GetKeyDown(KeyCode.B) && playerBody.GetComponent<Renderer>().material.color == Color.blue){
            //blink in direction of player head rotation
            Vector3 moveDir = playerHead.transform.forward;

            controller.Move(moveDir * blinkDistance);
            playerBody.GetComponent<Renderer>().material.color = Color.red;
            playerHead.GetComponent<Renderer>().material.color = Color.red;
        }
        //Checks if k key is pressed using unity library code for key presses


    }
}
