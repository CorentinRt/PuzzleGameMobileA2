using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropNoCanvas : MonoBehaviour
{
    bool _canMove;
    bool _dragging;
    Collider2D _collider;

    Vector2 _lastMousePosition;

    private bool _canDrag;

    public bool CanDrag { get => _canDrag; set => _canDrag = value; }
    public Collider2D Collider { get => _collider; set => _collider = value; }

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
        //_unselectedColor = GetComponent<Image>().color;
        //_image = GetComponent<Image>();
    }

    void Start()
    {
        GameManager.Instance.OnPhase1Started += AllowDrag;
        GameManager.Instance.OnPhase1Ended += DisallowDrag;


        _canMove = false;
        _dragging = false;
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnPhase1Started -= AllowDrag;
        GameManager.Instance.OnPhase1Ended -= DisallowDrag;
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))    // Si appuie
        {
            _lastMousePosition = mousePos;

            if (_collider == Physics2D.OverlapPoint(mousePos))
            {
                _canMove = true;
            }
            else
            {
                _canMove = false;
            }
            if (_canMove && _canDrag)
            {
                _dragging = true;
            }
        }
        if (_dragging)  // Drag
        {
            this.transform.position += (Vector3)mousePos - (Vector3)_lastMousePosition;
        }
        if (Input.GetMouseButtonUp(0))  // Si relache
        {
            _canMove = false;
            _dragging = false;
        }

        _lastMousePosition = mousePos;
    }
}

