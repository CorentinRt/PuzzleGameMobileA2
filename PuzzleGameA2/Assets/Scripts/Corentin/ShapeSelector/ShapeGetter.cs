using System.Collections;
using System.Collections.Generic;
using Enums;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShapeGetter : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject _shape;

    [SerializeField] AllShapesInfo _shapesInfo;

    [SerializeField] private int _shapeCount;

    private RectTransform _selectorPanel;

    private Vector2 _mousePosition;

    [SerializeField] private ShapeType _shapeType;

    [SerializeField] private ShapePower _shapePower;

    [SerializeField] private bool _isLookingLeft;

    [SerializeField] private List<GameObject> _powerVisuals;

    [SerializeField] private bool _isAffectedByGravity;

    private TextMeshProUGUI _shapeCountText;

    [SerializeField] private GameObject _visuals;

    public int ShapeCount { get => _shapeCount; set => _shapeCount = value; }
    public ShapePower ShapePower { get => _shapePower; set => _shapePower = value; }
    public ShapeType ShapeType { get => _shapeType; set => _shapeType = value; }
    public bool IsLookingLeft { get => _isLookingLeft; set => _isLookingLeft = value; }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_shapeCount > 0)
        {
            GameObject tempShape = Instantiate(_shape, _mousePosition, Quaternion.identity);

            _selectorPanel.GetComponent<ShapeSelector>().CloseTemporary();

            tempShape.GetComponentInChildren<ShapeManagerNoCanvas>().SetShapeType(_shapeType);
            if (_isAffectedByGravity)
            {
                tempShape.GetComponentInChildren<ShapeManagerNoCanvas>().IsAffectedByGravity = true;
            }
            if (_isLookingLeft)
            {
                tempShape.GetComponent<ShapeManagerNoCanvas>().ChangeDirection();
                Vector3 tempVect = tempShape.transform.localScale;
                tempVect.x = -1;
                tempShape.transform.localScale = tempVect;
            }
            tempShape.GetComponent<ShapeManagerNoCanvas>().SetShapePower(_shapePower);
            if (_shapePower == ShapePower.SideJump)
            {
                tempShape.GetComponent<ShapeManagerNoCanvas>().ActivateFieldOfView();
                if (_isLookingLeft)
                {
                    tempShape.GetComponent<ShapeManagerNoCanvas>().ReverseFieldOfView();
                }
            }
            tempShape.GetComponentInChildren<DragDropNoCanvas>().Dragging = true;
            DragDropManager.Instance.DraggingNumber++;
            tempShape.GetComponentInChildren<DragDropNoCanvas>().AllowDrag();
            tempShape.GetComponentInChildren<DragDropNoCanvas>().SetCollider(_shapeType);

            DecreaseShapeCount(1);
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    public void DecreaseShapeCount(int value)
    {
        _shapeCount -= value;
        if (_shapeCount < 0)
        {
            _shapeCount = 0;
        }
        _shapeCountText.text = _shapeCount.ToString();
    }
    public void IncreaseShapeCount(int value)
    {
        _shapeCount += value;
        _shapeCountText.text = _shapeCount.ToString();
    }
    private void IncreaseShapeCountFromTrash(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<ShapeManagerNoCanvas>(out ShapeManagerNoCanvas shapeManagerNoCanvas))
        {
            if (shapeManagerNoCanvas.GetShapePower() == ShapePower && shapeManagerNoCanvas.IsLookingLeft == _isLookingLeft)
            {
                IncreaseShapeCount(1);
                _shapeCountText.text = _shapeCount.ToString();
            }
        }
    }

    private void AdaptSprite()
    {
        //switch (_shapeType)     // Init right spriteRenderer
        //{
        //    case ShapeType.Square:

        //        foreach (var info in _shapesInfo.ShapesInfo)
        //        {
        //            if (info.Name == "Square")
        //            {
        //                GetComponent<Image>().sprite = info.Sprite;
        //                break;
        //            }
        //        }
        //        break;
        //    case ShapeType.Triangle:

        //        foreach (var info in _shapesInfo.ShapesInfo)
        //        {
        //            if (info.Name == "Triangle")
        //            {
        //                GetComponent<Image>().sprite = info.Sprite;
        //                break;
        //            }
        //        }
        //        break;
        //    case ShapeType.Circle:

        //        foreach (var info in _shapesInfo.ShapesInfo)
        //        {
        //            if (info.Name == "Circle")
        //            {
        //                GetComponent<Image>().sprite = info.Sprite;
        //                break;
        //            }
        //        }
        //        break;
        //}

        switch(_shapePower)
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
    }

    private void OnValidate()
    {
        AdaptSprite();
    }

    private void Awake()
    {
        _shapeCountText = GetComponentInChildren<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        AdaptSprite();

        _selectorPanel = transform.parent.parent.GetComponent<RectTransform>();

        _shapeCountText.text = _shapeCount.ToString();

        TrashShape.Instance.OnGoToTrashEvent += IncreaseShapeCountFromTrash;
    }

    public void DestroySelf() => Destroy(gameObject);

    public void Init(Shape shape)
    {
        _shapeCount = shape.MaxCount;
        _shapePower = shape.Power;
        _shapeType = shape.Type;
        _isLookingLeft = shape.IsLookingLeft;
        if (_isLookingLeft)
        {
            Vector3 tempVect = _visuals.transform.localScale;
            tempVect.x = -1;
            _visuals.transform.localScale = tempVect;
            //_shapeCountText.transform.localScale = tempVect;
        }
    }

    private void OnDestroy()
    {
        TrashShape.Instance.OnGoToTrashEvent -= IncreaseShapeCountFromTrash;
        transform.parent.GetComponent<GettersManager>().ResetGetters -= DestroySelf;
    }
    // Update is called once per frame
    void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
