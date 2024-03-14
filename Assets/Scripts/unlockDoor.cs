using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlockDoor : MonoBehaviour
{
    public GameObject doorToUnlock;

    void OnTriggerEnter(Collider other){
        if (other.tag == "Player"){
            Destroy(doorToUnlock);
            Destroy(gameObject);
        }
    }
}
