using UnityEngine;


public class ProjectileDetector : MonoBehaviour
{
    public bool playerDetected = false;

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            //Destroy the projectile
            playerDetected = true;
            Debug.Log("Player hit");

        }
    }
}
