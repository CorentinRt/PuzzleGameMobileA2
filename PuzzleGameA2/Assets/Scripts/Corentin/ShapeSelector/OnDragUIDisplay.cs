using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDragUIDisplay : MonoBehaviour
{
    [SerializeField] private List<GameObject> _elementsAffected;

    private bool _isDisplaying;



    private void Display()
    {
        _isDisplaying = true;
        foreach (GameObject elements in _elementsAffected)
        {
            elements.SetActive(true);
        }
    }
    private void UnDisplay()
    {
        _isDisplaying = false;
        foreach (GameObject elements in _elementsAffected)
        {
            elements.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _isDisplaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CurrentPhase == Enums.PhaseType.PlateformePlacement && DragDropManager.Instance.DraggingNumber == 0f && !_isDisplaying)
        {
            Display();
        }
        else if (GameManager.Instance.CurrentPhase == Enums.PhaseType.PlateformePlacement && DragDropManager.Instance.DraggingNumber != 0f && _isDisplaying)
        {
            UnDisplay();
        }
    }
}
