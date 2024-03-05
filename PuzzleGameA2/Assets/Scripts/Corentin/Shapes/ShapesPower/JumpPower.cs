using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPower : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.CurrentPhase == Enums.PhaseType.PlayersMoving)
        {
            if (collision.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehaviour))
            {
                playerBehaviour.Jump();
            }
            if (collision.TryGetComponent<CorpsesBehavior>(out CorpsesBehavior corpsesBehavior))
            {
                corpsesBehavior.Jump();
            }
        }
    }
}
