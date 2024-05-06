using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public GameObject pauseScreen;
    public bool gameIsPaused = false;

    void Start(){
        pauseScreen.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            if(gameIsPaused == false)
            {
                Time.timeScale = 0f;
                pauseScreen.SetActive(true);
            }
            else 
            {
                Time.timeScale = 1f;
                pauseScreen.SetActive(false);
            }
        }    
    }
    public void unpause(){
        gameIsPaused = false;
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
    }
}
