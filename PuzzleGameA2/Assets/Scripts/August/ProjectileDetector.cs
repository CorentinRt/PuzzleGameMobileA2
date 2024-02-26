using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileDetector : MonoBehaviour
{
    private bool playerDetected = false;

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //collision detection. bool true if player detected
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            playerDetected = true;
            Debug.Log("Player detected");
        }
    }
}
