using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class powerUp : MonoBehaviour
{
    public GameObject pickupEffect;

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            pickup(other);
        }
    }

    void pickup(Collider player){
        string color = "";
        //Grab instance of the color stack defined in ThirdPersonMovement.cs
        ThirdPersonMovement playerMaterials = player.GetComponent<ThirdPersonMovement>();
        //Push the color onto the stack
        
        if (this.name == "Blink"){
            color = "blue";
        }
        else if (this.name == "Speed"){
            color = "red";
        }
        else if (this.name == "Jump"){
            color = "green";
        }
        //Spawn a cool effect
        playerMaterials.pushColor(color);
        GameObject tmp2 = Instantiate(pickupEffect, transform.position, transform.rotation);
        
        //Remove Object
        GameObject tmp = gameObject;
        //Store transform of object
        Vector3 pos = tmp.transform.position;
        //Move object far away
        tmp.transform.position = new Vector3(0, -100, 0);
        //Wait 5 seconds
        StartCoroutine(Respawn(tmp, pos, tmp2));
        return;
    }
    IEnumerator Respawn(GameObject obj, Vector3 pos, GameObject particles){
        yield return new WaitForSeconds(5);
        //Move object back to original position
        Destroy(particles);
        obj.transform.position = pos;     
    }

}
