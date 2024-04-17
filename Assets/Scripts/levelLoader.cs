using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class levelLoader : MonoBehaviour
{
    public void nextLevel(){
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level")+1);
        loadLevel();
    }

    public void replayLevel(){
        loadLevel();
    }

    public void startMenu(){
        SceneManager.LoadScene("StartMenu"); 
    }

    void loadLevel(){
        SceneManager.LoadScene("Level " + PlayerPrefs.GetInt("Level")); 
    }
    public void resetLevels(){
        PlayerPrefs.SetInt("Level", 0);
    }
}
