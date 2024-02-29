using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class PhaseChoice : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField] private GameObject _choicePanel;

    [SerializeField] private TransitionLifeDisplay _transitionLifeDisplay;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        //_gameManager.OnPhase2Ended += ActivatePanel;

        _transitionLifeDisplay.OnTransitionLifeEnded += ActivatePanel;

        _choicePanel.SetActive(false);
    }

    private void OnDestroy()
    {
        //_gameManager.OnPhase2Ended -= ActivatePanel;
        
        _transitionLifeDisplay.OnTransitionLifeEnded -= ActivatePanel;
    }

    private void ActivatePanel()
    {
        _choicePanel.SetActive(true);
    }

    public void GoTo(int phaseNum)
    {
        _gameManager.ChangeGamePhase((PhaseType) phaseNum);
        _choicePanel.SetActive(false);
    }
}
