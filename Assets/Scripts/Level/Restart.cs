using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {        
        if (collider.gameObject.GetComponent<Movement>())
        {
            SceneManager.LoadScene("SampleScene");
        }      
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}

