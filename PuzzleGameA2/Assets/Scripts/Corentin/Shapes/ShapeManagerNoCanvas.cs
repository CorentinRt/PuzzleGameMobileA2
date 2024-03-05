using System.Collections;
using System.Collections.Generic;
using Enums;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

public class ShapeManagerNoCanvas : ItemsBehaviors, IResetable
{
    [SerializeField] private GameObject _visuals;
    [SerializeField] private GameObject _physics;

    [SerializeField] private ShapeType _shapeType;

    [SerializeField] private ShapePower _shapePower;

    [SerializeField] private bool _isLookingLeft;

    private int _direction = 1;
    
    public ShapePower GetShapePower() => _shapePower;


    [SerializeField] private bool _renderWithShapePower;

    private SpriteRenderer _spriteRd;
    [SerializeField] private List<GameObject> _shapesVisuals;

    [SerializeField] private AllShapesInfo _shapesInfo;

    [SerializeField] private GameObject _collidersContainer;

    [SerializeField] private List<Collider2D> _powerColliders;
    [SerializeField] private GameObject _triggersPowerContainer;

    [SerializeField] private Transform _fieldOfView;
    [SerializeField] private Transform _viewVisibilityHandler;

    private BoxCollider2D _boxCollider;
    private CircleCollider2D _circleCollider;
    private PolygonCollider2D _triangleCollider;

    [SerializeField] private bool _isAffectedByGravity;
    private Rigidbody2D _body2D;

    public ShapeType ShapeType1 { get => _shapeType; set => _shapeType = value; }
    public SpriteRenderer SpriteRd { get => _spriteRd; set => _spriteRd = value; }
    public bool IsAffectedByGravity { get => _isAffectedByGravity; set => _isAffectedByGravity = value; }
    public bool IsLookingLeft { get => _isLookingLeft; set => _isLookingLeft = value; }

    public void ChangeDirection()
    {
        if (_isLookingLeft)
        {
            _isLookingLeft = false;
        }
        else
        {
            _isLookingLeft = true;
        }
        _direction *= -1;
    }
    public int GetDirection()
    {
        return _direction;
    }
    private void DragMode()
    {
        if (_renderWithShapePower)
        {
            switch (_shapePower)
            {
                case ShapePower.Mine:

                    for (int i = 0; i < _shapesVisuals.Count; i++)
                    {
                        if (i == 0)
                        {
                            _spriteRd = _shapesVisuals[i].GetComponent<SpriteRenderer>();
                            _shapesVisuals[i].SetActive(true);
                            _powerColliders[i].enabled = true;
                        }
                        else
                        {
                            _shapesVisuals[i].SetActive(false);
                            _powerColliders[i].enabled = false;
                        }
                    }

                    //_boxCollider.enabled = true;
                    //_triangleCollider.enabled = false;
                    //_circleCollider.enabled = false;

                    //_boxCollider.isTrigger = true;
                    break;
                case ShapePower.InverseGravity:

                    for (int i = 0; i < _shapesVisuals.Count; i++)
                    {
                        if (i == 1)
                        {
                            _spriteRd = _shapesVisuals[i].GetComponent<SpriteRenderer>();
                            _shapesVisuals[i].SetActive(true);
                            _powerColliders[i].enabled = true;
                        }
                        else
                        {
                            _shapesVisuals[i].SetActive(false);
                            _powerColliders[i].enabled = false;
                        }
                    }

                    //_triangleCollider.enabled = true;
                    //_boxCollider.enabled = false;
                    //_circleCollider.enabled = false;

                    //_triangleCollider.isTrigger = true;
                    break;
                case ShapePower.ChangeDirection:

                    for (int i = 0; i < _shapesVisuals.Count; i++)
                    {
                        if (i == 2)
                        {
                            _spriteRd = _shapesVisuals[i].GetComponent<SpriteRenderer>();
                            _shapesVisuals[i].SetActive(true);
                            _powerColliders[i].enabled = true;
                        }
                        else
                        {
                            _shapesVisuals[i].SetActive(false);
                            _powerColliders[i].enabled = false;
                        }
                    }

                    //_circleCollider.enabled = true;
                    //_triangleCollider.enabled = false;
                    //_boxCollider.enabled = false;

                    //_circleCollider.isTrigger = true;
                    break;
                case ShapePower.SideJump:

                    for (int i = 0; i < _shapesVisuals.Count; i++)
                    {
                        if (i == 3)
                        {
                            _spriteRd = _shapesVisuals[i].GetComponent<SpriteRenderer>();
                            _shapesVisuals[i].SetActive(true);
                            _powerColliders[i].enabled = true;
                        }
                        else
                        {
                            _shapesVisuals[i].SetActive(false);
                            _powerColliders[i].enabled = false;
                        }
                    }


                    //_circleCollider.enabled = true;
                    //_triangleCollider.enabled = false;
                    //_boxCollider.enabled = false;

                    //_circleCollider.isTrigger = true;
                    break;
                case ShapePower.Acceleration:

                    for (int i = 0; i < _shapesVisuals.Count; i++)
                    {
                        if (i == 4)
                        {
                            _spriteRd = _shapesVisuals[i].GetComponent<SpriteRenderer>();
                            _shapesVisuals[i].SetActive(true);
                            _powerColliders[i].enabled = true;
                        }
                        else
                        {
                            _shapesVisuals[i].SetActive(false);
                            _powerColliders[i].enabled = false;
                        }
                    }

                    //_circleCollider.enabled = true;
                    //_triangleCollider.enabled = false;
                    //_boxCollider.enabled = false;

                    //_circleCollider.isTrigger = true;
                    break;
            }
        }
        else
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

    public void ActivateFieldOfView()
    {
        _fieldOfView.gameObject.SetActive(true);
        _viewVisibilityHandler.gameObject.SetActive(true);
    }
    public void ReverseFieldOfView()
    {
        Vector3 tempVect = _fieldOfView.localScale;
        tempVect.x *= -1;
        _fieldOfView.localScale = tempVect;
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
                DesactiveCollision();
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

    public void SetShapePower(ShapePower power)
    {
        _shapePower = power;

        if (_renderWithShapePower)
        {
            switch (_shapePower)
            {
                case ShapePower.Mine:

                    for (int i = 0; i < _shapesVisuals.Count; i++)
                    {
                        if (i == 0)
                        {

                            _spriteRd = _shapesVisuals[i].GetComponent<SpriteRenderer>();
                            _shapesVisuals[i].SetActive(true);
                            _powerColliders[i].enabled = true;
                        }
                        else
                        {
                            _shapesVisuals[i].SetActive(false);
                            _powerColliders[i].enabled = false;
                        }
                    }
                    break;
                case ShapePower.InverseGravity:

                    for (int i = 0; i < _shapesVisuals.Count; i++)
                    {
                        if (i == 1)
                        {
                            _spriteRd = _shapesVisuals[i].GetComponent<SpriteRenderer>();
                            _shapesVisuals[i].SetActive(true);
                            _powerColliders[i].enabled = true;
                        }
                        else
                        {
                            _shapesVisuals[i].SetActive(false);
                            _powerColliders[i].enabled = false;
                        }
                    }
                    break;
                case ShapePower.ChangeDirection:

                    for (int i = 0; i < _shapesVisuals.Count; i++)
                    {
                        if (i == 2)
                        {
                            _spriteRd = _shapesVisuals[i].GetComponent<SpriteRenderer>();
                            _shapesVisuals[i].SetActive(true);
                            _powerColliders[i].enabled = true;
                        }
                        else
                        {
                            _shapesVisuals[i].SetActive(false);
                            _powerColliders[i].enabled = false;
                        }
                    }
                    break;
                case ShapePower.SideJump:

                    for (int i = 0; i < _shapesVisuals.Count; i++)
                    {
                        if (i == 3)
                        {
                            _spriteRd = _shapesVisuals[i].GetComponent<SpriteRenderer>();
                            _shapesVisuals[i].SetActive(true);
                            _powerColliders[i].enabled = true;
                        }
                        else
                        {
                            _shapesVisuals[i].SetActive(false);
                            _powerColliders[i].enabled = false;
                        }
                    }
                    break;
                case ShapePower.Acceleration:

                    for (int i = 0; i < _shapesVisuals.Count; i++)
                    {
                        if (i == 4)
                        {
                            _spriteRd = _shapesVisuals[i].GetComponent<SpriteRenderer>();
                            _shapesVisuals[i].SetActive(true);
                            _powerColliders[i].enabled = true;
                        }
                        else
                        {
                            _shapesVisuals[i].SetActive(false);
                            _powerColliders[i].enabled = false;
                        }
                    }
                    break;
                case ShapePower.ElectricSphere:


                    break;
            }
    }
}
    public void SetShapeType(ShapeType shapeType)
    {
        _shapeType = shapeType;

        if (!_renderWithShapePower)
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
    }

    private void OnValidate()
    {
        _spriteRd = _visuals.GetComponent<SpriteRenderer>();

        _body2D = GetComponent<Rigidbody2D>();

        _boxCollider = _collidersContainer.GetComponent<BoxCollider2D>();
        _triangleCollider = _collidersContainer.GetComponent<PolygonCollider2D>();
        _circleCollider = _collidersContainer.GetComponent<CircleCollider2D>();

        //switch (_shapeType)
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
        //        _boxCollider.enabled = true;
        //        _triangleCollider.enabled = false;
        //        _circleCollider.enabled = false;

        //        _boxCollider.isTrigger = true;
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
        //        _triangleCollider.enabled = true;
        //        _boxCollider.enabled = false;
        //        _circleCollider.enabled = false;

        //        _triangleCollider.isTrigger = true;
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
        //        _circleCollider.enabled = true;
        //        _boxCollider.enabled = false;
        //        _triangleCollider.enabled = false;

        //        _circleCollider.isTrigger = true;
        //        break;
        //}

        DragMode();
    }

    private void Awake()
    {
        _spriteRd = _visuals.GetComponent<SpriteRenderer>();

        _body2D = GetComponent<Rigidbody2D>();

        _boxCollider = _collidersContainer.GetComponent<BoxCollider2D>();
        _triangleCollider = _collidersContainer.GetComponent<PolygonCollider2D>();
        _circleCollider = _collidersContainer.GetComponent<CircleCollider2D>();

        //switch (_shapeType)
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
        //        _boxCollider.enabled = true;
        //        _triangleCollider.enabled = false;
        //        _circleCollider.enabled = false;

        //        _boxCollider.isTrigger = true;
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
        //        _triangleCollider.enabled = true;
        //        _boxCollider.enabled = false;
        //        _circleCollider.enabled = false;

        //        _triangleCollider.isTrigger = true;
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
        //        _circleCollider.enabled = true;
        //        _triangleCollider.enabled = false;
        //        _boxCollider.enabled = false;

        //        _circleCollider.isTrigger = true;
        //        break;
        //}

        DragMode();

        GetComponentInChildren<DragDropNoCanvas>().SetCollider(_shapeType);
        _body2D.gravityScale = 0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnPhase1Started += DragMode;
        GameManager.Instance.OnPhase1Ended += LockMode;
        LevelManager.Instance.GetCurrentLevelController.OnLevelUnload += DestroySelf;
        Debug.Log((IResetable) this);
        LevelManager.Instance.GetCurrentLevelController.AddToResettableObject<IResetable>(this);

        switch (_shapePower)
        {
            case ShapePower.Jump:
                _triggersPowerContainer.AddComponent(typeof(JumpPower));
                break;
            case ShapePower.SideJump:
                _triggersPowerContainer.AddComponent(typeof(SideJumpPower));
                break;
            case ShapePower.ChangeDirection:
                _triggersPowerContainer.AddComponent(typeof(ChangeDirectionPower));
                break;
            case ShapePower.Acceleration:
                _triggersPowerContainer.AddComponent(typeof(AccelerationPower));
                break;
            case ShapePower.InverseGravity:
                _triggersPowerContainer.AddComponent(typeof(InverseGravityPower));
                break;
            case ShapePower.Mine:
                _triggersPowerContainer.AddComponent(typeof(MineBehavior));
                break;
            case ShapePower.ElectricSphere:
                _triggersPowerContainer.AddComponent(typeof(ElectricSpherePower));
                break;
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnPhase1Started -= DragMode;
        GameManager.Instance.OnPhase1Ended -= LockMode;
        LevelManager.Instance.GetCurrentLevelController.OnLevelUnload -= DestroySelf;
    }

    // Update is called once per frame
    void Update()
    {
        if (_shapePower == ShapePower.SideJump)
        {
            _fieldOfView.position = Vector3.zero;
        }
        Vector3 tempVect = _fieldOfView.localScale;
        if (transform.localScale.y == -1)
        {
            tempVect.y = -1;
            _fieldOfView.localScale = tempVect;
        }
        else
        {
            tempVect.y = 1;
            _fieldOfView.localScale = tempVect;
        }
    }

    public Vector3 StartPosition { get; set; }


    public void InitReset()
    {
        Debug.Log("Init reset");
        StartPosition = transform.position;
    }
    [Button]
    public void ResetActive()
    {
        Debug.Log("Reseting");
        if (_shapePower == ShapePower.InverseGravity)
        {
            Debug.Log("Reset Gravity");
            gameObject.SetActive(true);
            transform.position = StartPosition;
        }
        else if(_shapePower == ShapePower.Mine)
        {
            Debug.Log("Reset Mine");
            _triggersPowerContainer.GetComponent<MineBehavior>().HasExplode = false;
        }
    }
    [Button]
    public void Desactive()
    {
        if (_shapePower == ShapePower.InverseGravity)
        {
            Debug.Log("Desactive gravity");
            gameObject.SetActive(false);
        }
        else if (_shapePower == ShapePower.Mine)
        {
            Debug.Log("Desactive mine");
        }
    }
}