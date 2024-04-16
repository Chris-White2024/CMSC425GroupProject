using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCaptureDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* 
     * Called when inner collider catches a trigger
     */
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Player")) {
            Debug.Log("Game Over");
        }
    }
}
