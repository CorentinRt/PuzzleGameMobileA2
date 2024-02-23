using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerCollisionDetection : MonoBehaviour
{

    public event Action<GameObject> OnTriggerEnterEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            OnTriggerEnterEvent?.Invoke(collision.gameObject);
        }
    }
}
