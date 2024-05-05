using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLogic : MonoBehaviour
{

    private int currentLevel;

    public int level;
    public Sprite locked;
    public Sprite unlocked;
    private Spinner spinner;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        currentLevel = PlayerPrefs.GetInt("Level");
        spinner = this.GetComponent<Spinner>();
        if (currentLevel >= level) {
            image.sprite = unlocked;

        } else {
            //Make the button not look clickable
            this.GetComponent<Button>().interactable = false;
            image.sprite = locked;
            spinner.rotationSpeed = 0f;
        }
    }

    public void playLevel() {
        if (currentLevel >= level) {
            PlayerPrefs.SetInt("Current Level", level);
            SceneManager.LoadScene(level+5);
        }
    }

}
