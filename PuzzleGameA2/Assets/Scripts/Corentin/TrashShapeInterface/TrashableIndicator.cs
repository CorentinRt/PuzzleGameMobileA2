using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TrashableIndicator : MonoBehaviour
{
    [SerializeField] private GameObject _trashIndicator;

    private bool _indicatorOpen;

    [SerializeField] private CircleCollider2D _trigger;

    [SerializeField] private GameObject _associatedShape;

    [SerializeField] private LayerMask _shapeIndicatorsLayerMask;

    private void DisplayTrashIndicator()
    {
        _indicatorOpen = true;

        _trashIndicator.SetActive(true);

        if (_associatedShape.TryGetComponent<ShapeManagerNoCanvas>(out ShapeManagerNoCanvas shapeManagerNoCanvas))
        {
            Vector3 tempVector = _trashIndicator.transform.localPosition;
            if (shapeManagerNoCanvas.GetShapePower() == Enums.ShapePower.ChangeDirection)
            {
                tempVector.y = 2f;
            }
            else
            {
                tempVector.y = 1f;
            }
            _trashIndicator.transform.localPosition = tempVector;
        }
    }
    private void HideTrashIndicator()
    {
        _indicatorOpen = false;

        _trashIndicator.SetActive(false);
    }



    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnDestroy()
    {
        if (!_associatedShape.GetComponentInChildren<DragDropNoCanvas>().IsOverlaping && _associatedShape.GetComponentInChildren<DragDropNoCanvas>().IsUnable)
        {
            GameManager.Instance.OverlapShapeCount--;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (DragDropManager.Instance.CurrentShapeDragged == _associatedShape)
        {
            if (!_indicatorOpen)
            {
                DisplayTrashIndicator();
            }
        }
        else
        {
            if (_indicatorOpen)
            {
                HideTrashIndicator();
            }
        }

        if (Input.GetMouseButtonDown(0) && Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition), _shapeIndicatorsLayerMask) == _trigger)
        {
            TrashShape.Instance.ThrowTrash(_associatedShape);
        }
    }
}
