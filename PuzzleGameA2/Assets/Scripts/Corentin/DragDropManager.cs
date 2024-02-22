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

    public bool UseGrid { get => _useGrid; set => _useGrid = value; }
    public GameObject CurrentShapeDragged { get => _currentShapeDragged; set => _currentShapeDragged = value; }

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
