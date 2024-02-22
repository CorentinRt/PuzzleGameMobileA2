using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashShape : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event Action OnGoToTrashEvent;

    bool _canTrash;
    bool _justExit;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter trash");
        _canTrash = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _justExit = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && _canTrash)
        {
            Collider2D coll = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (coll != null)
            {
                if (coll.gameObject.TryGetComponent<TrashableHandler>(out TrashableHandler trashable))
                {
                    trashable.DeleteShape();
                }
            }
        }
        if (_justExit == true)
        {
            _canTrash = false;
        }
        _justExit = false;
    }
}
