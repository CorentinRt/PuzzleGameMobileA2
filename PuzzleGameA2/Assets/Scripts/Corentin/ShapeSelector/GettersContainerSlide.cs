using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GettersContainerSlide : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float _slideMultiplier;
    [SerializeField] private float _goBackToCenterSpeed;

    [SerializeField] private RectTransform _limitLeft;
    [SerializeField] private RectTransform _limitRight;

    private Coroutine _goBackToCenterCoroutine;

    private bool _isSliding;

    private bool _justPress;

    private Vector3 _mousePosition;
    private Vector3 _lastMousePosition;

    public void OnPointerDown(PointerEventData eventData)
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _lastMousePosition = _mousePosition;
        _isSliding = true;
        _justPress = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isSliding = false;

        if (transform.position.x < _limitLeft.position.x || transform.position.x > _limitRight.position.x)
        {
            _goBackToCenterCoroutine = StartCoroutine(GoBackToCenterCoroutine());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _lastMousePosition = _mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_justPress)
        {
            _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            _justPress = false;
        }

        if (_isSliding)
        {
            Vector3 slideDirection = _mousePosition - _lastMousePosition;

            Vector3 tempVector = transform.localPosition;

            tempVector.x += slideDirection.x * _slideMultiplier;

            transform.localPosition = tempVector;
        }


        _lastMousePosition = _mousePosition;
    }

    IEnumerator GoBackToCenterCoroutine()
    {
        Vector3 targetPosition;

        if ((_limitLeft.position - transform.position).magnitude < (_limitRight.position - transform.position).magnitude)
        {
            targetPosition = _limitLeft.position;
        }
        else
        {
            targetPosition = _limitRight.position;
        }

        Vector3 tempVector = transform.position;

        while (!_isSliding && (targetPosition - transform.position).magnitude >= 0.1f)
        {
            Debug.Log("Go Back To center");

            tempVector.x = Mathf.Lerp(tempVector.x, targetPosition.x, Time.deltaTime * _goBackToCenterSpeed);

            transform.position = tempVector;

            yield return null;
        }

        tempVector.x = targetPosition.x;

        transform.position = tempVector;

        _goBackToCenterCoroutine = null;

        yield return null;
    }
}
