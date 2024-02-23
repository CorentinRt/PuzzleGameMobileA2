using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDown : MonoBehaviour
{

    public float projectileSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);

    }
}
