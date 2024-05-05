using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class LevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject ompo;
    public GameObject shine1;
    public GameObject shine2;

    public float heightincrease;

    private Vector3 originalScale;
    public float hoverScaleMultiplier = 1.25f;
    public float scaleSpeed = 10f;
    private bool isHovering = false;
    
    private bool isHoveringEnabled = false;

    public Vector3 target1;
    public Vector3 target2;

    void Start()
    {
        originalScale = transform.localScale;

        StartCoroutine(waitForHover());
    }

    IEnumerator waitForHover() {
        yield return new WaitForSeconds(2.5f);

        isHoveringEnabled = true;

        if (isHovering) {
            StartCoroutine(ScaleOverTime(originalScale * hoverScaleMultiplier));
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Set the flag to indicate that the mouse is hovering over the button
        isHovering = true;
        // Start the scaling coroutine
        if (isHoveringEnabled) {
            StartCoroutine(ScaleOverTime(originalScale * hoverScaleMultiplier));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Reset the flag
        isHovering = false;
        // Start the scaling coroutine
        StartCoroutine(ScaleOverTime(originalScale));
    }

    public void OnPointerClick(PointerEventData eventData) {

        StartCoroutine(ShineThenGo());
    }

    IEnumerator ShineThenGo() {
        while (shine1.transform.localScale.x < target1.x - .001) {
            shine1.transform.localScale = Vector3.Lerp(shine1.transform.localScale, target1, Time.deltaTime * 5f);
            shine2.transform.localScale = Vector3.Lerp(shine2.transform.localScale, target2, Time.deltaTime * 5f);
            shine1.transform.Rotate(-Vector3.forward * 250 * Time.deltaTime);
            shine2.transform.Rotate(-Vector3.forward * 250 * Time.deltaTime);


            yield return null;
        }


        SceneManager.LoadScene(PlayerPrefs.GetInt("Level")+4); 
        PlayerPrefs.SetInt("Current Level", PlayerPrefs.GetInt("Level"));
    }

    IEnumerator ScaleOverTime(Vector3 targetScale)
    {
        bool increasing = isHovering;
        int sign = increasing ? 1 : -1;
        Vector3 newP = new Vector3(ompo.transform.position.x, ompo.transform.position.y + (heightincrease * sign), 0f);
        while (transform.localScale != targetScale)
        {
            if (increasing && !isHovering) {
                break;
            }
            if (!increasing && isHovering) {
                break;
            }

            ompo.transform.position = Vector3.Lerp(ompo.transform.position, newP, Time.deltaTime * scaleSpeed);
            // Interpolate the scale towards the target scale
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
            yield return null;
        }
    }
}
