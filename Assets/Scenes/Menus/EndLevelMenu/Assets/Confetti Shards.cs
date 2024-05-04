using System;
using System.Collections;
using UnityEngine;

public class ShardExplosion : MonoBehaviour
{
    public Vector2 newPos = new Vector2(10f, 10f); // Adjust this value to control the force of the explosion

    public float addedWeight = 1f;

    public int bobSpeedMin = 1;
    public int bobSpeedMax = 10;
    public int bobDistanceMin = 5;
    public int bobDistanceMax = 10;
    private float initialY;
    private int bobSpeed;
    private int bobDistance;

    private float direction;

    private float initialDirection;

    private Boolean startBobbing;


    private System.Random random = new System.Random();

    private Boolean lowered = false;


    void Start()
    {
        Explode();
        // set the speed and distance at which the object will bob
        bobSpeed = random.Next(bobSpeedMin, bobSpeedMax);
        bobDistance = random.Next(bobDistanceMin, bobDistanceMax);

        direction = (random.Next(-1,0) < 0)? 1 : -1;

        initialDirection = direction;

        initialY = transform.position.y;

        startBobbing = false;

        StartCoroutine(Bob());
    }


    void Explode()
    {        
        StartCoroutine(MoveShard());
    }

    IEnumerator Bob() {
        while (true) { 
            if (startBobbing) {
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

                transform.Rotate(-Vector3.forward * 30 * initialDirection * Time.deltaTime);
            }

            yield return null;
        }
    }

    IEnumerator MoveShard()
    {
        float weight = 1;

        while (Math.Abs(transform.localPosition.x - newPos.x) > .5) {
            weight = .1f * Vector2.Distance(transform.localPosition, newPos);

            transform.Translate(newPos * weight * addedWeight * Time.deltaTime);

            yield return null;
        }
        
        if (Math.Abs(transform.localPosition.x - newPos.x) < 2) {
            startBobbing = true;
        }

    }

}
