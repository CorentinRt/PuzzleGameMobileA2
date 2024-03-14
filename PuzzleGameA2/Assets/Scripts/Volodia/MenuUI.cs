using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
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
    [SerializeField] private GameObject _arrowButtons;

    [Header("Buttons")] [SerializeField] private GameObject _creditButton;
    [SerializeField] private GameObject _goBackButton;

    private Animator _animator;
    [OnValueChanged(nameof(DisplayLevels))] private int _firstDisplayedID;
    private List<LevelDisplay> _levelDisplays;
    private static readonly int OnLevelMenu = Animator.StringToHash("OnLevelMenu");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _firstDisplayedID = 1;
        _levelDisplays = new List<LevelDisplay>();
        for (int i = 0; i < 6; i++)
        {
            if (!LevelManager.Instance.DoesLevelExist(_firstDisplayedID+i)) return;
            LevelDisplay lvlDisplay = Instantiate(_levelDisplayPrefab, _levelsMenu.transform).GetComponent<LevelDisplay>();
            lvlDisplay.SetID(_firstDisplayedID + i);
            _levelDisplays.Add(lvlDisplay);
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
        _animator.SetBool(OnLevelMenu, true);
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
            _arrowButtons.SetActive(false);
            _animator.SetBool(OnLevelMenu, false); 
        }
        /*else if (_homeMenu.activeSelf)
        {
            Application.Quit();
            Debug.Log("Application Quit");
        }*/
    }

    public void OpenCreditMenu()
    {
        _creditMenu.SetActive(!_creditMenu.activeSelf);
    }

    public void OpenSettingsMenu()
    {
        _settingMenu.SetActive(!_settingMenu.activeSelf);
    }

    public void ActivateLevelMenu()
    {
        _firstDisplayedID = 1;
        _levelsMenu.SetActive(true);
        _arrowButtons.SetActive(true);
    }
    public void ActivateSelectMenu()
    {
        _selectMenu.SetActive(true);
    }

    public void DisplayLevels()
    {
        for (int i = 0; i < 6; i++)
        {
            if (!LevelManager.Instance.DoesLevelExist(_firstDisplayedID + i))
            {
                _levelDisplays[i].gameObject.SetActive(false);
                continue;
            }
            if (!_levelDisplays[i].gameObject.activeSelf) _levelDisplays[i].gameObject.SetActive(true);
            _levelDisplays[i].SetID(_firstDisplayedID + i);
        }
    }

    public void ChangePage(int num)
    {
        if (!LevelManager.Instance.DoesLevelExist(_firstDisplayedID + num * 6)) return;
        _firstDisplayedID += num * 6;
    }
}
