using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    GameObject ompo;
    ThirdPersonMovement ompoMovement;
    Vector3 offSet;
    bool hasGlider = false;
    bool hasBoots = false;
    Vector3 originalPosition;
    Quaternion originalRotation;

    void Start()
    {
        ompo = GameObject.Find("Third Person Player 1");
        ompoMovement = ompo.GetComponent<ThirdPersonMovement>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void OnTriggerEnter(Collider other)
    {
        this.transform.parent = ompo.transform;
        if (this.name.Contains("Glider"))
        {
            hasGlider = true;
            offSet = new Vector3(0f, .35f, 0f);
            transform.localRotation = Quaternion.identity;
        }
        if (this.name.Contains("Boots"))
        {
            hasBoots = true;
            ompoMovement.hasDoubleJump = true;
            offSet = new Vector3(.4f, -1f, .5f);
            transform.localRotation = Quaternion.Euler(0f, 180.0f, 0.0f);
        }
        transform.localPosition = Vector3.zero + offSet;
    }

    void Update()
    {
        if (hasBoots && !(ompoMovement.hasDoubleJump) && ompoMovement.isGrounded)
        {
            ompoMovement.hasDoubleJump = true;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItem();
            foreach (GameObject obj in GetItemsToRemove())
            {
                if (obj.transform.parent == ompo.transform)
                {
                    transform.parent = null;
                    transform.position = originalPosition;
                    transform.rotation = originalRotation;
                }
            }
        }
        GliderGravity();
    }

    void GliderGravity()
    {
        if (ompoMovement.velocity.y != -2 && ompoMovement.velocity.y < -1 && hasGlider){
            ompoMovement.velocity.y *= .95f;
        }
    }

    void DropItem()
    {
        ompoMovement.gravity = -9.81f;
        hasGlider = false;
        hasBoots = false;
    }

    List<GameObject> GetItemsToRemove()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        List<GameObject> gameObjectsWithScript = new List<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.GetComponent<PickupItem>() != null)
            {
                gameObjectsWithScript.Add(obj);
            }
        }
        return gameObjectsWithScript;
    }
}
