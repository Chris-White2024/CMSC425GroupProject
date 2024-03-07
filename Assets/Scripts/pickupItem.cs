using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupItem : MonoBehaviour
{
    GameObject ompo;
    GameObject gliderPos;

    // Start is called before the first frame update
    void Start()
    {
        ompo = GameObject.Find("Third Person Player 1");
        gliderPos = GameObject.Find("GliderPos");
    }

    void OnTriggerEnter(Collider other){
        this.transform.parent = ompo.transform;
        // Match position and rotation with the collided object
        transform.localPosition = Vector3.zero;
        transform.localPosition += new Vector3 (-1.25f,.2f,-.5f);
        transform.localRotation = Quaternion.identity;
        ThirdPersonMovement moveScript = ompo.GetComponent<ThirdPersonMovement>();
        moveScript.pickUpGlider();
    }

}
