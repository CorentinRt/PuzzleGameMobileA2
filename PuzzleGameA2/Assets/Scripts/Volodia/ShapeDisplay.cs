using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShapeDisplay : MonoBehaviour
{
    [SerializeField] private AllShapesInfo _shapesInfo;
    [SerializeField] private TextMeshProUGUI _nbShape;
    private Image _spriteRd;
    private void Awake()
    {
        LevelManager.Instance.GetCurrentLevelController.OnLevelUnload += DestroySelf;
        _spriteRd = GetComponent<Image>();
    }

    private void OnDestroy()
    {
        LevelManager.Instance.GetCurrentLevelController.OnLevelUnload -= DestroySelf;
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void SetShape(Shape shape)
    {
        switch (shape.Type)
        {
            case ShapeType.Square:
                foreach (var info in _shapesInfo.ShapesInfo)
                {
                    if (info.Name == "Square")
                    {
                        _spriteRd.sprite = info.Sprite;
                        break;
                    }
                }
                break;
            case ShapeType.Triangle:
                foreach (var info in _shapesInfo.ShapesInfo)
                {
                    if (info.Name == "Triangle")
                    {
                        _spriteRd.sprite = info.Sprite;
                        break;
                    }
                }
                break;
            case ShapeType.Circle:
                foreach (var info in _shapesInfo.ShapesInfo)
                {
                    if (info.Name == "Circle")
                    {
                        _spriteRd.sprite = info.Sprite;
                        break;
                    }
                }
                break;
        }

        _nbShape.text = shape.MaxCount.ToString();
    }
}
