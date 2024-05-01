using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vialCollectable : MonoBehaviour
{
    private string sceneName;
    public bool debugMode = true;
    void Start(){
        //Get Name of Scene We are in
        sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        //Check if we have collected the vial in this scene
        if(PlayerPrefs.GetInt(sceneName + " vial") == 1 && !debugMode){
            Destroy(gameObject);
        }

    }
    public void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            PlayerPrefs.SetInt(sceneName + " vial", 1);
            //Destroy the vial  
            Destroy(gameObject);
        }
    }
}
