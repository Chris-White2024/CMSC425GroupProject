using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextGrower : MonoBehaviour
{

    public float scaleSpeed = 10f;

    public Vector3 targetScale = new Vector3(0f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ScaleOverTime(targetScale));
    }

    IEnumerator ScaleOverTime(Vector3 targetScale)
    {
        while (transform.localScale != targetScale)
        {
            // Interpolate the scale towards the target scale
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
            yield return null;
        }
    }
}
