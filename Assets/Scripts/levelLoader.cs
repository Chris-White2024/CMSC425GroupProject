using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class levelLoader : MonoBehaviour
{
    public void nextLevel(){
        loadLevel();
    }

    public void replayLevel(){
        loadLevel();
    }

    public void startMenu(){
        SceneManager.LoadScene(0); 
    }

    void loadLevel(){
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level")+4); 
    }
    public void resetLevels(){
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetInt("Level 1 vial", 0);
        PlayerPrefs.SetInt("Level 2 vial", 0);
        PlayerPrefs.SetInt("Level 3 vial", 0);
        PlayerPrefs.SetInt("Level 4 vial", 0);
        PlayerPrefs.SetInt("Level 5 vial", 0);
    }
    public void loadSettings() {
        SceneManager.LoadScene(4);
    }

    public void exit(){
        //If in editor, stop playing
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        //If in build, quit the application
        Application.Quit();
    }

}
