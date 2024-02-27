using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPower : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.GetComponent<PlayerBehaviour>().Jump();
            if (TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehaviour))
            {
                playerBehaviour.Bump(collision.transform.position);
            }
        }
    }
}
