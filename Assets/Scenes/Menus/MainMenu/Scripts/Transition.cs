using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveGear());
    }

    // Update is called once per frame
    IEnumerator MoveGear()
    {
        while (transform.position.x > -50000) {
            transform.position = new Vector3(transform.position.x - 1000f * Time.deltaTime, transform.position.y, 0f);
            transform.Rotate(Vector3.forward, 100f * Time.deltaTime);
            yield return null;
        }
    }
}
