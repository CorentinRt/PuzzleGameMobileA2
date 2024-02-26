using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirectionPower : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //Debug.Log("Shape Change Direction Player");
                collision.gameObject.GetComponent<PlayerBehaviour>().ChangeDirection();
            }
        }
    }
}
