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

    [SerializeField] private Transform _buttonVisual;

    [SerializeField] private Transform _pressedTransf;
    private Vector3 _unpressedPosition;

    private bool _isFirstupdate;

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

        _buttonVisual.position = _pressedTransf.position;
    }
    [Button]
    private void UnpressButton()
    {
        if (_totalInTrigger == 0f)
        {
            Debug.Log("UnPress Button");
            OnButtonUnpressed?.Invoke();

            _buttonVisual.position = _unpressedPosition;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _collisionDetection.OnTriggerEnterEvent += CheckButtonPressed;
        _collisionDetection.OnTriggerExitEvent += CheckButtonUnpressed;

        _unpressedPosition = _buttonVisual.transform.position;
    }
    private void OnDestroy()
    {
        _collisionDetection.OnTriggerEnterEvent -= CheckButtonPressed;
        _collisionDetection.OnTriggerExitEvent -= CheckButtonUnpressed;
    }
}
