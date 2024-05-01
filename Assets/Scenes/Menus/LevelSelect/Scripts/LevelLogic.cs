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

    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        currentLevel = PlayerPrefs.GetInt("Level");

        if (currentLevel >= level) {
            image.sprite = unlocked;

        } else {
            image.sprite = locked;
        }
    }

    public void playLevel() {
        SceneManager.LoadScene(level);
    }

}
