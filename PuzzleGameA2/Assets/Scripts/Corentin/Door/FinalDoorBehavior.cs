using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorBehavior : MonoBehaviour
{

    [SerializeField] private TriggerCollisionDetection _triggerCol;


    private void FinalDoorReachedCheck(GameObject gameObject)
    {
        if (gameObject.CompareTag("Player"))
        {
            GameManager.Instance.ChangeGamePhase(Enums.PhaseType.GameEndPhase);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _triggerCol.OnTriggerEnterEvent += FinalDoorReachedCheck;
    }
    private void OnDestroy()
    {
        _triggerCol.OnTriggerEnterEvent -= FinalDoorReachedCheck;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
