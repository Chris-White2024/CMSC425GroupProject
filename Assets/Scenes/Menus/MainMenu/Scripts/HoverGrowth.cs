using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverGrowth : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    public float hoverScaleMultiplier = 1.25f;
    public float scaleSpeed = 10f;
    private bool isHovering = false;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Set the flag to indicate that the mouse is hovering over the button
        isHovering = true;
        // Start the scaling coroutine
        StartCoroutine(ScaleOverTime(originalScale * hoverScaleMultiplier));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Reset the flag
        isHovering = false;
        // Start the scaling coroutine
        StartCoroutine(ScaleOverTime(originalScale));
    }

    IEnumerator ScaleOverTime(Vector3 targetScale)
    {
        bool increasing = isHovering;
        while (transform.localScale != targetScale)
        {
            if (increasing && !isHovering) {
                break;
            }
            // Interpolate the scale towards the target scale
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
            yield return null;
        }
    }
}
