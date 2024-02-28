using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Enums;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Vector3 _spawnpoint;
    [SerializeField] private Vector3 _startpoint;
    [SerializeField] private int _nbLives;
    private PlayerBehaviour _currentPlayer;
    private PlayerBehaviour _nextPlayer;
    private int _playerCount;
    private bool _isOnPlayerPhase;
    private GameManager _gameManager;
    private LevelManager _levelManager;

    public event Action OnPlayerDeath;

    public int GetPlayerAliveCount() => _nbLives - _playerCount + 2;

    private void Start()
    {
        _isOnPlayerPhase = false;
        _gameManager = GameManager.Instance;
        _levelManager = _gameManager.gameObject.GetComponent<LevelManager>();
        _levelManager.OnLevelFinishedLoad += CreateFirstPlayer;
        OnPlayerDeath += _levelManager.ResetTraps;
        _gameManager.OnPhase2Started += StartNextPlayer;
        _gameManager.SetPlayerManager(this);
        Debug.Log("pManager");
    }

    private void CreateFirstPlayer()
    {
        SummonPlayer(true);
    }

    private void OnDestroy()
    {
        _gameManager.OnPhase2Started -= StartNextPlayer;
        _levelManager.OnLevelFinishedLoad -= CreateFirstPlayer;
        OnPlayerDeath -= _levelManager.ResetTraps;
    }
    private void Update()
    {
        if (_playerCount == 6) return;
        if (_currentPlayer == null && _isOnPlayerPhase)
        {
            _isOnPlayerPhase = false;
            OnPlayerDeath?.Invoke();
            _gameManager.ChangeGamePhase(PhaseType.ChoicePhase);
        }
    }

    private void Reset()
    {
        _currentPlayer = null;
        SummonPlayer(true);
        _playerCount = 1;
        _isOnPlayerPhase = false;
        
    }

    private void SummonPlayer(bool resetPlayerCount=false)
    {
        _playerCount = resetPlayerCount ? 1 : _playerCount + 1;
        _nextPlayer = Instantiate(_playerPrefab, _spawnpoint, transform.rotation).GetComponent<PlayerBehaviour>();
        _nextPlayer.SetSpawnpoint(_startpoint);
        _nextPlayer.SetManager(_levelManager);
    }
    private void StartNextPlayer()
    {
        if (_currentPlayer != null) return;
        _nextPlayer.StartWalking();
        _currentPlayer = _nextPlayer;
        if (_playerCount >= _nbLives) return;
        SummonPlayer();
        _isOnPlayerPhase = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.green;
        Gizmos.DrawWireSphere(_spawnpoint, 0.5f);
        Gizmos.color=Color.blue;
        Gizmos.DrawWireSphere(_startpoint, 0.5f);
    }
}
