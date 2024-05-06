using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if (other.tag == "Player"){
            //If level we are on is the highest level we've reached based on the scene name, then increment the level count
            if(PlayerPrefs.GetInt("Level") == PlayerPrefs.GetInt("Current Level"))
            {
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Current Level")+1);
            }

            //If we completed the 5th level, load scene 6 as we have completed the game
            if(PlayerPrefs.GetInt("Current Level") == 5)
            {
                SceneManager.LoadScene(6);
            }

            SceneManager.LoadScene(3); 
        }
    }
}
