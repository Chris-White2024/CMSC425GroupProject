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
        SceneManager.LoadScene(PlayerPrefs.GetInt("Current Level")+5);
    }

    public void replayLevelFromEndMenu(){
        SceneManager.LoadScene(PlayerPrefs.GetInt("Current Level")+5);
    }

    public void startMenu(){
        SceneManager.LoadScene(0); 
    }
    public void levelSelect(){
        SceneManager.LoadScene(1); 
    }


    void loadLevel(){
        if(PlayerPrefs.GetInt("Level") == 6){
            SceneManager.LoadScene(5);
        }
        else{
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level")+5); 
        PlayerPrefs.SetInt("Current Level", PlayerPrefs.GetInt("Level"));
        }
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

    void Update(){
        //If L is pressed set level to 5 and all vial counts to 1
        if(Input.GetKeyDown(KeyCode.L)){
            PlayerPrefs.SetInt("Level", 5);
            PlayerPrefs.SetInt("Level 1 vial", 1);
            PlayerPrefs.SetInt("Level 2 vial", 1);
            PlayerPrefs.SetInt("Level 3 vial", 1);
            PlayerPrefs.SetInt("Level 4 vial", 1);
            PlayerPrefs.SetInt("Level 5 vial", 1);
        }
    }

}
