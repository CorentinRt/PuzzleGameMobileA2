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

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.white;

        //for (int x = 0; x < 23; x++)
        //{
        //    Gizmos.DrawLine(transform.position + new Vector3(x, 0f), transform.position + new Vector3(x, 0f) + new Vector3(0f, 10f));
        //}
        //for (int y = 0; y < 11; y++)
        //{
        //    Gizmos.DrawLine(transform.position + new Vector3(0f, y), transform.position + new Vector3(0f, y) + new Vector3(22f, 0f));
        //}
    }
}
