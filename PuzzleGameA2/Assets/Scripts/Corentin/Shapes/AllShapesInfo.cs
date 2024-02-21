using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ShapeInfo
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;

    public string Name { get => _name; set => _name = value; }
    public Sprite Sprite { get => _sprite; set => _sprite = value; }
}
[CreateAssetMenu]
public class AllShapesInfo : ScriptableObject
{
    [SerializeField] private List<ShapeInfo> _shapesInfo;
    public List<ShapeInfo> ShapesInfo { get => _shapesInfo; set => _shapesInfo = value; }
}
