using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class endGoal : MonoBehaviour
{
    //Declares all required UI components to put text on user's screen
    public TMP_Text text;
    bool win = false;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !win)
        {
            text.text = "You Win!" + "\n" + "Time taken: " + Time.timeSinceLevelLoad.ToString() + " seconds";
            win = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (win)
        {
        
        }
        else{
        text.text = "Reach the top of the Castle to win!" + "\n" + "Time taken so far" + Time.timeSinceLevelLoad.ToString() + " seconds";
        }    
    }

}
