using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpsesBehavior : MonoBehaviour
{
    private Rigidbody2D _rb;

    public Rigidbody2D Rb { get => _rb; set => _rb = value; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void CreateCorpse(Vector3 position, Vector3 velocity)
    {
        transform.position = position;

        _rb.velocity = velocity;
    }

    public void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        body.AddForce(dir.normalized * explosionForce * wearoff);
    }
}
