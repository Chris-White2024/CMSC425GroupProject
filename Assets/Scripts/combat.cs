using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


public class combat : MonoBehaviour
{
    public int health = 5;

    public ThirdPersonMovement playerMovement;
    public float pushForce = 5f;
    public PickupItem ompoItems;

    public GameObject ompoMesh;

    void Start()
    {
        playerMovement = this.GetComponent<ThirdPersonMovement>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (this.gameObject.tag == "Player"){
            this.GetComponent<displayHealth>().updateHealth();
        }
    }
    public void Heal(int heal)
    {
        health += heal;
        if (this.gameObject.tag == "Player"){
            this.GetComponent<displayHealth>().updateHealth();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            pushBack(other);
            TakeDamage(1);
            StartCoroutine(flashMesh());
        }
        else if (other.gameObject.tag == "Hazard")
        {
            pushUp();
            TakeDamage(2);
            StartCoroutine(flashMesh());
        }
        else if (other.gameObject.tag == "Player" && ompoItems.hasBoots == true)
        {
            TakeDamage(1);
            pushUpOmpo(other);
            StartCoroutine(flashMesh());
        }
    }

    public void pushUpOmpo(Collider other)
    {
        Vector3 upWards = other.transform.up.normalized;
        //Reset ompoGravity to 0 to avoid player falling back down
        other.GetComponent<ThirdPersonMovement>().velocity.y = 0f;
        other.GetComponent<ThirdPersonMovement>().controller.Move(upWards * pushForce);
    }
    public void pushBack(Collider other)
    {
        Vector3 pushDirection = other.transform.forward.normalized;
        //Angle Slightly up to avoid pushing player into the ground
        pushDirection.y = 0.5f;
        //Apply force in opposite direction for a short time
        playerMovement.controller.Move(pushDirection * pushForce); 
    }
    public void pushUp()
    {
        Vector3 upWards = this.transform.up.normalized;
        //Reset ompoGravity to 0 to avoid player falling back down
        playerMovement.controller.Move(upWards * pushForce); 
        playerMovement.velocity.y = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (this.gameObject.tag == "Player"){
            SceneManager.LoadScene("deathScreen");
            }
            else{
                //Destroy enemy and all parent objects
                Destroy(this.gameObject.transform.parent.gameObject);
            }
        }
    }

    IEnumerator flashMesh(){

        MeshRenderer mesh = ompoMesh.GetComponent<MeshRenderer>();
        for (int i = 0; i < 5; i++)
        {
            mesh.enabled = false;
            yield return new WaitForSeconds(0.1f);
            mesh.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
