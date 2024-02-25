using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerCollisionDetection : MonoBehaviour
{

    public event Action<GameObject> OnTriggerEnterEvent;

    public event Action<GameObject> OnTriggerExitEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            OnTriggerEnterEvent?.Invoke(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            OnTriggerExitEvent?.Invoke(collision.gameObject);
        }
    }
}
