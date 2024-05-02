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
            image.sprite = locked;
            spinner.rotationSpeed = 0f;
        }
    }

    public void playLevel() {
        SceneManager.LoadScene(level+4);
    }

}
