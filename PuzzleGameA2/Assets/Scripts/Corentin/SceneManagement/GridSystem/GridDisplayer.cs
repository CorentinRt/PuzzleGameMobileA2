using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject _gridVisual;



    private void Start()
    {
        
        _gridVisual.SetActive(false);
    }

    private void Update()
    {
        //if (GameManager.Instance != null)
        //{
        //    if (GameManager.Instance.CurrentPhase == Enums.PhaseType.PlateformePlacement)
        //    {
        //        _gridVisual.SetActive(true);
        //    }
        //    else
        //    {
        //        _gridVisual.SetActive(false);
        //    }
        //}
    }
}
