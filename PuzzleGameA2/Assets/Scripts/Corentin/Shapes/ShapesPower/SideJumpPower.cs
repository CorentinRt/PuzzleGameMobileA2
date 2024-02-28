using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideJumpPower : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ShapeManagerNoCanvas shapeManagerNoCanvas = GetComponentInParent<ShapeManagerNoCanvas>();
            if (collision.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehaviour))
            {
                playerBehaviour.SideJump(shapeManagerNoCanvas.GetDirection());
                Debug.Log(shapeManagerNoCanvas.GetDirection());
            }
            if (collision.TryGetComponent<CorpsesBehavior>(out CorpsesBehavior corpsesBehavior))
            {
                corpsesBehavior.SideJump(shapeManagerNoCanvas.GetDirection());
            }
        }
    }
}
