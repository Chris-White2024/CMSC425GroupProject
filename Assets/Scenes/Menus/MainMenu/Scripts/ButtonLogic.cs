using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLogic : MonoBehaviour
{
    

    public int levelSelect;
    public int settings;

    public void play() {
        SceneManager.LoadScene(levelSelect);
    }
}
