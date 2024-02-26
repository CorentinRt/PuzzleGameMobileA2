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
    public void CreateCorpse(Vector3 position)
    {
        transform.position = position;

        Debug.Log("Create corpse");
    }
}
