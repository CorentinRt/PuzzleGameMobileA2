using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonOpenShapeSelector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action OnPointerDownEvent;
    public event Action OnPointerUpEvent;

    [SerializeField] private ShapeSelector _shapeSelector;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownEvent?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpEvent?.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        OnPointerDownEvent += _shapeSelector.ChangeSelectorState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
