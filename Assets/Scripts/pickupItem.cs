using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupItem : MonoBehaviour
{
    GameObject ompo;

    // Start is called before the first frame update
    void Start()
    {
        ompo = GameObject.Find("Third Person Player 1");
    }

    void OnTriggerEnter(Collider other){
        this.transform.parent = ompo.transform;
        ThirdPersonMovement playerMaterials = ompo.GetComponent<ThirdPersonMovement>();
        playerMaterials.pickUpUmbrella();
    }

}
