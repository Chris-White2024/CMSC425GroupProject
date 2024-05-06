using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinningObjects : MonoBehaviour
{   //This Script makes objects spin around and hover up and down slowly
    public float hoverSpeed = 0.5f;
    public float hoverHeight = 0.5f;
    public float spinSpeed = 50.0f;
    public float spinSpeed2 = 50.0f;
    public char dim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Rotates the object around the respective axis
        if (dim == 'z'){
        transform.Rotate(0,0, spinSpeed * Time.deltaTime);
        }
        else if (dim == 'y'){
        transform.Rotate(0, spinSpeed * Time.deltaTime,0);
        }
        else if (dim == 'w'){
        transform.Rotate(spinSpeed * Time.deltaTime,spinSpeed * Time.deltaTime,0);
        }
        else{
        transform.Rotate(spinSpeed * Time.deltaTime, 0,0);
        }
    }
}
