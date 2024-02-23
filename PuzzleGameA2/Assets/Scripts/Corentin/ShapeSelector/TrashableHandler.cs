using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashableHandler : MonoBehaviour
{
    public void DeleteShape()
    {
        Debug.Log("Destroy Shape");
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnDestroy()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
