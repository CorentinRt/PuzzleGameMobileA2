using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DragDropManager : MonoBehaviour
{
    private static DragDropManager _instance;

    public static DragDropManager Instance { get => _instance; set => _instance = value; }

    [SerializeField] private bool _useGrid;
    private GameObject _currentShapeDragged;

    [SerializeField] private Color _unableDragColor;
    [SerializeField] private Color _ableDragColor;

    private int _unableCount;

    public bool UseGrid { get => _useGrid; set => _useGrid = value; }
    public GameObject CurrentShapeDragged { get => _currentShapeDragged; set => _currentShapeDragged = value; }
    public Color UnableDragColor { get => _unableDragColor; set => _unableDragColor = value; }
    public Color AbleDragColor { get => _ableDragColor; set => _ableDragColor = value; }
    public int UnableCount { get => _unableCount; set => _unableCount = value; }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
