using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _levelDisplayPrefab;
    [Header("Menus")]
    [SerializeField] private GameObject _levelsMenu;
    [SerializeField] private GameObject _homeMenu;
    [SerializeField] private GameObject _selectMenu;
    [SerializeField] private GameObject _creditMenu;
    [SerializeField] private GameObject _settingMenu;

    [Header("Buttons")] [SerializeField] private GameObject _creditButton;
    [SerializeField] private GameObject _goBackButton;
    
    private void Start()
    {
        foreach (Level level in LevelManager.Instance.GetLevelList())
        {
            //ATTENTION A CHANGER SI BESOIN !
            if (level.GetID > 15) return;
            LevelDisplay lvlDisplay = Instantiate(_levelDisplayPrefab, _levelsMenu.transform).GetComponent<LevelDisplay>();
            lvlDisplay.SetID(level.GetID);
        }
        
    }

    public void OpenSelectMenu()
    {
        _homeMenu.SetActive(false);
        _selectMenu.SetActive(true);
        _goBackButton.SetActive(true);
        _creditButton.SetActive(false);
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
            _goBackButton.SetActive(false);
            _creditButton.SetActive(true);
        }
        else if (_levelsMenu.activeSelf)
        {
            _levelsMenu.SetActive(false);
            _selectMenu.SetActive(true);
        }
        else if (_homeMenu.activeSelf)
        {
            Application.Quit();
            Debug.Log("Application Quit");
        }
    }

    public void OpenCreditMenu()
    {
        _creditMenu.SetActive(!_creditMenu.activeSelf);
    }

    public void OpenSettingsMenu()
    {
        _settingMenu.SetActive(!_settingMenu.activeSelf);
    }
    
}
