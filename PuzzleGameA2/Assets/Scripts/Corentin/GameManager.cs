using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using NaughtyAttributes;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; set => _instance = value; }

    private PhaseType _currentPhase;
    
    private LevelManager _levelManager;
    private PlayerManager _playerManager;

    public event Action OnPhase1Started;
    public event Action OnPhase1Ended;

    public event Action OnPhase2Started;
    public event Action OnPhase2Ended;

    public PhaseType CurrentPhase { get => _currentPhase; set => _currentPhase = value; }

    private void Start()
    {
        _levelManager = GetComponent<LevelManager>();
        _playerManager = GetComponent<PlayerManager>();
    }

    public void ChangeGamePhase(PhaseType phase)
    {
        switch (phase)
        {
            case PhaseType.PlateformePlacement:
                StartPhase1();
                break;
            case PhaseType.ChoicePhase:
                EndPhase2();
                break;
            case PhaseType.PlayersMoving:
                if (_currentPhase==PhaseType.PlateformePlacement) EndPhase1();
                StartPhase2();
                break;
            case PhaseType.GameEndPhase:
                int nbStars = CalculateStars();
                break;
        }

        _currentPhase = phase;
    }

    private int CalculateStars()
    {
        int nbPlayerAliveCount = _playerManager.GetPlayerAliveCount();
        int nonRequiredKilledPlayer = _levelManager.GetCurrentLevel().LevelInfo.MaxPlayerToSave - nbPlayerAliveCount;
        return (nonRequiredKilledPlayer==0? 3 : nonRequiredKilledPlayer<=2? 2 : nbPlayerAliveCount==1? 0 : 1);
    }
    
    [Button]
    private void StartPhase1()
    {
        OnPhase1Started?.Invoke();
    }
    [Button]
    private void EndPhase1()
    {
        OnPhase1Ended?.Invoke();
    }
    [Button]
    private void StartPhase2()
    {
        OnPhase2Started?.Invoke();
    }
    [Button]
    private void EndPhase2()
    {
        OnPhase2Ended?.Invoke();
    }


    private void GameEnd()
    {
        ChangeGamePhase(PhaseType.GameEndPhase);
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(_instance);
        }
        _instance = this;
    }
}
