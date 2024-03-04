using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _levelsMenu;
    [SerializeField] private GameObject _homeMenu;
    [SerializeField] private GameObject _selectMenu;

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
    
}
