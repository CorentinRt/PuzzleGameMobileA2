using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; set => _instance = value; }

    private PhaseType _currentPhase;
    
    public int NbStars { get => _nbStars;}

    int _nbStars;

    int _overlapShapeCount;

    private LevelManager _levelManager;
    private PlayerManager _playerManager;

    public event Action OnPhase1Started;
    public event Action OnPhase1Ended;

    public event Action OnPhase2Started;
    public event Action OnPhase2Ended;

    public event Action OnGameEnd;
    public event Action OnGameLost;

    public PhaseType CurrentPhase { get => _currentPhase; set => _currentPhase = value; }
    public int OverlapShapeCount { get => _overlapShapeCount; set => _overlapShapeCount = value; }

    public void SetPlayerManager(PlayerManager playerManager) => _playerManager = playerManager;

    private void Start()
    {
        _levelManager = LevelManager.Instance;
        _levelManager.OnLevelFinishedLoad += StartGame;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (_levelManager!=null) _levelManager.OnLevelFinishedLoad -= StartGame;
    }

    public void StartGame() => ChangeGamePhase(PhaseType.PlateformePlacement);

    public void ChangeGamePhase(PhaseType phase)
    {
        if (_overlapShapeCount != 0 && _currentPhase == PhaseType.PlateformePlacement)
        {
            Debug.Log("Can't change phase beacause shapes are overlaping");
            return;
        }

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
                _nbStars = CalculateStars();
                _levelManager.GetCurrentLevel().SetStars(_nbStars);
                _levelManager.UnlockNextLevel();
                GameEnd();
                break;
            case PhaseType.GameOver:
                Debug.Log("GameOver Manager");
                GameOver();
                break;

        }

        _currentPhase = phase;
    }

    private int CalculateStars()
    {
        int nbPlayerAliveCount = _playerManager.GetPlayerAliveCount();
        int nonRequiredKilledPlayer = _levelManager.GetCurrentLevel().LevelInfo.MaxPlayerToSave - nbPlayerAliveCount;
        return (nonRequiredKilledPlayer<=0? 3 : nonRequiredKilledPlayer<=2? 2 : nbPlayerAliveCount==1? 0 : 1);
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
        OnGameEnd?.Invoke();
    }
    private void GameOver()
    {
        OnGameLost?.Invoke();
    }


    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }
}
