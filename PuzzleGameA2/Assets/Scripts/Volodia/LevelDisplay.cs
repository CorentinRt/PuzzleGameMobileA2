using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] private int _levelID;
    private LevelManager _levelManager;
    private Level _level;
    [SerializeField, Foldout("References")] private Button _button;
    [SerializeField, Foldout("References")] private TextMeshProUGUI _text;
    [SerializeField, Foldout("References")] private List<GameObject> _stars;

    public void SetID(int id)
    {
        _levelID = id;
        UpdateUI();
    }
    
    private void Start()
    {
        UpdateUI();
        LevelManager.Instance.OnSaveLoaded += UpdateUI;
    }

    private void OnDestroy()
    {
        LevelManager.Instance.OnSaveLoaded -= UpdateUI;
    }

    private void UpdateUI()
    {
        Debug.Log("Updating UI");
        _levelManager = LevelManager.Instance;
        _level = _levelManager.GetLevel(_levelID);
        _text.text = "Level " + _level.GetID;
        _button.interactable = _level.isUnlocked;
        if (!_level.isUnlocked) return;
        int stars = _level.GetStarsNum;
        for (int i = 1; i < 4; i++)
        {
            _stars[i - 1].SetActive(stars >= i);
        }
    }

    public void StartLevel()
    {
        //_levelManager.LoadGlobalSceneAndLevel(_levelID);  // Ancienne version

        _levelManager.LoadGlobalSceneAndLevelWithTransition(_levelID); // Alternative avec transition
    }
}
