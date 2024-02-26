using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehavior : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("TakeMine");
            if(collision.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehaviour))
            {
                playerBehaviour.StepOnMine(transform.position);
            }

        }
    }
}
