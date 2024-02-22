using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShapeGetter : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject _shape;

    private RectTransform _selectorPanel;

    private Vector2 _mousePosition;

    [SerializeField] private ShapeManagerNoCanvas.ShapeType _shapeType;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("ClickDown on getter");

        GameObject tempShape = Instantiate(_shape, _mousePosition, Quaternion.identity);

        _selectorPanel.GetComponent<ShapeSelector>().CloseSelector();

        tempShape.GetComponent<ShapeManagerNoCanvas>().SetShapeType(_shapeType);
        tempShape.GetComponent<DragDropNoCanvas>().Dragging = true;
        tempShape.GetComponent<DragDropNoCanvas>().AllowDrag();
        tempShape.GetComponent<DragDropNoCanvas>().SetCollider(_shapeType);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("ClickUp on getter");
    }

    // Start is called before the first frame update
    void Start()
    {
        _selectorPanel = transform.parent.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
