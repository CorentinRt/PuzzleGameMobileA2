using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiDisplayPhase : MonoBehaviour
{
    [SerializeField] private List<GameObject> _elementsPlayerMovementDisplay;

    [SerializeField] private List<GameObject> _elementsPlacementDisplay;

    private bool _playerElementsDisplaying;
    private bool _placementElementsDisplaying;

    private void Update()
    {
        if (!_placementElementsDisplaying && GameManager.Instance.CurrentPhase == Enums.PhaseType.PlayersMoving)
        {
            _placementElementsDisplaying = true;
            _playerElementsDisplaying = false;

            foreach (GameObject element in _elementsPlayerMovementDisplay)
            {
                element.SetActive(true);
            }
            foreach (GameObject element in _elementsPlacementDisplay)
            {
                element.SetActive(false);
            }
        }
        if (!_playerElementsDisplaying && GameManager.Instance.CurrentPhase == Enums.PhaseType.PlateformePlacement)
        {
            _placementElementsDisplaying = false;
            _playerElementsDisplaying = true;

            foreach (GameObject element in _elementsPlayerMovementDisplay)
            {
                element.SetActive(false);
            }
            foreach (GameObject element in _elementsPlacementDisplay)
            {
                element.SetActive(true);
            }
        }
    }
}
