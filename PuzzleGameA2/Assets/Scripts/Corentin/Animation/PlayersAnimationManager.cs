using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersAnimationManager : MonoBehaviour
{
    private Animator _animator;

    public Animator Animator { get => _animator; set => _animator = value; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayWalkAnimation()
    {

    }
    public void PlayRunAnimation()
    {

    }
    public void PlayJumpAnimation()
    {

    }


    public void PlayWinAnimation()
    {

    }
    public void PlayDeathAnimation()
    {

    }
}
