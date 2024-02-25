using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ButtonDoorBehavior : MonoBehaviour
{
    [SerializeField] private TriggerCollisionDetection _collisionDetection;

    [SerializeField] private Color _pressedColor;
    [SerializeField] private Color _unpressedColor;

    private int _totalInTrigger;

    public event Action OnButtonPressed;
    public event Action OnButtonUnpressed;

    private void CheckButtonPressed(GameObject gameObject)
    {
        if (gameObject.CompareTag("Player"))
        {
            _totalInTrigger++;
            PressButton();
        }
    }
    private void CheckButtonUnpressed(GameObject gameObject)
    {
        if (gameObject.CompareTag("Player"))
        {
            _totalInTrigger--;
            UnpressButton();
        }
    }
    [Button]
    private void PressButton()
    {
        Debug.Log("Press Button");
        OnButtonPressed?.Invoke();
    }
    [Button]
    private void UnpressButton()
    {
        if (_totalInTrigger == 0f)
        {
            Debug.Log("UnPress Button");
            OnButtonUnpressed?.Invoke();
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
        _collisionDetection.OnTriggerEnterEvent += CheckButtonPressed;
        _collisionDetection.OnTriggerExitEvent += CheckButtonUnpressed;

        ClampToGrid();
    }
    private void OnDestroy()
    {
        _collisionDetection.OnTriggerEnterEvent -= CheckButtonPressed;
        _collisionDetection.OnTriggerExitEvent -= CheckButtonUnpressed;
    }
}
