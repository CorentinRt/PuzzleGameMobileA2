using System.Collections;
using System.Collections.Generic;
using Enums;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropNoCanvas : MonoBehaviour
{
    bool _canMove;
    bool _dragging;
    //Collider2D _collider;

    //[SerializeField] private GameObject _collidersContainer;

    [SerializeField] private CircleCollider2D _dragtrigger;    
    [SerializeField] private LayerMask _dragLayerMask;
    [SerializeField] private LayerMask _groundLayerMask;

    private bool _isUnable;

    private bool _isOverlaping;

    private bool _canDrag;

    public bool CanDrag { get => _canDrag; set => _canDrag = value; }
    // public Collider2D Collider { get => _collider; set => _collider = value; }
    public bool Dragging { get => _dragging; set => _dragging = value; }

    public void SetOverlaping()
    {
        _isOverlaping = true;
    }
    public void UnSetOverlaping()
    {
        _isOverlaping = false;
    }

    public void AllowDrag()
    {
        _canDrag = true;
    }
    public void DisallowDrag()
    {
        _canDrag = false;
    }

    public void SetCollider(ShapeType shapeType)
    {
        //switch (shapeType)
        //{
        //    case ShapeManagerNoCanvas.ShapeType.Circle:
        //        _collider = _collidersContainer.GetComponent<CircleCollider2D>();
        //        break;
        //    case ShapeManagerNoCanvas.ShapeType.Square:
        //        _collider = _collidersContainer.GetComponent<BoxCollider2D>();
        //        break;
        //    case ShapeManagerNoCanvas.ShapeType.Triangle:
        //        _collider = _collidersContainer.GetComponent<PolygonCollider2D>();
        //        break;
        //}
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
            if (_dragtrigger == Physics2D.OverlapPoint(mousePos, _dragLayerMask))
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
            DragDropManager.Instance.CurrentShapeDragged = gameObject.transform.parent.gameObject;
            if (DragDropManager.Instance.UseGrid)
            {
                Vector3Int tempVect = Vector3Int.zero;

                Vector3 cellCize = GridManager.Instance.CellSize;

                tempVect = GridManager.Instance.GetWorldToCellPosition((Vector3)mousePos);
                gameObject.transform.parent.position = GridManager.Instance.GetCellToWorldPosition(tempVect);
                gameObject.transform.parent.position += cellCize/2f;
            }
            else
            {
                gameObject.transform.parent.position = (Vector3)mousePos;
            }
        }
        if (Input.GetMouseButtonUp(0))  // Si relache
        {
            if (transform.parent.gameObject == DragDropManager.Instance.CurrentShapeDragged && transform.parent.gameObject.TryGetComponent<ShapeManagerNoCanvas>(out ShapeManagerNoCanvas shapeManagerNoCanvas))
            {
                shapeManagerNoCanvas.InitReset();
            }
            _canMove = false;
            _dragging = false;
        }

        if (!_isUnable)
        {
            if (DragDropManager.Instance.CurrentShapeDragged == transform.parent.gameObject)
            {
                if (transform.parent.gameObject.TryGetComponent<ShapeManagerNoCanvas>(out ShapeManagerNoCanvas shapeManagerNoCanvas))
                {
                    shapeManagerNoCanvas.SpriteRd.color = DragDropManager.Instance.SelectedDragColor;
                }
            }
            else
            {
                if (transform.parent.gameObject.TryGetComponent<ShapeManagerNoCanvas>(out ShapeManagerNoCanvas shapeManagerNoCanvas))
                {
                    shapeManagerNoCanvas.SpriteRd.color = DragDropManager.Instance.AbleDragColor;
                }
            }
        }

        if (transform.parent.gameObject.TryGetComponent<ShapeManagerNoCanvas>(out ShapeManagerNoCanvas shapeManagerNoCanvas1) && !_isOverlaping)
        {
            if (shapeManagerNoCanvas1.GetShapePower() == ShapePower.Mine || shapeManagerNoCanvas1.GetShapePower() == ShapePower.Jump)
            {
                if (CheckGroundUnder(transform))
                {
                    SetAbleColor();
                }
                else
                {
                    SetUnableColor();
                }
            }
        }
    }

    public void SetUnableColor()
    {
        if (transform.parent.gameObject.TryGetComponent<ShapeManagerNoCanvas>(out ShapeManagerNoCanvas shapeManagerNoCanvas) && !_isUnable)
        {
            _isUnable = true;
            shapeManagerNoCanvas.SpriteRd.color = DragDropManager.Instance.UnableDragColor;
            GameManager.Instance.OverlapShapeCount++;
        }
    }
    public void SetAbleColor()
    {
        if (transform.parent.gameObject.TryGetComponent<ShapeManagerNoCanvas>(out ShapeManagerNoCanvas shapeManagerNoCanvas) && _isUnable)
        {
            _isUnable = false;
            shapeManagerNoCanvas.SpriteRd.color = DragDropManager.Instance.AbleDragColor;
            GameManager.Instance.OverlapShapeCount--;
        }
    }

    private bool CheckGroundUnder(Transform transf)
    {
        Vector2 checkPos = transf.position;
        checkPos.y -= GridManager.Instance.CellSize.y;

        if (Physics2D.OverlapPoint(checkPos, _groundLayerMask))
        {
            return true;
        }

        return false;
    }
}