using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersAnimationManager : MonoBehaviour
{
    private Animator _animator;
    public event Action OnStopLanding;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayIdleAnimation()
    {
        _animator.SetTrigger("Idle");
    }
    public void EnableFearAnimation()
    {
        _animator.SetBool("IsAffraid", true);
    }
    public void DisableFearAnimation()
    {
        _animator.SetBool("IsAffraid", false);
    }

    public void StartRunAnimation()
    {
        _animator.SetBool("IsRunning", true);
    }
    public void EndRunAnimation()
    {
        _animator.SetBool("IsRunning", false);
    }
    public void PlaySpeedBoostAnimation()
    {
        _animator.SetTrigger("SpeedBoost");
    }
    public void PlayStartJumpAnimation()
    {
        _animator.SetTrigger("StartJump");
    }
    public void StartMidJumpAnimation()
    {
        _animator.SetBool("IsJumping", true);
    }
    public void EndMidJumpAnimation()
    {
        _animator.SetBool("IsJumping", false);
    }
    public void StopLanding()
    {
        OnStopLanding?.Invoke();
    }
    public void PlayWinAnimation()
    {
        if (_animator!=null) _animator.SetTrigger("Win");
    }
    public void PlayDeathBySpikeAnimation()
    {
        _animator.SetTrigger("SpikeDeath");
        _animator.SetBool("IsDead", true);
    }
    public void PlayDeathByLaserAnimation()
    {
        _animator.SetTrigger("LaserDeath");
        _animator.SetBool("IsDead", true);
    }
}
