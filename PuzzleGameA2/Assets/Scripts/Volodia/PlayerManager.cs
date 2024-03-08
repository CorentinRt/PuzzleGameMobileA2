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
    private int _spawnGravity;


    private int _corpsesInMotionCount;

    private bool _onePlayerDied;

    public event Action OnPlayerDeath;

    private static PlayerManager _instance;
    public static PlayerManager Instance { get => _instance; set => _instance = value; }
    public int CorpsesInMotionCount { get => _corpsesInMotionCount; set => _corpsesInMotionCount = value; }
    public PlayerBehaviour CurrentPlayer { get => _currentPlayer; set => _currentPlayer = value; }
    public bool OnePlayerDied { get => _onePlayerDied; set => _onePlayerDied = value; }

    public int GetPlayerAliveCount() => _nbLives - _playerCount  + 2 ;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    public void SetStartPoint(Vector3 spawnpoint, Vector3 pos, bool isGravityInverted)
    {
        _spawnpoint = spawnpoint;
        _startpoint = pos;
        _spawnGravity = isGravityInverted ? -1 : 1;
    }


    private void Start()
    {
        _isOnPlayerPhase = false;
        _gameManager = GameManager.Instance;
        _levelManager = LevelManager.Instance;
        _levelManager.GetCurrentLevelController.OnLevelUnload += Reset;
        _levelManager.OnLevelFinishedLoad += SetLives;
        _gameManager.OnPhase1Started += CreateFirstPlayer;
        OnPlayerDeath += LevelManager.Instance.GetCurrentLevelController.ResetTraps;
        _gameManager.OnPhase2Started += StartNextPlayer;
        _gameManager.SetPlayerManager(this);
        Debug.Log("pManager");

        _levelManager.OnLevelFinishedLoad += ResetOnePlayerKilled;
    }

    private void SetLives()
    {
        _nbLives = _levelManager.GetCurrentLevel().LevelInfo.NbPlayerLives;
        
    }

    private void CreateFirstPlayer()
    {
        SummonPlayer(true);
        _gameManager.OnPhase1Started -= CreateFirstPlayer;

    }

    private void ResetOnePlayerKilled()
    {
        _onePlayerDied = false;
        Debug.Log("Reset one player died");
    }

    private void OnDestroy()
    {
        _gameManager.OnPhase1Started -= CreateFirstPlayer;
        _gameManager.OnPhase2Started -= StartNextPlayer;
        _levelManager.OnLevelFinishedLoad -= SetLives;
        _levelManager.GetCurrentLevelController.OnLevelUnload -= Reset;
        OnPlayerDeath -= LevelManager.Instance.GetCurrentLevelController.ResetTraps;

        _levelManager.OnLevelFinishedLoad -= ResetOnePlayerKilled;
    }
    private void Update()
    {
        Debug.Log(_currentPlayer);
        if (!_isOnPlayerPhase) return;
        if (_currentPlayer == null && _nextPlayer == null)
        {
            _gameManager.ChangeGamePhase(PhaseType.GameOver);
            Debug.Log("GameOver Zigos");
            _isOnPlayerPhase = false;
            return;
        }

        if (_currentPlayer == null && _isOnPlayerPhase && _corpsesInMotionCount == 0)
        {
            _isOnPlayerPhase = false;
            OnPlayerDeath?.Invoke();
            _onePlayerDied = true;
            _gameManager.ChangeGamePhase(PhaseType.ChoicePhase);
        }
    }

    public void Reset()
    {
        _currentPlayer = null;
        _playerCount = 0;
        _isOnPlayerPhase = false;
        _gameManager.OnPhase1Started += CreateFirstPlayer;
        
    }

    private void SummonPlayer(bool resetPlayerCount=false)
    {
        if (_spawnGravity == 0) _spawnGravity = 1;
        _playerCount = resetPlayerCount ? 1 : _playerCount + 1;
        _nextPlayer = Instantiate(_playerPrefab, new Vector3(_spawnpoint.x, /* _spawnGravity * */ _spawnpoint.y, _spawnpoint.z), transform.rotation).GetComponentInChildren<PlayerBehaviour>();
        _nextPlayer.transform.parent.gameObject.GetComponent<Rigidbody2D>().gravityScale *= _spawnGravity;
        _nextPlayer.transform.parent.localScale = new Vector3(_nextPlayer.transform.parent.localScale.x,  _nextPlayer.transform.parent.localScale.y * _spawnGravity, _nextPlayer.transform.parent.localScale.z);
        Debug.Log(_spawnGravity);
        Debug.Log(_nextPlayer.transform.localScale);
        _nextPlayer.SetSpawnpoint(_startpoint);
        _nextPlayer.SetManager(_levelManager);
    }
    private void StartNextPlayer()
    {
        if (_currentPlayer != null) return;
        _nextPlayer.StartWalking();
        _currentPlayer = _nextPlayer;
        _isOnPlayerPhase = true;
        if (_playerCount >= _nbLives) return;
        SummonPlayer();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.green;
        Gizmos.DrawWireSphere(_spawnpoint, 0.5f);
        Gizmos.color=Color.blue;
        Gizmos.DrawWireSphere(_startpoint, 0.5f);
    }
}
