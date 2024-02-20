using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPowerUp : MonoBehaviour
{
    public GameObject pickupEffect;
    public GameObject playerBody;
    public GameObject playerHead;
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            pickup(other);
        }
    }

    void pickup(Collider player){
        //Spawn a cool effect
        Instantiate(pickupEffect, transform.position, transform.rotation);

        playerBody.GetComponent<Renderer>().material.color = Color.green;
        playerHead.GetComponent<Renderer>().material.color = Color.green;
        //Remove Object
        Destroy(gameObject);
        pickupEffect.GetComponent<Renderer>().enabled = false;
        return;
    }
}
