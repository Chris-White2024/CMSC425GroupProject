using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadIn : MonoBehaviour
{
    public GameObject gameObject;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveGear());
    }

    // Update is called once per frame
    IEnumerator MoveGear()
    {
        yield return new WaitForSeconds(3f);
        while (transform.position.x > 250) {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - 1000f * Time.deltaTime, gameObject.transform.position.y, 0f);
            transform.position = new Vector3(transform.position.x - 1000f * Time.deltaTime, transform.position.y, 0f);
            transform.Rotate(Vector3.forward, 100f * Time.deltaTime);
            yield return null;
        }
        SceneManager.LoadScene(12);
    }
}
