using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{
    // Start is called before the first frame update
    public string tutorialText; 
    public int waitTime = 5;
    public TMP_Text textBox; // Change TMP to TMP_Text

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Use CompareTag for performance
        {
            StartCoroutine(setText()); // Correct coroutine call
        }
    }

    IEnumerator setText()
    {  
        textBox.text = tutorialText;
        yield return new WaitForSeconds(waitTime);
        textBox.text = "";
    }
}