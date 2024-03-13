using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class LaserGatesBehaviour : ItemsBehaviors
{
    [SerializeField, OnValueChanged("UpdateVisuals")] private LaserColor _laserColor;
    
    [SerializeField] private ColorLaserInfos _colorLaserInfos;

    [SerializeField] private LayerMask _dragTriggerLayerMask;
    [SerializeField] private LayerMask _indicatorLayerMask;
    private void UpdateVisuals()
    {
        Color color = Color.white;
        Material material = _colorLaserInfos.ColorLaserInfosList[0].Material;
        foreach (ColorLaserInfo info in _colorLaserInfos.ColorLaserInfosList)
        {
            if (info.LaserColor == _laserColor)
            {
                color = info.Color;
                material = info.Material;
            }
        }

        foreach (ParticleSystem particle  in _laserParticules)
        {
            particle.startColor = color;
        }
        _laserSprite.color = color;
        _lineRenderer.material = material;
    }
    
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LaserButtonBehaviour _buttonRelated;

    [SerializeField] private GameObject _endFX;
    [SerializeField] private List<ParticleSystem> _laserParticules;

    public LaserButtonBehaviour ButtonRelated
    {
        get => _buttonRelated;
        set
        {
            _buttonRelated = value;
            _buttonRelated.OnButtonPressed += Desactive;
            _buttonRelated.OnButtonUnpressed += Active;
        }
    }

    private bool _isActive;
    [SerializeField] private SpriteRenderer _laserSprite;
    [SerializeField] private LayerMask _ignoreLayer;

    public Vector3 StartPosition { get; set; }
    
    private void Start()
    {
        UpdateVisuals();
        Active();
        if (ColorLaserManager.Instance!=null) ColorLaserManager.Instance.AddToLaserList(this);
        
        StartPosition = transform.parent.position;
        if (GridManager.Instance != null)
        {
            Vector3Int tempVectInt = GridManager.Instance.GetWorldToCellPosition(StartPosition);
            StartPosition = GridManager.Instance.GetCellToWorldPosition(tempVectInt);
            StartPosition += GridManager.Instance.CellSize / 2f;
        }

        if (LevelManager.Instance.GetCurrentLevelController != null)
        {
            LevelManager.Instance.GetCurrentLevelController.AddToResettableObject<IResetable>(this);
        }
    }
    private void OnDestroy()
    {
        if (_buttonRelated == null) return;
        _buttonRelated.OnButtonPressed -= Desactive;
        _buttonRelated.OnButtonUnpressed -= Active;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isActive)
        {
            _lineRenderer.gameObject.SetActive(false);
            _endFX.SetActive(false);
            return;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), Mathf.Infinity, ~(_ignoreLayer + _dragTriggerLayerMask + _indicatorLayerMask));
        if (!hit) return;
        Debug.Log("Object hit : " + hit.transform.gameObject.name);
        if (hit.collider.gameObject.TryGetComponent<CorpsesBehavior>(out CorpsesBehavior corpsesBehavior))
        {
            corpsesBehavior.DesintagratedByLaser();
        }
        if (hit.collider.gameObject.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehaviour))
        {
            playerBehaviour.KillPlayerByLaser();
        }
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, hit.point);
        _lineRenderer.gameObject.SetActive(true);
        _endFX.transform.position = hit.point;
        _endFX.SetActive(_laserSprite.color.a >= 1f);
    }


    [Button]
    public void Desactive()
    {
        _isActive = false;
    }
    
    [Button]
    public void Active()
    {
        _isActive = true;
    }

    public LaserColor GetColor()
    {
        return _laserColor;
    }
    
    //Reset lineRenderer pos

    private void OnDrawGizmosSelected()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), Mathf.Infinity);
        if (!hit) return;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, hit.point);
    }
}
