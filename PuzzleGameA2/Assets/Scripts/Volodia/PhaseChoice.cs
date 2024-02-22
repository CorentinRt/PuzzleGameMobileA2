using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class PhaseChoice : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField] private GameObject _choicePanel;

    private void Start()
    {
        _gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        _gameManager.OnPhase2Ended += ActivatePanel;
        _choicePanel.SetActive(false);
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
