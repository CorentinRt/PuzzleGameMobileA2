using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSpherePower : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameManager.Instance.CurrentPhase == Enums.PhaseType.PlayersMoving)
            {
                if (collision.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehaviour))
                {
                    playerBehaviour.KillPlayer();
                    GetComponentInParent<ShapeManagerNoCanvas>().Desactive();
                }
            }
        }
    }

}
