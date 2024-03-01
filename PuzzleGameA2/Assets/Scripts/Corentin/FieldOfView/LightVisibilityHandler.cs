using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightVisibilityHandler : MonoBehaviour
{
    [SerializeField] private FieldOfView _fieldOfView;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _fieldOfView.SetOrigin(transform.position);
    }
}
