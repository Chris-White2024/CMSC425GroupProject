using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class powerUp : MonoBehaviour
{


    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            pickup(other);
        }
    }

    void pickup(Collider player){

        //If playerMaterials.colorStack.Count == 3, return
        string color = "";
        //Grab instance of the color stack defined in ThirdPersonMovement.cs
        ColorHandler playerMaterials = player.GetComponent<ColorHandler>();
        //Push the color onto the stack
        if (playerMaterials.colorStack.Count == 3){
            //Find Instructions gameobject that is child of player
            GameObject canvas = player.transform.Find("Canvas").gameObject;
            GameObject instructions = canvas.transform.Find("Instructions").gameObject;
            //Find the text component of the instructions
            TMPro.TextMeshProUGUI text = instructions.GetComponent<TMPro.TextMeshProUGUI>();
            //Change the text to "You have reached the maximum number of powerups"
            StartCoroutine(fullPowerup(text));
            return;
        }
        
        if (this.name.Contains("Blink")){
            color = "blue";
        }
        else if (this.name.Contains("Speed")){
            color = "red";
        }
        else if (this.name.Contains("Jump")){
            color = "green";
        }
        else if (this.name.Contains("WallRunning")){
            color = "yellow";
        }
        //Spawn a cool effect
        playerMaterials.PushColor(color);
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

    IEnumerator fullPowerup(TMPro.TextMeshProUGUI text){
        yield return new WaitUntil(() => text.text == "");
        text.text = "You have reached the maximum number of powerups";
        yield return new WaitForSeconds(5);
        text.text = "";
    }

}
