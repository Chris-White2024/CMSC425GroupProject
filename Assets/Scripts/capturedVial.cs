using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class capturedVial : MonoBehaviour
{
    //Contains Two Images for the button to display either blank or the vial if it has been captured on that stage
    public Sprite blank;
    public Sprite vial;
    public int level;

    void Start(){
        //If player prefs has the vial for this level, display the vial image as the source image of the image component
        if(PlayerPrefs.GetInt("Level " + level +  " vial") == 1){
            this.GetComponent<UnityEngine.UI.Image>().sprite = vial;
        }
        //If player prefs does not have the vial for this level, display the blank image
        else{
            this.GetComponent<UnityEngine.UI.Image>().sprite = blank;
        }
    }
}
