using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLayout : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Vector3 pos = transform.position;
        Gizmos.DrawLine(pos, new Vector3(-pos.x,pos.y));
        Gizmos.DrawLine(pos, new Vector3(pos.x,-pos.y));
        Gizmos.DrawLine(new Vector3(pos.x,-pos.y),new Vector3(-pos.x,-pos.y));
        Gizmos.DrawLine(new Vector3(-pos.x,pos.y),new Vector3(-pos.x,-pos.y));
    }
}
