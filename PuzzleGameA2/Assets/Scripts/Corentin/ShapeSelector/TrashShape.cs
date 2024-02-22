using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashShape : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private static TrashShape instance;

    public event Action<GameObject> OnGoToTrashEvent;

    bool _canTrash;
    bool _justExit;

    public static TrashShape Instance { get => instance; set => instance = value; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _canTrash = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _justExit = true;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && _canTrash && DragDropManager.Instance.CurrentShapeDragged != null)
        {
            OnGoToTrashEvent?.Invoke(DragDropManager.Instance.CurrentShapeDragged);
            Destroy(DragDropManager.Instance.CurrentShapeDragged);
        }
        if (_justExit == true)
        {
            _canTrash = false;
        }
        _justExit = false;
    }
}
