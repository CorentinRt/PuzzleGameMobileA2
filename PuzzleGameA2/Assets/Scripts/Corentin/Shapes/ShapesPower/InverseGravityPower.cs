using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseGravityPower : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Shape Acceleration Player");
            if (TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehavior))
            {
                playerBehavior.InverseGravity();
            }
            if (TryGetComponent<CorpsesBehavior>(out CorpsesBehavior corpsesBehavior))
            {
                corpsesBehavior.InverseGravity();
            }
        }
    }
}
