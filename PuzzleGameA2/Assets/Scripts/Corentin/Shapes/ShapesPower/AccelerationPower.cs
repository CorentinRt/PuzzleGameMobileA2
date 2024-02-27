using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationPower : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Shape Acceleration Player");
            if (TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehavior))
            {
                playerBehavior.Acceleration();
            }
        }
    }
}
