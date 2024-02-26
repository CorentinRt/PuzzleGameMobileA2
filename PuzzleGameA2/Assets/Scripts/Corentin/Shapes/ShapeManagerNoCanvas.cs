using System.Collections;
using System.Collections.Generic;
using Enums;
using Unity.VisualScripting;
using UnityEngine;

public class ShapeManagerNoCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _visuals;
    [SerializeField] private GameObject _physics;

    [SerializeField] private ShapeType _shapeType;

    [SerializeField] private ShapePower _shapePower;
    
    public ShapePower GetShapePower() => _shapePower;

    public void SetShapePower(ShapePower power) => _shapePower = power;

    private SpriteRenderer _spriteRd;

    [SerializeField] private AllShapesInfo _shapesInfo;

    [SerializeField] private GameObject _collidersContainer;

    private BoxCollider2D _boxCollider;
    private CircleCollider2D _circleCollider;
    private PolygonCollider2D _triangleCollider;

    [SerializeField] private bool _isAffectedByGravity;
    private Rigidbody2D _body2D;

    public ShapeType ShapeType1 { get => _shapeType; set => _shapeType = value; }
    public SpriteRenderer SpriteRd { get => _spriteRd; set => _spriteRd = value; }
    public bool IsAffectedByGravity { get => _isAffectedByGravity; set => _isAffectedByGravity = value; }


    private void DragMode()
    {
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

                _boxCollider.isTrigger = true;
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

                _triangleCollider.isTrigger = true;
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

                _circleCollider.isTrigger = true;
                break;
        }
        //GetComponentInChildren<DragDropNoCanvas>().SetCollider(_shapeType);
        _body2D.gravityScale = 0f;
    }
    private void LockMode()
    {
        if (_isAffectedByGravity)
        {
            ActivateGravity();
        }
        else
        {
            DesactivateGravity();
        }
    }

    private void ActivateGravity()
    {
        _body2D.gravityScale = 1.0f;

        ActiveCollision();
    }
    private void DesactivateGravity()
    {
        _body2D.gravityScale = 1.0f;

        _body2D.constraints = RigidbodyConstraints2D.FreezeAll;

        switch (_shapePower)
        {
            case ShapePower.None:
                ActiveCollision();
                break;
            case ShapePower.ChangeDirection:
                ActiveCollision();
                break;
            case ShapePower.Jump:
                DesactiveCollision();
                break;
            case ShapePower.SideJump:
                DesactiveCollision();
                break;
            case ShapePower.Acceleration:
                DesactiveCollision();
                break;
            case ShapePower.InverseGravity:
                DesactiveCollision();
                break;
            case ShapePower.Mine:
                DesactiveCollision();
                break;
        }
    }

    private void ActiveCollision()
    {
        switch (_shapeType)
        {
            case ShapeType.Square:
                _boxCollider.isTrigger = false;
                break;
            case ShapeType.Triangle:
                _triangleCollider.isTrigger = false;
                break;
            case ShapeType.Circle:
                _circleCollider.isTrigger = false;
                break;
        }
    }
    private void DesactiveCollision()
    {
        switch (_shapeType)
        {
            case ShapeType.Square:
                _boxCollider.isTrigger = true;
                break;
            case ShapeType.Triangle:
                _triangleCollider.isTrigger = true;
                break;
            case ShapeType.Circle:
                _circleCollider.isTrigger = true;
                break;
        }
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
        _spriteRd = _visuals.GetComponent<SpriteRenderer>();

        _boxCollider = _collidersContainer.GetComponent<BoxCollider2D>();
        _triangleCollider = _collidersContainer.GetComponent<PolygonCollider2D>();
        _circleCollider = _collidersContainer.GetComponent<CircleCollider2D>();

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

                _boxCollider.isTrigger = true;
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

                _triangleCollider.isTrigger = true;
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

                _circleCollider.isTrigger = true;
                break;
        }
    }

    private void Awake()
    {
        _spriteRd = _visuals.GetComponent<SpriteRenderer>();

        _body2D = GetComponent<Rigidbody2D>();

        _boxCollider = _collidersContainer.GetComponent<BoxCollider2D>();
        _triangleCollider = _collidersContainer.GetComponent<PolygonCollider2D>();
        _circleCollider = _collidersContainer.GetComponent<CircleCollider2D>();

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

                _boxCollider.isTrigger = true;
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

                _triangleCollider.isTrigger = true;
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

                _circleCollider.isTrigger = true;
                break;
        }
        GetComponentInChildren<DragDropNoCanvas>().SetCollider(_shapeType);
        _body2D.gravityScale = 0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnPhase1Started += DragMode;
        GameManager.Instance.OnPhase1Ended += LockMode;

        switch (_shapePower)
        {
            case ShapePower.Jump:
                _collidersContainer.AddComponent(typeof(JumpPower));
                break;
            case ShapePower.SideJump:
                _collidersContainer.AddComponent(typeof(SideJumpPower));
                break;
            case ShapePower.ChangeDirection:
                _collidersContainer.AddComponent(typeof(ChangeDirectionPower));
                break;
            case ShapePower.Acceleration:
                _collidersContainer.AddComponent(typeof(AccelerationPower));
                break;
            case ShapePower.InverseGravity:
                _collidersContainer.AddComponent(typeof(InverseGravityPower));
                break;
            case ShapePower.Mine:
                _collidersContainer.AddComponent(typeof(MineBehavior));
                break;
        }
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnPhase1Started -= DragMode;
        GameManager.Instance.OnPhase1Ended -= LockMode;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
