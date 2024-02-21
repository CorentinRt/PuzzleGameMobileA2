using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using NaughtyAttributes;
using NaughtyAttributes.Test;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBehaviour : MonoBehaviour
{
    [FormerlySerializedAs("speed")] [SerializeField] private float _speed;
    [SerializeField] private GameObject _corpse;
    [SerializeField] private LayerMask _layer;
    private Vector3 _startpoint;
    private bool _walking;
    private Rigidbody2D _rb;
    private int _direction; //-1 = left ; 1 = right

    [Button]
    public void StartWalking() => _walking = true;

    public void SetSpawnpoint(Vector3 spawnpoint) => _startpoint = spawnpoint;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _walking = false;
        _direction = 1;
    }

    private void FixedUpdate()
    {
        if (_walking)
        {
            Vector2 velocity = AdjustVelocityToSlope(new Vector2(_speed * _direction * Time.deltaTime, _rb.velocity.y));
            _rb.velocity = velocity;
        }
        else if(!_walking && transform.position.x <= _startpoint.x && _rb.velocity.y==0) _rb.velocity = new Vector2(_speed * 1 * Time.deltaTime, _rb.velocity.y);
        else _rb.velocity = new Vector2(0, _rb.velocity.y);
    }

    private Vector3 AdjustVelocityToSlope(Vector3 velocity)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 1.5f, _layer);
        Debug.DrawRay(transform.position, Vector3.down, hit? Color.green : Color.red);
        if (hit);
        {
            Debug.DrawRay(transform.position, Vector2.Perpendicular(hit.normal) * -1);
            Debug.DrawRay(transform.position,hit.normal * Vector3.right, Color.black);
            var slopeRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            var adjustVelocity = slopeRotation * velocity;
            if (adjustVelocity.y < 0)
            {
                return adjustVelocity;
            }
        }
        return velocity;
    }

    public void ChangeDirection()
    {
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        _direction *= -1;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Death"))
        {
            Instantiate(_corpse, transform.position + new Vector3(_direction*0.5f,-transform.localScale.y/2,0), transform.rotation);
            Destroy(gameObject);
        }
    }
}
