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

    private bool _canDrag;

    public bool CanDrag { get => _canDrag; set => _canDrag = value; }
    public Collider2D Collider { get => _collider; set => _collider = value; }
    public bool Dragging { get => _dragging; set => _dragging = value; }

    public void AllowDrag()
    {
        _canDrag = true;
    }
    public void DisallowDrag()
    {
        _canDrag = false;
    }

    public void SetCollider(ShapeManagerNoCanvas.ShapeType shapeType)
    {
        switch (shapeType)
        {
            case ShapeManagerNoCanvas.ShapeType.Circle:
                _collider = GetComponent<CircleCollider2D>();
                break;
            case ShapeManagerNoCanvas.ShapeType.Square:
                _collider = GetComponent<BoxCollider2D>();
                break;
            case ShapeManagerNoCanvas.ShapeType.Triangle:
                _collider = GetComponent<PolygonCollider2D>();
                break;
        }
    }

    private void Awake()
    {

    }

    void Start()
    {
        GameManager.Instance.OnPhase1Started += AllowDrag;
        GameManager.Instance.OnPhase1Ended += DisallowDrag;


        _canMove = false;
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
            DragDropManager.Instance.CurrentShapeDragged = gameObject;
            if (DragDropManager.Instance.UseGrid)
            {
                Vector3Int tempVect = Vector3Int.zero;

                Vector3 cellCize = GridManager.Instance.CellSize;

                tempVect = GridManager.Instance.GetWorldToCellPosition((Vector3)mousePos);
                this.transform.position = GridManager.Instance.GetCellToWorldPosition(tempVect);
                this.transform.position += cellCize/2f;
            }
            else
            {
                this.transform.position = (Vector3)mousePos;
            }
        }
        if (Input.GetMouseButtonUp(0))  // Si relache
        {
            _canMove = false;
            _dragging = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            GetComponent<SpriteRenderer>().color = DragDropManager.Instance.UnableDragColor;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponent<SpriteRenderer>().color = DragDropManager.Instance.AbleDragColor;
    }
}