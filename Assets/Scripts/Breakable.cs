using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{


    // Update is called once per frame
    void OnTriggerEnter(Collider other){
        GameObject player = GameObject.Find("Body");
        if(player.GetComponent<Renderer>().material.color == Color.red){
            Destroy(gameObject);
        }
    }

}
