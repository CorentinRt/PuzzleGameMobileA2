using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _shapesInLevel;


    private void DisplayShapesInSelector()
    {
        // Display shapes in selector in ui
    }

    // Start is called before the first frame update
    void Start()
    {
        DisplayShapesInSelector();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
