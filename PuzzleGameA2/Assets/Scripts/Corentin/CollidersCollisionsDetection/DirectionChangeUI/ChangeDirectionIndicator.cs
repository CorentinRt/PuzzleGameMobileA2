using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirectionIndicator : MonoBehaviour
{
    [SerializeField] private GameObject _changeDirectionIndicator;

    private bool _indicatorOpen;

    [SerializeField] private CircleCollider2D _trigger;

    [SerializeField] private GameObject _associatedShape;

    [SerializeField] private GameObject _associatedVisuals;

    [SerializeField] private LayerMask _shapeIndicatorsLayerMask;

    private void DisplayDirectionIndicator()
    {
        _indicatorOpen = true;

        _changeDirectionIndicator.SetActive(true);
    }
    private void HideDirectionIndicator()
    {
        _indicatorOpen = false;

        _changeDirectionIndicator.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DragDropManager.Instance.CurrentShapeDragged == _associatedShape && (_associatedShape.GetComponent<ShapeManagerNoCanvas>().GetShapePower() == Enums.ShapePower.SideJump || _associatedShape.GetComponent<ShapeManagerNoCanvas>().GetShapePower() == Enums.ShapePower.Acceleration))
        {
            if (!_indicatorOpen)
            {
                DisplayDirectionIndicator();
            }
        }
        else
        {
            if (_indicatorOpen)
            {
                HideDirectionIndicator();
            }
        }

        if (Input.GetMouseButtonDown(0) && Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition), _shapeIndicatorsLayerMask) == _trigger)
        {
            Vector3 tempVector = _associatedVisuals.transform.localScale;
            tempVector.x *= -1;
            _associatedVisuals.transform.localScale = tempVector;

            _associatedShape.GetComponent<ShapeManagerNoCanvas>().ChangeDirection();
        }
    }
}
