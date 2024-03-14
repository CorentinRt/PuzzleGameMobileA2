using System;
using System.Collections.Generic;
using Enums;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


[CreateAssetMenu(menuName = "ScriptableObjects/LevelInfo")]
public class LevelInfo : ScriptableObject
{
    [SerializeField] private int _levelID;
    [SerializeField] private int _maxPlayerToSave;
    [SerializeField] private int _nbPlayerLives;
    [SerializeField,Scene] private string _levelAdditiveScene;
    [SerializeField] private List<Shape> _shapes;
    [SerializeField] private string _levelName;

    public int LevelID => _levelID;
    public string GetName => _levelName;
    public int MaxPlayerToSave => _maxPlayerToSave;
    public int NbPlayerLives => _nbPlayerLives;
    public string LevelScene => _levelAdditiveScene;

    public List<Shape> Shapes => _shapes;

}

[Serializable]
public class Shape
{
    [SerializeField] private ShapePower _power;
    [SerializeField] private ShapeType _type;
    [SerializeField] private int _maxCount;
    [SerializeField] private bool _isLookingLeft;

    public ShapePower Power => _power;
    public ShapeType Type => _type;
    public int MaxCount => _maxCount;
    public bool IsLookingLeft { get => _isLookingLeft; set => _isLookingLeft = value; }
}