using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileArrow : MonoBehaviour
{
    //Arrow speed
    public float arrowSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Arrow movement
        transform.Translate(Vector3.up * arrowSpeed * Time.deltaTime);


    }
}
