using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; set => _instance = value; }

    public event Action OnPhase1Started;
    public event Action OnPhase1Ended;

    public event Action OnPhase2Started;
    public event Action OnPhase2Ended;

    private enum PhaseType
    {
        GamePhase1,
        GamePhase2,
        GameEndPhase
    }
    private void ChangeGamePhase(PhaseType phase)
    {
        switch (phase)
        {
            case PhaseType.GamePhase1:
                StartPhase1();
                break;
            case PhaseType.GamePhase2:
                EndPhase1();
                StartPhase2();
                break;
            case PhaseType.GameEndPhase:
                EndPhase2();
                break;
        }
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

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(_instance);
        }
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
