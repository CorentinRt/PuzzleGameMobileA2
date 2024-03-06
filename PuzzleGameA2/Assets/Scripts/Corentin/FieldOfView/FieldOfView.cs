using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    private Mesh mesh;
    private Vector3 origin;
    [SerializeField] private LayerMask _floorLayerMask;

    [SerializeField] private float _distance;
    [SerializeField] private float fov;

    private bool _isLookingLeft;


    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }


    public void InverseXAxis(bool isLookingLeft)
    {
        _isLookingLeft = isLookingLeft;
    }

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
    }
    // Start is called before the first frame update
    void Update()
    {
        float fov = 90f;
        int rayCount = 80;

        float angle = 0f;

        if (_isLookingLeft)
        {
            if (transform.parent.parent.localScale.y == 1f)
            {
                angle = 180f;
            }
            else
            {
                angle = 270f;
            }
        }
        else
        {
            if (transform.parent.parent.localScale.y == 1f)
            {
                angle = 90f;
            }
            else
            {
                angle = 0f;
            }
        }
        float angleIncrease = fov / rayCount;
        float viewDistance = _distance;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;

        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, _floorLayerMask);
            if (raycastHit2D.collider == null)
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = raycastHit2D.point;
            }
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                
                triangleIndex += 3;
            }

            vertexIndex++;

            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }
}
