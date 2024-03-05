using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

public class LaserGatesBehaviour : ItemsBehaviors
{

    [SerializeField] private LineRenderer _lineRenderer;

    private bool _isActive;

    public Vector3 StartPosition { get; set; }
   

    private void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isActive)
        {
            _lineRenderer.gameObject.SetActive(false);
            return;
        }
        Debug.Log("test");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), Mathf.Infinity);
        if (hit.collider.gameObject.TryGetComponent<CorpsesBehavior>(out CorpsesBehavior corpsesBehavior))
        {
            corpsesBehavior.DesintagratedByLaser();
        }
        if (hit.collider.gameObject.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehaviour))
        {
            playerBehaviour.KillPlayer();
        }
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, hit.point);
        _lineRenderer.gameObject.SetActive(true);
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
    
    

}
