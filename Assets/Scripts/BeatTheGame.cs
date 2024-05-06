using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeatTheGame : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if (other.tag == "Player"){
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Current Level")+1);

            SceneManager.LoadScene(6); 
        }
    }
}
