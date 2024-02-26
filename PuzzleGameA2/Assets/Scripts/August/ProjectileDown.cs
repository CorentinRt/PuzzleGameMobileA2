using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDown : MonoBehaviour
{
    public float projectileSpeed;

    // Update is called once per frame
    void Update()
    {
        
    }

    //collision detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Destroy the projectile
            Destroy(gameObject);
            Debug.Log("Player hit");

        }

        if (collision.gameObject.tag == "Detector")
        {
            //Translate the projectile down
            transform.Translate(Vector3.down * projectileSpeed * Time.deltaTime);
        }
    }
}
