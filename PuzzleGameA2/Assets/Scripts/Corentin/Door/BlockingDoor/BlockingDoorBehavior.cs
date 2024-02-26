using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class BlockingDoorBehavior : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private ButtonDoorBehavior _associatedButton;

    [SerializeField] private BoxCollider2D _boxCollider;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Parameters")]

    [SerializeField] private float _openSpeed;
    [SerializeField] private float _closeSpeed;

    [SerializeField] private Color _openColor;
    [SerializeField] private Color _closeColor;

    private Coroutine _openCoroutine;
    private Coroutine _closeCoroutine;

    private bool _isOpen;

    [Button]
    private void OpenDoor()
    {
        if (_closeCoroutine != null)
        {
            StopCoroutine(_closeCoroutine);
            _closeCoroutine = null;
        }
        if (_openCoroutine == null)
        {
            Debug.Log("Open Door");

            _isOpen = true;
            _openCoroutine = StartCoroutine(OpenDoorCoroutine());
        }
    }
    [Button]
    private void CloseDoor()
    {
        if (_openCoroutine != null)
        {
            StopCoroutine(_openCoroutine);
            _openCoroutine = null;
        }
        if (_closeCoroutine == null)
        {
            Debug.Log("Close Door");

            _isOpen = false;
            _closeCoroutine = StartCoroutine(CloseDoorCoroutine());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_associatedButton == null)
        {
            Debug.LogWarning("No Button associated to door");
        }

        _associatedButton.OnButtonPressed += OpenDoor;
        _associatedButton.OnButtonUnpressed += CloseDoor;
    }
    private void OnDestroy()
    {
        _associatedButton.OnButtonPressed -= OpenDoor;
        _associatedButton.OnButtonUnpressed -= CloseDoor;
    }

    IEnumerator OpenDoorCoroutine()
    {
        _boxCollider.enabled = false;
        _spriteRenderer.color = _openColor;

        yield return null;
    }
    IEnumerator CloseDoorCoroutine()
    {
        _boxCollider.enabled = true;
        _spriteRenderer.color = _closeColor;

        yield return null;
    }
}
