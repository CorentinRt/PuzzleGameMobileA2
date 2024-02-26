using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDown : MonoBehaviour
{
    public float projectileSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);
    }

    //Destroy the projectile when it collides with player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
