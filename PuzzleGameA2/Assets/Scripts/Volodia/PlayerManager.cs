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


    public event Action OnPlayerDeath;

    private static PlayerManager _instance;
    public static PlayerManager Instance { get => _instance; set => _instance = value; }


    public int GetPlayerAliveCount() => _nbLives - _playerCount /* + 2 */;


    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    public void SetStartPoint(Vector3 pos, bool isGravityInverted)
    {
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

    private void OnDestroy()
    {
        _gameManager.OnPhase1Started -= CreateFirstPlayer;
        _gameManager.OnPhase2Started -= StartNextPlayer;
        _levelManager.OnLevelFinishedLoad -= SetLives;
        _levelManager.GetCurrentLevelController.OnLevelUnload -= Reset;
        OnPlayerDeath -= LevelManager.Instance.GetCurrentLevelController.ResetTraps;
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

        if (_currentPlayer == null && _isOnPlayerPhase)
        {
            _isOnPlayerPhase = false;
            OnPlayerDeath?.Invoke();
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
        _nextPlayer = Instantiate(_playerPrefab, new Vector3(_spawnpoint.x, _spawnGravity * _spawnpoint.y, _spawnpoint.z), transform.rotation).GetComponent<PlayerBehaviour>();
        _nextPlayer.gameObject.GetComponent<Rigidbody2D>().gravityScale *= _spawnGravity;
        _nextPlayer.transform.localScale = new Vector3(1, _spawnGravity, 1);
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
