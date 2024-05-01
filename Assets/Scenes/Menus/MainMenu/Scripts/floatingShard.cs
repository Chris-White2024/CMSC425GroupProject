using System;
using UnityEngine;

public class floatingShard : MonoBehaviour
{

    public int bobSpeedMin = 1;
    public int bobSpeedMax = 10;
    public int bobDistanceMin = 5;
    public int bobDistanceMax = 10;
    private float initialY;
    private int bobSpeed;
    private int bobDistance;

    private float direction;

    private float initialDirection;


    private System.Random random = new System.Random();

    private Boolean lowered = false;

    void Start() 
    {

        // set the speed and distance at which the object will bob
        bobSpeed = random.Next(bobSpeedMin, bobSpeedMax);
        bobDistance = random.Next(bobDistanceMin, bobDistanceMax);

        direction = (random.Next(-1,0) < 0)? 1 : -1;

        initialDirection = direction;

        initialY = transform.position.y;
    }

    void Update()
    {
        if (Math.Abs(transform.position.y - initialY) >= bobDistance) {
            direction *= -1;
        }

        if (Math.Abs(Math.Abs(transform.position.y - initialY) - bobDistance) <= .5 && !lowered) {
            direction *= .5f;
            lowered = true;
        } else if (Math.Abs(Math.Abs(transform.position.y - initialY) - bobDistance) >= .5) {
            direction = 1 * (1/direction) * .5f;
            lowered = false;
        }

        transform.Translate(0f, direction * bobSpeed * Time.deltaTime, 0f);

        transform.Rotate(-Vector3.forward * random.Next(1,15) * initialDirection * Time.deltaTime);
    }
}
