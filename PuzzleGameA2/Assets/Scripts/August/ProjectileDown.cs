using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDown : MonoBehaviour
{
    public float projectileSpeed;
    private bool playerDetected = false;

    // Update is called once per frame
    void Update()
    {
        //Detect player on object
        if (playerDetected == true)
        {
            Debug.Log("Player Detected");
        }

    }

    //Destroy the projectile when it collides with the platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerDetected == true)
        {
            Destroy(gameObject);
        }
    }
}
