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

        GameManager.Instance.OnPhase1Started += ResetTraps;
    }

    private void OnDestroy()
    {
        OnLevelUnload -= ResetResettableList;

        GameManager.Instance.OnPhase1Started -= ResetTraps;
    }

    public void AddToResettableObject<T>(UnityEngine.Object toResetObject) where T : IResetable
    {
        //Debug.Log(toResetObject);
        _elementsToReset.Add(toResetObject as ItemsBehaviors);
    }

    public void ResetResettableList() => _elementsToReset = new List<ItemsBehaviors>();

    public void ResetTraps()
    {
        //foreach (IResetable resettable in _elementsToReset)
        //{
        //    try
        //    {
        //        Debug.Log(resettable);
        //        resettable.ResetActive();
        //    }
        //    catch
        //    {
        //        _elementsToReset.Remove((ItemsBehaviors)resettable);
        //    }
        //}
        for (int i = 0; i < _elementsToReset.Count; i++)
        {
            try
            {
                if (_elementsToReset[i].TryGetComponent<IResetable>(out IResetable resetable))
                {
                    Debug.Log(resetable);
                    resetable.ResetActive();
                }
            }
            catch
            {
                _elementsToReset.Remove(_elementsToReset[i]);
            }
        }
    }
}
