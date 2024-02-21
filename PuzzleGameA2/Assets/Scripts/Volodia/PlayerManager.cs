using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Vector3 _spawnpoint;
    [SerializeField] private int _nbLives;
    private PlayerBehaviour _currentPlayer;
    private PlayerBehaviour _nextPlayer;
    private int _playerCount;
    private bool _isOnPlayerPhase;

    private void Start()
    {
        _nextPlayer = Instantiate(_playerPrefab, _spawnpoint + new Vector3(-2,0,0), transform.rotation).GetComponent<PlayerBehaviour>();
        _nextPlayer.SetSpawnpoint(_spawnpoint);
        _playerCount = 1;
        _isOnPlayerPhase = false;
    }

    [Button]
    public void StartFirstPlayer()
    {
        StartNextPlayer();
        _isOnPlayerPhase = true;
    }
    private void Update()
    {
        if (_playerCount == 6) return;
        if (_currentPlayer == null && _isOnPlayerPhase)
        {
            StartNextPlayer();
        }

    }

    private void StartNextPlayer()
    {
        _nextPlayer.StartWalking();
        _currentPlayer = _nextPlayer;
        if (_playerCount >= _nbLives) return;
        _nextPlayer = Instantiate(_playerPrefab, _spawnpoint + new Vector3(-2,0,0), transform.rotation).GetComponent<PlayerBehaviour>();
        _nextPlayer.SetSpawnpoint(_spawnpoint);
        _playerCount++;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.green;
        Gizmos.DrawWireSphere(_spawnpoint, 1);
    }
}
