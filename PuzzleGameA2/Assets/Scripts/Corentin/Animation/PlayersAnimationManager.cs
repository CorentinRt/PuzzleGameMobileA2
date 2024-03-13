using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayersAnimationManager : MonoBehaviour
{
    private Animator _animator;
    public event Action OnStopLanding;

    [FormerlySerializedAs("boneSprites")] [SerializeField] private List<SpriteRenderer> _boneSprites;

    [Button]
    private void GetSprites()
    {
        _boneSprites = new List<SpriteRenderer>();
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            _boneSprites.Add(spriteRenderer);
        }
    }

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

    public IEnumerator PlayFadeOutAnimation()
    {
        while (_boneSprites[0].color.a>0)
        {
            foreach (SpriteRenderer boneSprite in _boneSprites)
            {
                var color = boneSprite.color;
                color.a -= Time.deltaTime * 3;
                boneSprite.color = color;
            }
            yield return null;
        }

    }
}
