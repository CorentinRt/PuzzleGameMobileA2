using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeDirectionPower : MonoBehaviour
{
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject != null)
    //    {
    //        if (collision.gameObject.CompareTag("Player"))
    //        {
    //            //Debug.Log("Shape Change Direction Player");
    //            if (collision.gameObject.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehavior))
    //            {
    //                playerBehavior.ChangeDirection();
    //            }
    //        }
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Player") && GameManager.Instance.CurrentPhase == Enums.PhaseType.PlayersMoving)
            {
                //Debug.Log("Shape Change Direction Player");
                if (collision.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehavior))
                {
                    playerBehavior.ChangeDirection();
                }
            }
        }
    }
}
