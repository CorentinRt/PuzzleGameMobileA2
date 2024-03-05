using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startpoint : MonoBehaviour
{
    [SerializeField] private bool _isGravityInverted;
    [SerializeField] private Vector3 _spawnpoint;
    void Start()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.SetStartPoint(transform.position + _spawnpoint, transform.position,_isGravityInverted);

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + _spawnpoint,0.2f);
    }
}