using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class ShapeSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform _openTransf;

    [SerializeField] private RectTransform _closeTransf;

    private BoxCollider2D _boxCollider;

    [SerializeField] private Color _semiTransparentColor;
    private Color _notTransparentColor;

    private CanvasGroup _canvasGroup;

    [SerializeField] float _openSpeed;
    [SerializeField] float _closeSpeed;

    private Image _image;
    private Coroutine _openCoroutine;
    private Coroutine _closeCoroutine;

    private bool _isOpen;
    private bool _isClosedTemporary;

    private bool _isForcedClose;


    [Button]
    public void ChangeSelectorState()
    {
        if( _isOpen)
        {
            CloseSelector();
        }
        else
        {
            OpenSelector();
        }
    }
    [Button]
    public void OpenSelector()
    {
        if (_openCoroutine == null && GameManager.Instance.CurrentPhase == Enums.PhaseType.PlateformePlacement)
        {
            if (_closeCoroutine != null)
            {
                StopCoroutine(_closeCoroutine);
                _closeCoroutine = null;

            }
            _isOpen = true;
            _openCoroutine = StartCoroutine(OpenSelectorCoroutine());
        }
    }
    [Button]
    public void CloseSelector()
    {
        if (_closeCoroutine == null)
        {
            if (_openCoroutine != null)
            {
                StopCoroutine(_openCoroutine);
                _openCoroutine = null;

            }
            _isOpen = false;
            _closeCoroutine = StartCoroutine(CloseSelectorCoroutine());
        }
    }
    public void CloseTemporary()
    {
        _isClosedTemporary = true;
        CloseSelector();
    }

    public void ForceCloseSelector()
    {
        _isForcedClose = true;
        CloseSelector();
    }
    public void ForceOpenSelector()
    {
        _isForcedClose = false;
        OpenSelector();
    }
    public void ToggleForcePosition()
    {
        if (_isForcedClose)
        {
            ForceOpenSelector();
        }
        else
        {
            ForceCloseSelector();
        }
    }

    [Button]
    private void ActivateSemiTransparent()
    {
        _canvasGroup.alpha = _semiTransparentColor.a;
    }
    [Button]
    private void DesactiveSemiTransparent()
    {
        _canvasGroup.alpha = 1.0f;
    }
    private void Awake()
    {
        _image = GetComponent<Image>();

        _boxCollider = GetComponent<BoxCollider2D>();

        _canvasGroup = GetComponent<CanvasGroup>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnPhase1Ended += CloseSelector;

        _notTransparentColor = GetComponent<Image>().color;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnPhase1Ended -= CloseSelector;
    }
    // Update is called once per frame
    void Update()
    {
        if (_isClosedTemporary && Input.GetMouseButtonUp(0))
        {
            _isClosedTemporary = false;
            OpenSelector();
        }

        if (GameManager.Instance.CurrentPhase == Enums.PhaseType.PlateformePlacement && !_isOpen && !_isClosedTemporary && !_isForcedClose)
        {
            OpenSelector();
        }
        if (GameManager.Instance.CurrentPhase != Enums.PhaseType.PlateformePlacement && _isOpen)
        {
            CloseSelector();
        }
    }

    IEnumerator OpenSelectorCoroutine()
    {
        while ((_openTransf.localPosition - transform.localPosition).magnitude >= 0.1f)
        {

            //Debug.Log("Opening");
            Vector3 tempVect = transform.localPosition;

            tempVect.y = Mathf.Lerp(tempVect.y, _openTransf.localPosition.y, Time.deltaTime * _openSpeed);

            transform.localPosition = tempVect;

            yield return null;
        }
        transform.localPosition = _openTransf.localPosition;

        _openCoroutine = null;

        yield return null;
    }

    IEnumerator CloseSelectorCoroutine()
    {
        while ((_closeTransf.localPosition - transform.localPosition).magnitude >= 0.1f)
        {
            //Debug.Log("Closing");

            Vector3 tempVect = transform.localPosition;

            tempVect.y = Mathf.Lerp(tempVect.y, _closeTransf.localPosition.y, Time.deltaTime * _closeSpeed);

            transform.localPosition = tempVect;

            yield return null;
        }
        transform.localPosition = _closeTransf.localPosition;

        _closeCoroutine = null;

        yield return null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = _semiTransparentColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = _notTransparentColor;
    }
}
