using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class ShapeManagerNoCanvas : MonoBehaviour
{
    public enum ShapeType
    {
        Square,
        Triangle,
        Circle
    }

    [SerializeField] private ShapeType _shapeType;

    [SerializeField] private ShapePower _shapePower;

    private SpriteRenderer _spriteRd;

    [SerializeField] private AllShapesInfo _shapesInfo;

    private BoxCollider2D _boxCollider;
    private CircleCollider2D _circleCollider;
    private PolygonCollider2D _triangleCollider;

    [SerializeField] private bool _isAffectedByGravity;
    private Rigidbody2D _body2D;

    public ShapeType ShapeType1 { get => _shapeType; set => _shapeType = value; }

    private void ActivateGravity()
    {
        Debug.Log("Dynamic");
        _body2D.bodyType = RigidbodyType2D.Dynamic;
    }

    public void SetShapeType(ShapeType shapeType)
    {
        switch (shapeType)
        {
            case ShapeType.Square:
                _shapeType = ShapeType.Square;
                foreach (var info in _shapesInfo.ShapesInfo)
                {
                    if (info.Name == "Square")
                    {
                        _spriteRd.sprite = info.Sprite;
                        break;
                    }
                }
                _boxCollider.enabled = true;
                _triangleCollider.enabled = false;
                _circleCollider.enabled = false;
                break;
            case ShapeType.Triangle:
                _shapeType = ShapeType.Triangle;
                foreach (var info in _shapesInfo.ShapesInfo)
                {
                    if (info.Name == "Triangle")
                    {
                        _spriteRd.sprite = info.Sprite;
                        break;
                    }
                }
                _triangleCollider.enabled = true;
                _boxCollider.enabled = false;
                _circleCollider.enabled = false;
                break;
            case ShapeType.Circle:
                _shapeType = ShapeType.Circle;
                foreach (var info in _shapesInfo.ShapesInfo)
                {
                    if (info.Name == "Circle")
                    {
                        _spriteRd.sprite = info.Sprite;
                        break;
                    }
                }
                _circleCollider.enabled = true;
                _boxCollider.enabled = false;
                _triangleCollider.enabled = false;
                break;
        }
    }

    private void OnValidate()
    {
        _spriteRd = GetComponent<SpriteRenderer>();

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
                        _spriteRd.sprite = info.Sprite;
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
                        _spriteRd.sprite = info.Sprite;
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
                        _spriteRd.sprite = info.Sprite;
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
        _spriteRd = GetComponent<SpriteRenderer>();

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
                        _spriteRd.sprite = info.Sprite;
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
                        _spriteRd.sprite = info.Sprite;
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
                        _spriteRd.sprite = info.Sprite;
                        break;
                    }
                }
                _circleCollider.enabled = true;
                _triangleCollider.enabled = false;
                _boxCollider.enabled = false;
                break;
        }
        GetComponent<DragDropNoCanvas>().SetCollider(_shapeType);
        _body2D.bodyType = RigidbodyType2D.Kinematic;
        /*
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
        }*/
    }
    // Start is called before the first frame update
    void Start()
    {
        if (_isAffectedByGravity)
        {
            GameManager.Instance.OnPhase1Ended += ActivateGravity;
        }
    }
    private void OnDestroy()
    {
        if(_isAffectedByGravity)
        {
            GameManager.Instance.OnPhase1Ended -= ActivateGravity;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
