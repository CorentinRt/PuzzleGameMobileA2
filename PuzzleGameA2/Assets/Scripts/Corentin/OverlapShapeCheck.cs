using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapShapeCheck : MonoBehaviour
{
    [SerializeField] private DragDropNoCanvas _dragDropNoCanvas;

    [SerializeField] private LayerMask _dragTriggerLayerMask;

    public event Action OnTriggerStayEvent;
    public event Action OnTriggerExitEvent;

    private void Awake()
    {
        OnTriggerStayEvent += _dragDropNoCanvas.SetUnableColor;
        OnTriggerExitEvent += _dragDropNoCanvas.SetAbleColor;

        OnTriggerStayEvent += _dragDropNoCanvas.SetOverlaping;
        OnTriggerExitEvent += _dragDropNoCanvas.UnSetOverlaping;
    }
    private void OnDestroy()
    {
        OnTriggerStayEvent -= _dragDropNoCanvas.SetUnableColor;
        OnTriggerExitEvent -= _dragDropNoCanvas.SetAbleColor;

        OnTriggerStayEvent -= _dragDropNoCanvas.SetOverlaping;
        OnTriggerExitEvent -= _dragDropNoCanvas.UnSetOverlaping;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Plateforme") || collision.CompareTag("Death") || collision.CompareTag("Floor") || collision.CompareTag("FinalDoor") || (collision.CompareTag("Player") && collision.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehavior) && GameManager.Instance.CurrentPhase == Enums.PhaseType.PlateformePlacement))
        {
            if (((1 << collision.gameObject.layer) & _dragTriggerLayerMask) == 0)
            {
                OnTriggerStayEvent?.Invoke();
                Debug.Log(collision.gameObject.name);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Plateforme") || collision.CompareTag("Death") || collision.CompareTag("Floor") || collision.CompareTag("FinalDoor") || (collision.CompareTag("Player") && collision.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehavior) && GameManager.Instance.CurrentPhase == Enums.PhaseType.PlateformePlacement))
        {
            if (((1 << collision.gameObject.layer) & _dragTriggerLayerMask) == 0)
            {
                OnTriggerExitEvent?.Invoke();
            }
        }
    }
}
