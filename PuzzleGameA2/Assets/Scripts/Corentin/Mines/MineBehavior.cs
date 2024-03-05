using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                    collision.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

                    collision.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

                    // !!!!!!!!!!!!! Explosion normale !!!!!!!!!!!!!!!!!!!!!!!!
                    //AddExplosionForce(collision.GetComponent<Rigidbody2D>(), 700f, transform.position + new Vector3(0f, -0.5f), 10f);

                    // !!!!!!!!!!!!!!!!!!! Pour explosion predef !!!!!!!!!!!!!!!!!!!!!
                    /*
                     */
                    Vector2 dir = new Vector2(1f, 1f);

                    dir = dir.normalized;

                    dir.x *= GetComponentInParent<ShapeManagerNoCanvas>().GetDirection();

                    dir.y *= GetComponentInParent<ShapeManagerNoCanvas>().transform.localScale.y;

                    collision.GetComponent<Rigidbody2D>().AddForce(dir * 10f, ForceMode2D.Impulse);
                    
                    // !!!!!!!!!!!!!!!!!!! Fin explosion predef !!!!!!!!!!!!!!!!!!!!!
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
