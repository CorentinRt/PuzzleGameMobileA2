using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ButtonDoorBehavior : MonoBehaviour
{
    [SerializeField] private TriggerCollisionDetection _collisionDetection;

    public event Action OnButtonPressed;
    public event Action OnButtonUnpressed;

    private void CheckButtonPressed(GameObject gameObject)
    {
        if (gameObject.CompareTag("Player"))
        {
            PressButton();
        }
    }
    private void CheckButtonUnpressed(GameObject gameObject)
    {
        if (gameObject.CompareTag("Player"))
        {
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
        Debug.Log("UnPress Button");
        OnButtonUnpressed?.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        _collisionDetection.OnTriggerEnterEvent += CheckButtonPressed;
        _collisionDetection.OnTriggerExitEvent += CheckButtonUnpressed;
    }
    private void OnDestroy()
    {
        _collisionDetection.OnTriggerEnterEvent -= CheckButtonPressed;
        _collisionDetection.OnTriggerExitEvent -= CheckButtonUnpressed;
    }
}
