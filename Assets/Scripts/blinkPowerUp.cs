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
        //Grab instance of the color stack defined in ThirdPersonMovement.cs
        ThirdPersonMovement playerMaterials = player.GetComponent<ThirdPersonMovement>();
        //Push the color onto the stack
        playerMaterials.pushColor("blue");
        //Spawn a cool effect
        Instantiate(pickupEffect, transform.position, transform.rotation);
        //Remove Object
        GameObject tmp = gameObject;
        //Store transform of object
        Vector3 pos = tmp.transform.position;
        //Move object far away
        tmp.transform.position = new Vector3(0, -100, 0);
        //Wait 5 seconds
        StartCoroutine(Respawn(tmp, pos));
        return;
    }
    IEnumerator Respawn(GameObject obj, Vector3 pos){
        yield return new WaitForSeconds(5);
        //Move object back to original position
        obj.transform.position = pos;     
    }

}
