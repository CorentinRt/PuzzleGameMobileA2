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

    [SerializeField] private ShapeManagerNoCanvas.ShapeType _shapeType;

    [SerializeField] private ShapePower _shapePower;

    [SerializeField] private bool _isAffectedByGravity;

    private TextMeshProUGUI _shapeCountText;

    public int ShapeCount { get => _shapeCount; set => _shapeCount = value; }
    public ShapePower ShapePower { get => _shapePower; set => _shapePower = value; }
    public ShapeManagerNoCanvas.ShapeType ShapeType { get => _shapeType; set => _shapeType = value; }

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
            tempShape.GetComponent<ShapeManagerNoCanvas>().SetShapePower(_shapePower);
            tempShape.GetComponentInChildren<DragDropNoCanvas>().Dragging = true;
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
            if (shapeManagerNoCanvas.ShapeType1 == _shapeType)
            {
                _shapeCount++;
                _shapeCountText.text = _shapeCount.ToString();
            }
        }
    }

    private void AdaptSprite()
    {
        switch (_shapeType)     // Init right spriteRenderer
        {
            case ShapeManagerNoCanvas.ShapeType.Square:

                foreach (var info in _shapesInfo.ShapesInfo)
                {
                    if (info.Name == "Square")
                    {
                        GetComponent<Image>().sprite = info.Sprite;
                        break;
                    }
                }
                break;
            case ShapeManagerNoCanvas.ShapeType.Triangle:

                foreach (var info in _shapesInfo.ShapesInfo)
                {
                    if (info.Name == "Triangle")
                    {
                        GetComponent<Image>().sprite = info.Sprite;
                        break;
                    }
                }
                break;
            case ShapeManagerNoCanvas.ShapeType.Circle:

                foreach (var info in _shapesInfo.ShapesInfo)
                {
                    if (info.Name == "Circle")
                    {
                        GetComponent<Image>().sprite = info.Sprite;
                        break;
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
    private void OnDestroy()
    {
        TrashShape.Instance.OnGoToTrashEvent -= IncreaseShapeCountFromTrash;
    }
    // Update is called once per frame
    void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
