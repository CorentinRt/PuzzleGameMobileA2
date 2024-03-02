using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class MineBehavior : MonoBehaviour
{
    private bool _hasExplode;
    
    public bool HasExplode { get => _hasExplode; set => _hasExplode = value; }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!_hasExplode)
            {
                if (collision.TryGetComponent<CorpsesBehavior>(out CorpsesBehavior corpsesBehavior))
                {
                    //_hasExplode = true;
                    //Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10f);
                    //foreach (var item in colliders)
                    //{
                    //    if (item.TryGetComponent<CorpsesBehavior>(out CorpsesBehavior corpsesBehavior1))
                    //    {
                    //        AddExplosionForce(item.GetComponent<Rigidbody2D>(), 700f, transform.position + new Vector3(0f, -0.5f), 10f);
                    //        Debug.Log("Corpse take mine");
                    //    }
                    //}
                    AddExplosionForce(collision.GetComponent<Rigidbody2D>(), 700f, transform.position + new Vector3(0f, -0.5f), 10f);
                }
                if (GameManager.Instance.CurrentPhase == Enums.PhaseType.PlayersMoving)
                {
                    if (collision.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehaviour))
                    {
                        playerBehaviour.StepOnMine(transform.position);
                        Debug.Log("Player take mine");
                    }
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
