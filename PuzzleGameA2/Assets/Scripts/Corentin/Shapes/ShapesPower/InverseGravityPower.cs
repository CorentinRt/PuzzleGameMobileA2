using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseGravityPower : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.CurrentPhase == Enums.PhaseType.PlayersMoving)
        {
            //Debug.Log("Shape Acceleration Player");
            if (collision.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehavior))
            {
                playerBehavior.InverseGravity();
                GetComponentInParent<ShapeManagerNoCanvas>().Desactive();
                
            }
            if (collision.TryGetComponent<CorpsesBehavior>(out CorpsesBehavior corpsesBehavior))
            {
                corpsesBehavior.InverseGravity();
                GetComponentInParent<ShapeManagerNoCanvas>().Desactive();
            }
        }
    }
}
