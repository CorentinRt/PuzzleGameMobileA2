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

    private Sprite _sprite;

    [SerializeField] private AllShapesInfo _shapesInfo;

    private BoxCollider2D _boxCollider;
    private CircleCollider2D _circleCollider;
    private PolygonCollider2D _triangleCollider;

    [SerializeField] private bool _isAffectedByGravity;
    private Rigidbody2D _body2D;


    private void Awake()
    {
        _sprite = GetComponent<Image>().sprite;

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
                        _sprite = info.Sprite;
                        break;
                    }
                }
                _triangleCollider.enabled = false;
                _circleCollider.enabled = false;
                break;
            case ShapeType.Triangle:
                foreach (var info in _shapesInfo.ShapesInfo)
                {
                    if (info.Name == "Triangle")
                    {
                        _sprite = info.Sprite;
                        break;
                    }
                }
                _boxCollider.enabled = false;
                _circleCollider.enabled = false;
                break;
            case ShapeType.Circle:
                foreach (var info in _shapesInfo.ShapesInfo)
                {
                    if (info.Name == "Circle")
                    {
                        _sprite = info.Sprite;
                        break;
                    }
                }
                _triangleCollider.enabled = false;
                _boxCollider.enabled = false;
                break;
        }

        if (!_isAffectedByGravity)
        {
            _body2D.bodyType = RigidbodyType2D.Kinematic;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
