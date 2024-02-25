using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(menuName = "ScriptableObjects/LevelInfo")]
public class LevelInfo : ScriptableObject
{
    [SerializeField] private int _levelID;
    [SerializeField] private int _maxPlayerToSave;
    [SerializeField,Scene] private string _levelAdditiveScene;

    public int LevelID => _levelID;
    public int MaxPlayerToSave => _maxPlayerToSave;
    public string LevelScene => _levelAdditiveScene;

}

public class Shape
{
    [SerializeField] private GameObject _shapePrefab;
    [SerializeField] private int maxCount;
}