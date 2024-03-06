using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLaserManager : MonoBehaviour
{
    private static ColorLaserManager _instance;
    public static ColorLaserManager Instance { get => _instance; set => _instance = value; }
    
    private List<LaserGatesBehaviour> _laserList;
    private List<LaserButtonBehaviour> _buttonList;
    private bool _needUpdate;

    public void AddToLaserList(LaserGatesBehaviour laser)
    {
        _laserList.Add(laser);
        _needUpdate = true;
    }

    public void AddToButtonList(LaserButtonBehaviour button)
    {
        _buttonList.Add(button);
        _needUpdate = true;
    } 

    public void ResetLists()
    {
        _laserList = new List<LaserGatesBehaviour>();
        _buttonList = new List<LaserButtonBehaviour>();
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        Debug.Log("yes");

        _laserList = new List<LaserGatesBehaviour>(0);
        _buttonList = new List<LaserButtonBehaviour>(0);
    }

    private void Start()
    {
        if (LevelManager.Instance.GetCurrentLevelController!=null) LevelManager.Instance.GetCurrentLevelController.OnLevelUnload += ResetLists;
    }

    private LaserButtonBehaviour GetButtonOfColor(LaserColor color)
    {
        foreach (LaserButtonBehaviour button in _buttonList)
        {
            if (button.GetColor() == color) return button;
        }

        return null;
    }

    private void Update()
    {
        if (!_needUpdate) return;
        foreach (LaserGatesBehaviour laser in _laserList)
        {
            if (laser.ButtonRelated == null)
            {
                LaserButtonBehaviour button = GetButtonOfColor(laser.GetColor());
                if (button!=null) laser.ButtonRelated = button;
            }
        }

        _needUpdate = false;
    }
}
