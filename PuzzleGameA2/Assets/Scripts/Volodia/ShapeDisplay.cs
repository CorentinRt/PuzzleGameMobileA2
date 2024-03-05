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

    [SerializeField] private List<GameObject> _powerVisuals;

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
        //switch (shape.Type)
        //{
        //    case ShapeType.Square:
        //        foreach (var info in _shapesInfo.ShapesInfo)
        //        {
        //            if (info.Name == "Square")
        //            {
        //                _spriteRd.sprite = info.Sprite;
        //                break;
        //            }
        //        }
        //        break;
        //    case ShapeType.Triangle:
        //        foreach (var info in _shapesInfo.ShapesInfo)
        //        {
        //            if (info.Name == "Triangle")
        //            {
        //                _spriteRd.sprite = info.Sprite;
        //                break;
        //            }
        //        }
        //        break;
        //    case ShapeType.Circle:
        //        foreach (var info in _shapesInfo.ShapesInfo)
        //        {
        //            if (info.Name == "Circle")
        //            {
        //                _spriteRd.sprite = info.Sprite;
        //                break;
        //            }
        //        }
        //        break;
        //}

        switch (shape.Power)
        {
            case ShapePower.Mine:
                for (int i = 0; i < _powerVisuals.Count; i++)
                {
                    if (i == 0)
                    {
                        _powerVisuals[i].SetActive(true);
                    }
                    else
                    {
                        _powerVisuals[i].SetActive(false);
                    }
                }
                break;
            case ShapePower.InverseGravity:
                for (int i = 0; i < _powerVisuals.Count; i++)
                {
                    if (i == 1)
                    {
                        _powerVisuals[i].SetActive(true);
                    }
                    else
                    {
                        _powerVisuals[i].SetActive(false);
                    }
                }
                break;
            case ShapePower.ChangeDirection:
                for (int i = 0; i < _powerVisuals.Count; i++)
                {
                    if (i == 2)
                    {
                        _powerVisuals[i].SetActive(true);
                    }
                    else
                    {
                        _powerVisuals[i].SetActive(false);
                    }
                }
                break;
            case ShapePower.SideJump:
                for (int i = 0; i < _powerVisuals.Count; i++)
                {
                    if (i == 3)
                    {
                        _powerVisuals[i].SetActive(true);
                    }
                    else
                    {
                        _powerVisuals[i].SetActive(false);
                    }
                }
                break;
            case ShapePower.Acceleration:
                for (int i = 0; i < _powerVisuals.Count; i++)
                {
                    if (i == 4)
                    {
                        _powerVisuals[i].SetActive(true);
                    }
                    else
                    {
                        _powerVisuals[i].SetActive(false);
                    }
                }
                break;
            case ShapePower.ElectricSphere:
                for (int i = 0; i < _powerVisuals.Count; i++)
                {
                    if (i == 5)
                    {
                        _powerVisuals[i].SetActive(true);
                    }
                    else
                    {
                        _powerVisuals[i].SetActive(false);
                    }
                }
                break;
        }

        _nbShape.text = shape.MaxCount.ToString();
    }
}
