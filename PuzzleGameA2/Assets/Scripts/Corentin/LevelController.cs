using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    [SerializeReference] private List<ItemsBehaviors> _elementsToReset = new List<ItemsBehaviors>();
    public Action OnLevelUnload;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    void Start()
    {
        OnLevelUnload += ResetResettableList;
    }

    private void OnDestroy()
    {
        OnLevelUnload -= ResetResettableList;
    }

    public void AddToResettableObject<T>(UnityEngine.Object toResetObject) where T : IResetable
    {
        //Debug.Log(toResetObject);
        _elementsToReset.Add(toResetObject as ItemsBehaviors);
    }

    public void ResetResettableList() => _elementsToReset = new List<ItemsBehaviors>();

    public void ResetTraps()
    {
        foreach (IResetable resettable in _elementsToReset)
        {
            //Debug.Log(resettable);
            resettable.ResetActive();
        }
    }
}
