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

    private void ClampToGrid()
    {
        Vector3Int tempVecInt = GridManager.Instance.GetWorldToCellPosition(transform.parent.position);

        Vector3 cellSize = GridManager.Instance.CellSize;

        transform.parent.position = GridManager.Instance.GetCellToWorldPosition(tempVecInt);
        transform.parent.position += cellSize / 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        _associatedButton.OnButtonPressed += OpenDoor;
        _associatedButton.OnButtonUnpressed += CloseDoor;

        ClampToGrid();
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
