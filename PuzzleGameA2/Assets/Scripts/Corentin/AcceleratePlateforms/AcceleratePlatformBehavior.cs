using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratePlatformBehavior : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private float _accelerationSpeed;


    private void Accelerate()
    {
        _animator.speed = _accelerationSpeed;
    }
    private void ResetAcceleration()
    {
        _animator.speed = 1;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Start()
    {
        GameManager.Instance.OnPhase1Started += Accelerate;
        GameManager.Instance.OnLevelPresent += ResetAcceleration;
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnPhase1Started -= Accelerate;
        GameManager.Instance.OnLevelPresent -= ResetAcceleration;
    }

}
