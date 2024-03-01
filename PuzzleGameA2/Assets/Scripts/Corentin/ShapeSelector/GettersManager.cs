using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GettersManager : MonoBehaviour
{
    [SerializeField] private GameObject _shapeGetterPrefab;
    private LevelManager _levelManager;
    private List<Shape> _shapes;
    public event Action ResetGetters;

    private void InitGetters()
    {
        ResetGetters?.Invoke();
        _shapes = _levelManager.GetCurrentLevel().LevelInfo.Shapes;
        GetComponent<HorizontalLayoutGroup>().spacing = -1000 - 100 * (_shapes.Count - 3);
        foreach (Shape shape in _shapes)
        {
            ShapeGetter shapeGetter = Instantiate(_shapeGetterPrefab, transform).GetComponent<ShapeGetter>();
            shapeGetter.Init(shape);
            ResetGetters += shapeGetter.DestroySelf;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        _levelManager = LevelManager.Instance;
        _levelManager.OnLevelFinishedLoad += InitGetters;
    }

    private void OnDestroy()
    {
        _levelManager.OnLevelFinishedLoad -= InitGetters;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
