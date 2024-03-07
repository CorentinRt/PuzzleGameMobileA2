using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideJumpPower : MonoBehaviour
{

    private bool _hasUsed;

    public bool HasUsed { get => _hasUsed; set => _hasUsed = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.CurrentPhase == Enums.PhaseType.PlayersMoving && !_hasUsed)
        {
            _hasUsed = true;

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
