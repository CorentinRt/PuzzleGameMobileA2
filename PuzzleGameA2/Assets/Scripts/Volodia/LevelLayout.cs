using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLayout : MonoBehaviour
{
    [SerializeField] private bool _displayGrid;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Vector3 pos = transform.position;
        Gizmos.DrawLine(pos, new Vector3(-pos.x,pos.y));
        Gizmos.DrawLine(pos, new Vector3(pos.x,-pos.y));
        Gizmos.DrawLine(new Vector3(pos.x,-pos.y),new Vector3(-pos.x,-pos.y));
        Gizmos.DrawLine(new Vector3(-pos.x,pos.y),new Vector3(-pos.x,-pos.y));

        if (!_displayGrid) return;

        for (int x = 0; x < 23; x++)
        {
            Gizmos.DrawLine(transform.position + new Vector3(x, 0f), transform.position + new Vector3(x, 0f) + new Vector3(0f, 10f));
        }
        for (int y = 0; y < 11; y++)
        {
            Gizmos.DrawLine(transform.position + new Vector3(0f, y), transform.position + new Vector3(0f, y) + new Vector3(22f, 0f));
        }
        
    }
}