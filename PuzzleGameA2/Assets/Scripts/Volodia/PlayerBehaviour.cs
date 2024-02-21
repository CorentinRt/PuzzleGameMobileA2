using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using NaughtyAttributes.Test;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBehaviour : MonoBehaviour
{
    [FormerlySerializedAs("speed")] [SerializeField] private float _speed;
    [SerializeField] private GameObject _corpse;
    private bool _walking;
    private Rigidbody2D _rb;
    private int _direction; //-1 = left ; 1 = right

    [Button]
    private void StartWalking() => _walking = true;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _walking = false;
        _direction = 1;
    }

    private void FixedUpdate()
    {
        if (_walking) _rb.velocity = new Vector2(_speed * _direction * Time.deltaTime, _rb.velocity.y);
    }

    public void ChangeDirection()
    {
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        _direction *= -1;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ()
    }
}
