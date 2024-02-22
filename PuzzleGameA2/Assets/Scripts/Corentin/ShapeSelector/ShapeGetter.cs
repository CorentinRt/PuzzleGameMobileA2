using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShapeGetter : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject _shape;

    [SerializeField] private int _shapeCount;

    private RectTransform _selectorPanel;

    private Vector2 _mousePosition;

    [SerializeField] private ShapeManagerNoCanvas.ShapeType _shapeType;

    private TextMeshProUGUI _shapeCountText;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (_shapeCount > 0)
        {
            GameObject tempShape = Instantiate(_shape, _mousePosition, Quaternion.identity);

            //_selectorPanel.GetComponent<ShapeSelector>().CloseSelector();

            tempShape.GetComponentInChildren<ShapeManagerNoCanvas>().SetShapeType(_shapeType);
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

    private void Awake()
    {
        _shapeCountText = GetComponentInChildren<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _selectorPanel = transform.parent.GetComponent<RectTransform>();

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
