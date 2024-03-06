using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersAnimationManager : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayIdleAnimation()
    {
        _animator.SetTrigger("Idle");
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
    public void PlayMidJumpAnimation()
    {
        _animator.SetTrigger("MidJump");
    }
    public void PlayEndJumpAnimation()
    {

    }
    public void PlayWinAnimation()
    {
        _animator.SetTrigger("Win");
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
