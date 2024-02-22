using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinkPowerUp : MonoBehaviour
{
    public GameObject pickupEffect;

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            pickup(other);
        }
    }

    void pickup(Collider player){
        //Spawn a cool effect
        Instantiate(pickupEffect, transform.position, transform.rotation);
        ThirdPersonMovement playerMaterials = player.GetComponent<ThirdPersonMovement>();
        playerMaterials.playerBody.GetComponent<Renderer>().material.color = Color.blue;
        playerMaterials.playerHead.GetComponent<Renderer>().material.color = Color.blue;
        //Remove Object
        Destroy(gameObject);
        pickupEffect.GetComponent<Renderer>().enabled = false;
        return;
    }
}