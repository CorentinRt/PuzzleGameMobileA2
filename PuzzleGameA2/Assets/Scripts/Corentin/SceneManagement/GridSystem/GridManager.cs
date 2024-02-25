using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
//using UnityEditor.Search;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private static GridManager _instance;

    private Grid _grid;
    private Vector3 _cellSize;

    public static GridManager Instance { get => _instance; set => _instance = value; }
    public Vector3 CellSize { get => _cellSize; set => _cellSize = value; }

    public Vector3 GetCellToWorldPosition(Vector3Int cell)
    {
        return _grid.CellToWorld(cell);
    }
    public Vector3Int GetWorldToCellPosition(Vector3 pos)
    {
        return _grid.WorldToCell(pos);
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        _instance = this;

        _grid = GetComponent<Grid>();

        _cellSize = _grid.cellSize;
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
