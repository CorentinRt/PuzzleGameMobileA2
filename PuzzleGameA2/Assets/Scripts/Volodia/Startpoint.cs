using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startpoint : MonoBehaviour
{
    [SerializeField] private bool _isGravityInverted;
    void Start()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.SetStartPoint(transform.position,_isGravityInverted);
        }
    }
}