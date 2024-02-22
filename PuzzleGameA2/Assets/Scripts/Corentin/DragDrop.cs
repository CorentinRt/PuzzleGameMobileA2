using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDropImage : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas _canvas;

    private RectTransform _rectTransform;

    [SerializeField] private Color _selectedColor;
    private Color _unselectedColor;
    private Image _image;

    private bool _canDrag;

    public bool CanDrag { get => _canDrag; set => _canDrag = value; }

    private void AllowDrag()
    {
        _canDrag = true;
    }
    private void DisallowDrag()
    {
        _canDrag = false;
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _unselectedColor = GetComponent<Image>().color;
        _image = GetComponent<Image>();
    }
    private void Start()
    {
        GameManager.Instance.OnPhase1Started += AllowDrag;
        GameManager.Instance.OnPhase1Ended += DisallowDrag;
    }
    private void OnDestroy()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_canDrag)
        {
            Debug.Log("Begin Drag");
            _image.color = _selectedColor;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_canDrag)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_canDrag)
        {
            Debug.Log("Begin Drag");
            _image.color = _unselectedColor;
        }
    }
}
