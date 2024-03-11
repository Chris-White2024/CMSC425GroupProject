using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class outOfBounds : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if (other.name == "Third Person Player 1"){
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        }
    }
}
