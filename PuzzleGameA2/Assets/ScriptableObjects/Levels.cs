using System;
using System.Collections.Generic;
using Enums;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(menuName = "ScriptableObjects/LevelInfo")]
public class LevelInfo : ScriptableObject
{
    [SerializeField] private int _levelID;
    [SerializeField] private int _maxPlayerToSave;
    [SerializeField,Scene] private string _levelAdditiveScene;
    [SerializeField] private List<Shape> _shapes;

    public int LevelID => _levelID;
    public int MaxPlayerToSave => _maxPlayerToSave;
    public string LevelScene => _levelAdditiveScene;

    public List<Shape> Shapes => _shapes;

}

[Serializable]
public class Shape
{
    [SerializeField] private ShapePower _power;
    [SerializeField] private ShapeType _type;
    [SerializeField] private int _maxCount;

    public ShapePower Power => _power;
    public ShapeType Type => _type;
    public int MaxCount => _maxCount;
}