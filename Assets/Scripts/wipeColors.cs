using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wipeColors : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ColorHandler>().popAll();
            other.GetComponent<ColorHandler>().PopColor();
        }
    }
    // Update is called once per frame
}
