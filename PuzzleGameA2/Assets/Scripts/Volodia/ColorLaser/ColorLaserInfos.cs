using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ColorLaserInfo
{
    [SerializeField] private LaserColor _laserColor;
    [SerializeField] private Material _laserMaterial;
    [SerializeField] private Color _color;

    public LaserColor LaserColor { get => _laserColor; set => _laserColor = value; }
    public Material Material { get => _laserMaterial; set => _laserMaterial = value; }
    public Color Color { get => _color; set => _color = value; }
}

public enum LaserColor
{
    Purple,
    Orange
}

[CreateAssetMenu]
public class ColorLaserInfos : ScriptableObject
{
    [SerializeField] private List<ColorLaserInfo> _colorLaserInfos;
    public List<ColorLaserInfo> ColorLaserInfosList { get => _colorLaserInfos; set => _colorLaserInfos = value; }
}