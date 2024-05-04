using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vialCollectable : MonoBehaviour
{
    private string sceneName;
    public bool debugMode = true;
    public AudioSource audioSource;
    public AudioClip audioClip;
    private Renderer rend;
    void Start(){
        //Get Name of Scene We are in
        sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        audioSource = GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        //Check if we have collected the vial in this scene
        if(PlayerPrefs.GetInt(sceneName + " vial") == 1 && !debugMode){
            Destroy(gameObject);
        }

    }
    public void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            //Play the audio clip
            audioSource.clip = audioClip;
            audioSource.Play();
            PlayerPrefs.SetInt(sceneName + " vial", 1);
            //Destroy the vial  
            if(audioSource.isPlaying){
                rend.enabled = false;
                Destroy(gameObject, audioClip.length);
            }
            else{
                Destroy(gameObject);
            }
        }
    }
}
