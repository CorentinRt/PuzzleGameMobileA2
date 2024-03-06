using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _levelDisplayPrefab;
    [SerializeField] private GameObject _levelsMenu;
    [SerializeField] private GameObject _homeMenu;
    [SerializeField] private GameObject _selectMenu;

    private void Start()
    {
        foreach (Level level in LevelManager.Instance.GetLevelList())
        {
            LevelDisplay lvlDisplay = Instantiate(_levelDisplayPrefab, _levelsMenu.transform).GetComponent<LevelDisplay>();
            lvlDisplay.SetID(level.GetID);
        }
        
    }

    public void OpenSelectMenu()
    {
        _homeMenu.SetActive(false);
        _selectMenu.SetActive(true);
    }

    public void NewGame()
    {
        //TODO: Add popup check if wants to reset
        _selectMenu.SetActive(false);
        SaveManager.DeleteData();
        LevelManager.Instance.ReloadSave();
        _levelsMenu.SetActive(true);
    }

    public void Continue()
    {
        LevelManager.Instance.LoadLastLevelUnlocked();
    }

    public void Levels()
    {
        _selectMenu.SetActive(false);
        _levelsMenu.SetActive(true);
    }

    public void GoBack()
    {
        if (_selectMenu.activeSelf)
        {
            _selectMenu.SetActive(false);
            _homeMenu.SetActive(true);
        }
        else if (_levelsMenu.activeSelf)
        {
            _levelsMenu.SetActive(false);
            _selectMenu.SetActive(true);
        }
    }
    
}
