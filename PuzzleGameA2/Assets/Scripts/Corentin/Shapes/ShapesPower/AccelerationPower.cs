using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationPower : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.CurrentPhase == Enums.PhaseType.PlayersMoving)
        {
            //Debug.Log("Shape Acceleration Player");
            if (collision.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehavior))
            {
                int tempDir = GetComponentInParent<ShapeManagerNoCanvas>().GetDirection();

                //if (tempDir == playerBehavior.Direction)
                //{
                //
                //}
                //else
                //{
                //    Debug.Log("Wrong direction to accelerate");
                //}

                playerBehavior.Acceleration();
            }
        }
    }
}
