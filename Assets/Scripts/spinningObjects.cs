using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinningObjects : MonoBehaviour
{   //This Script makes objects spin around and hover up and down slowly
    public float hoverSpeed = 0.5f;
    public float hoverHeight = 0.5f;
    public float spinSpeed = 50.0f;
    public float spinSpeed2 = 50.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Make rotate in place
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }
}
