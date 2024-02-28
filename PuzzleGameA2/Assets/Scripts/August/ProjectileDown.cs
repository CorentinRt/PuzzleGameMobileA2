using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDown : MonoBehaviour
{
    public float projectileSpeed;
    private bool playerDetected = false;
    private ProjectileDetector projectileDetector;

    void Start()
    {
        projectileDetector = GetComponentInChildren<ProjectileDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (projectileDetector.playerDetected == true)
        {
            transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);
        }
        
    }

    //collision detection
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            //Destroy the projectile
            playerDetected = true;
            Destroy(gameObject);
            Debug.Log("Player hit");

        }
        
    }
}
