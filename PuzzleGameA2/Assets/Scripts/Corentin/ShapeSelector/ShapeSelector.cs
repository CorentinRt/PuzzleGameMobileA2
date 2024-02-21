using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class ShapeSelector : MonoBehaviour
{
    [SerializeField] private RectTransform _openTransf;

    [SerializeField] private RectTransform _closeTransf;

    private BoxCollider2D _boxCollider;

    [SerializeField] private Color _semiTransparentColor;

    [SerializeField] float _openSpeed;
    [SerializeField] float _closeSpeed;

    private Image _image;
    private Coroutine _openCoroutine;
    private Coroutine _closeCoroutine;

    private bool _isOpen;

    [Button]
    public void OpenSelector()
    {
        if (_openCoroutine == null)
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
    private void Awake()
    {
        _image = GetComponent<Image>();

        _boxCollider = GetComponent<BoxCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator OpenSelectorCoroutine()
    {
        while ((_openTransf.position - transform.position).magnitude >= 0.1f)
        {

            Debug.Log("Opening");
            Vector3 tempVect = transform.position;

            tempVect.y = Mathf.Lerp(tempVect.y, _openTransf.position.y, Time.deltaTime * _openSpeed);

            transform.position = tempVect;

            yield return null;
        }
        transform.position = _openTransf.position;

        _openCoroutine = null;

        yield return null;
    }

    IEnumerator CloseSelectorCoroutine()
    {
        while ((_closeTransf.position - transform.position).magnitude >= 0.1f)
        {
            Debug.Log("Closing");

            Vector3 tempVect = transform.position;

            tempVect.y = Mathf.Lerp(tempVect.y, _closeTransf.position.y, Time.deltaTime * _closeSpeed);

            transform.position = tempVect;

            yield return null;
        }
        transform.position = _closeTransf.position;

        _closeCoroutine = null;

        yield return null;
    }
}
