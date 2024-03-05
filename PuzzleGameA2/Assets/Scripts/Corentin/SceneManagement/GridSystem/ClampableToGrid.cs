using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampableToGrid : MonoBehaviour
{
    private void ClampToGrid()
    {
        if (GridManager.Instance != null)
        {
            Vector3Int tempVecInt = GridManager.Instance.GetWorldToCellPosition(transform.position);

            Vector3 cellSize = GridManager.Instance.CellSize;

            transform.position = GridManager.Instance.GetCellToWorldPosition(tempVecInt);
            transform.position += cellSize / 2;
        }
    }

    private void Awake()
    {
        ClampToGrid();
    }
}
