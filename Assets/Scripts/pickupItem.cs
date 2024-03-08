using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupItem : MonoBehaviour
{
    GameObject ompo;
    ThirdPersonMovement ompoMovement;
    Vector3 offSet;
    bool hasGlider = false;
    bool hasBoots = false;
    Vector3 originalPosition;
    Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        ompo = GameObject.Find("Third Person Player 1");
        ompoMovement = ompo.GetComponent<ThirdPersonMovement>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        
    }

    void OnTriggerEnter(Collider other){
        this.transform.parent = ompo.transform;
        if (this.name.Contains("Glider")){
            hasGlider = true;
            offSet = new Vector3 (0f,.35f,0f);
            transform.localRotation = Quaternion.identity;
        }
        if (this.name.Contains("Boots")){
            hasBoots = true;
            ompoMovement.hasDoubleJump = true;
            offSet = new Vector3 (.5f,-1f,.5f);
            transform.localRotation = Quaternion.Euler(0f, 180.0f, 0.0f);
        }
        // Match position and rotation with the collided object
        transform.localPosition = Vector3.zero;
        transform.localPosition += offSet;
    }
    void Update(){
        if (hasBoots && !(ompoMovement.hasDoubleJump)){
            if(ompoMovement.isGrounded){
                ompoMovement.hasDoubleJump = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q)){
            dropItem();
            foreach (GameObject obj in getItemsToRemove()){
                if (obj.transform.parent == ompo.transform){
                    transform.parent = null;
                    transform.position = originalPosition;
                    transform.rotation = originalRotation;
                }
            }

        }
        if (Input.GetButtonDown("Jump")){
            StartCoroutine(doubleJump());
        }
        gliderGravity();
    }
    void gliderGravity(){
        if(ompoMovement.velocity.y != -2 && ompoMovement.velocity.y < 0 && hasGlider){
            ompoMovement.gravity = -2.5f;
        }
        else{
            ompoMovement.gravity = -9.81f;
        }
    }

    IEnumerator doubleJump(){
        yield return new WaitForSeconds(1);
        ompoMovement.hasDoubleJump = false;    
    }
    void dropItem(){
        hasGlider = false;
        hasBoots = false;
    }
    List<GameObject> getItemsToRemove(){
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        // List to store GameObjects with the script
        List<GameObject> gameObjectsWithScript = new List<GameObject>();

        // Loop through each GameObject
        foreach (GameObject obj in allObjects)
        {
            // Check if the GameObject has the script attached
            if (obj.GetComponent<pickupItem>() != null)
            {
                // Add the GameObject to the list
                gameObjectsWithScript.Add(obj);
            }
        }
        return gameObjectsWithScript;
    }

}
