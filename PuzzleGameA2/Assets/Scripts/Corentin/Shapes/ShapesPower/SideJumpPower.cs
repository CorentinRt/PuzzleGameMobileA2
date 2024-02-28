using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideJumpPower : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehaviour))
            {
                playerBehaviour.SideJump();
            }
            if (collision.TryGetComponent<CorpsesBehavior>(out CorpsesBehavior corpsesBehavior))
            {
                corpsesBehavior.SideJump();
            }
        }
    }
}
