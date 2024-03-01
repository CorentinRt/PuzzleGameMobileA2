using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehavior : MonoBehaviour
{
    private bool _hasExplode;
    
    public bool HasExplode { get => _hasExplode; set => _hasExplode = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameManager.Instance.CurrentPhase == Enums.PhaseType.PlayersMoving)
        {
            if (!_hasExplode)
            {
                if (collision.TryGetComponent<CorpsesBehavior>(out CorpsesBehavior corpsesBehavior))
                {
                    //_hasExplode = true;
                    AddExplosionForce(collision.GetComponent<Rigidbody2D>(), 700f, transform.position + new Vector3(0f, -0.5f), 5f);
                    Debug.Log("Corpse take mine");
                }
                if (collision.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehaviour))
                {
                    playerBehaviour.StepOnMine(transform.position);
                    Debug.Log("Player take mine");
                }
            }
        }
    }
    private void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        body.AddForce(dir.normalized * explosionForce * wearoff);
    }
}
