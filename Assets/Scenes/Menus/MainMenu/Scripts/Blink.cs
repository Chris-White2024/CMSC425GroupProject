using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    public Sprite beakerSprite; // Assign the "beaker" sprite in the Unity Editor
    public Sprite blinkSprite;  // Assign the "blink" sprite in the Unity Editor
    private SpriteRenderer spriteRenderer;
    private Image image;


    public float blinkMin = 10f;
    public float blinkMax = 30f;

    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(StartBlinking());
    }

    IEnumerator StartBlinking()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(blinkMin, blinkMax)); // Wait for a random time between 10 and 30 seconds
            image.sprite = blinkSprite;
            yield return new WaitForSeconds(.5f);
            image.sprite = beakerSprite;
        }
    }
}
