using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShapeManager : MonoBehaviour
{
    private enum ShapeType
    {
        Square,
        Triangle,
        Circle
    }

    private enum ShapePower
    {
        Bounce,
        ArrowTrap,
        ReverseMove
    }

    [SerializeField] private ShapeType _shapeType;

    [SerializeField] private ShapePower _shapePower;

    private Image _image;

    [SerializeField] private AllShapesInfo _shapesInfo;

    private BoxCollider2D _boxCollider;
    private CircleCollider2D _circleCollider;
    private PolygonCollider2D _triangleCollider;

    [SerializeField] private bool _isAffectedByGravity;
    private Rigidbody2D _body2D;


    private void ActivateGravity()
    {
        _body2D.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnValidate()
    {
        _image = GetComponent<Image>();

        _boxCollider = GetComponent<BoxCollider2D>();
        _triangleCollider = GetComponent<PolygonCollider2D>();
        _circleCollider = GetComponent<CircleCollider2D>();

        switch (_shapeType)
        {
            case ShapeType.Square:
                foreach (var info in _shapesInfo.ShapesInfo)
                {
                    if (info.Name == "Square")
                    {
                        _image.sprite = info.Sprite;
                        break;
                    }
                }
                _boxCollider.enabled = true;
                _triangleCollider.enabled = false;
                _circleCollider.enabled = false;
                break;
            case ShapeType.Triangle:
                foreach (var info in _shapesInfo.ShapesInfo)
                {
                    if (info.Name == "Triangle")
                    {
                        _image.sprite = info.Sprite;
                        break;
                    }
                }
                _triangleCollider.enabled = true;
                _boxCollider.enabled = false;
                _circleCollider.enabled = false;
                break;
            case ShapeType.Circle:
                foreach (var info in _shapesInfo.ShapesInfo)
                {
                    if (info.Name == "Circle")
                    {
                        _image.sprite = info.Sprite;
                        break;
                    }
                }
                _circleCollider.enabled = true;
                _boxCollider.enabled = false;
                _triangleCollider.enabled = false;
                break;
        }
    }

    private void Awake()
    {
        _image = GetComponent<Image>();

        _body2D = GetComponent<Rigidbody2D>();

        _boxCollider = GetComponent<BoxCollider2D>();
        _triangleCollider = GetComponent<PolygonCollider2D>();
        _circleCollider = GetComponent<CircleCollider2D>();

        switch (_shapeType)
        {
            case ShapeType.Square:
                foreach (var info in _shapesInfo.ShapesInfo)
                {
                    if (info.Name == "Square")
                    {
                        _image.sprite = info.Sprite;
                        break;
                    }
                }
                _boxCollider.enabled = true;
                _triangleCollider.enabled = false;
                _circleCollider.enabled = false;
                break;
            case ShapeType.Triangle:
                foreach (var info in _shapesInfo.ShapesInfo)
                {
                    if (info.Name == "Triangle")
                    {
                        _image.sprite = info.Sprite;
                        break;
                    }
                }
                _triangleCollider.enabled = true;
                _boxCollider.enabled = false;
                _circleCollider.enabled = false;
                break;
            case ShapeType.Circle:
                foreach (var info in _shapesInfo.ShapesInfo)
                {
                    if (info.Name == "Circle")
                    {
                        _image.sprite = info.Sprite;
                        break;
                    }
                }
                _circleCollider.enabled = true;
                _triangleCollider.enabled = false;
                _boxCollider.enabled = false;
                break;
        }
        _body2D.bodyType = RigidbodyType2D.Kinematic;

        if (_isAffectedByGravity)
        {
            GameManager.Instance.OnPhase1Ended += ActivateGravity;
        }

        switch (_shapePower)
        {
            case ShapePower.Bounce:
                //gameObject.AddComponent<>();
                break;
            case ShapePower.ArrowTrap:
                //gameObject.AddComponent<>();
                break;
            case ShapePower.ReverseMove:
                //gameObject.AddComponent<>();
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    // Update is called once per frame
    void Update()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
}
