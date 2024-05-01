using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ringBoost : MonoBehaviour
{
    GameObject ompo;
    ThirdPersonMovement ompoMovement;
    public float boostTime = 3f;
    public float boostMultiplier = 2f;

    public bool boostActive = false;
    
    // Start is called before the first frame update
    void Start()
    {
        ompo = GameObject.FindWithTag("Player");
        ompoMovement = ompo.GetComponent<ThirdPersonMovement>();        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            StartCoroutine(speedBoost(ompoMovement, ompoMovement.speed));
        }
    }
    IEnumerator speedBoost(ThirdPersonMovement ompoMovement, float originalSpeed){
        yield return new WaitUntil(() => boostActive == false);
        boostActive = true;
        ompoMovement.speed *= boostMultiplier;
        yield return new WaitForSeconds(boostTime);
        boostActive = false;
        ompoMovement.speed = originalSpeed;
    }
}
