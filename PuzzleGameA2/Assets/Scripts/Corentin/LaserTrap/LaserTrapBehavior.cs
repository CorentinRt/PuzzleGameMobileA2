using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserTrapBehavior : ItemsBehaviors, IResetable
{
    [SerializeField] private float _timeBeforeShoot;

    [SerializeField] private bool _hasInfiniteRange;
    [SerializeField] private float _laserRange;

    [SerializeField] private bool _isAlwaysShooting;

    [SerializeField] private float _laserVisualDuration;

    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private List<GameObject> _laserFX;

    [SerializeField] private LayerMask _playerLayerMask;

    [SerializeField] private LayerMask _dragTriggerLayerMask;
    [SerializeField] private LayerMask _indicatorLayerMask;

    private bool _canShoot;
    private bool _hasDetected;
    [SerializeField] private LayerMask _ignoreLayer;

    public Vector3 StartPosition { get; set; }

    private void LaserHit(PlayerBehaviour playerBehaviour)
    {
        _canShoot = false;

        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), Mathf.Infinity, ~(_playerLayerMask + _ignoreLayer + _indicatorLayerMask + _dragTriggerLayerMask));

        if (hit)
        {
            //Debug.Log("Fire ray on : " + hit.transform.gameObject.name);

            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.green, 5f);

            _lineRenderer.gameObject.SetActive(true);

            _lineRenderer.SetPosition(0, transform.position);

            _lineRenderer.SetPosition(1, hit.point);

            foreach (GameObject laserFX in _laserFX)
            {
                laserFX.SetActive(true);
            }

            _laserFX[1].transform.position = hit.point;

            StartCoroutine(LaserVisualCoroutine());
        }
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), Mathf.Infinity, _playerLayerMask);
        if (raycastHit)
        {
            if (raycastHit.transform.CompareTag("Player"))
            {
                playerBehaviour.KillPlayerByLaser();
            }
        }
    }
    private void LaserHitCorpses(CorpsesBehavior corpsesBehavior)
    {
        _canShoot = false;

        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), Mathf.Infinity, ~(_playerLayerMask + _ignoreLayer + _indicatorLayerMask + _dragTriggerLayerMask));

        if (hit)
        {
            //Debug.Log("Fire ray on : " + hit.transform.gameObject.name);

            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.green, 5f);

            _lineRenderer.gameObject.SetActive(true);

            _lineRenderer.SetPosition(0, transform.position);

            _lineRenderer.SetPosition(1, hit.point);
            
            foreach (GameObject laserFX in _laserFX)
            {
                laserFX.SetActive(true);
            }

            _laserFX[1].transform.position = hit.point;

            StartCoroutine(LaserVisualCoroutine());
        }
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), Mathf.Infinity, _playerLayerMask);
        if (raycastHit)
        {
            if (raycastHit.transform.CompareTag("Player"))
            {
                corpsesBehavior.DesintagratedByLaser();
            }
        }
    }

    private void Start()
    {
        StartPosition = transform.parent.position;
        if (GridManager.Instance != null)
        {
            Vector3Int tempVectInt = GridManager.Instance.GetWorldToCellPosition(StartPosition);
            StartPosition = GridManager.Instance.GetCellToWorldPosition(tempVectInt);
            StartPosition += GridManager.Instance.CellSize / 2f;
        }

        _canShoot = true;

        if (LevelManager.Instance.GetCurrentLevelController != null)
        {
            LevelManager.Instance.GetCurrentLevelController.AddToResettableObject<IResetable>(this);
        }
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnPhase2Started += ResetActive;
        }
    }
    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnPhase2Started -= ResetActive;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_canShoot && !_hasDetected && !_isAlwaysShooting)
        {
            RaycastHit2D hit;
            if (_hasInfiniteRange)
            {
                hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), Mathf.Infinity, ~(_ignoreLayer + _indicatorLayerMask + _dragTriggerLayerMask));
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * _laserRange, Color.red, 1f);
            }
            else
            {
                hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), _laserRange,~(_ignoreLayer + _indicatorLayerMask + _dragTriggerLayerMask));
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * _laserRange, Color.red, 1f);
            }

            if (!hit) return;
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                if (hit.collider.gameObject.TryGetComponent<CorpsesBehavior>(out CorpsesBehavior corpsesBehavior))
                {
                    _hasDetected = true;

                    LaserHitCorpses(corpsesBehavior);
                }
                else if (hit.collider.gameObject.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehaviour))
                {
                    _hasDetected = true;

                    StartCoroutine(TimeBeforeShootCoroutine(playerBehaviour));
                }
            }
        }
        else if (_isAlwaysShooting)
        {
            _lineRenderer.gameObject.SetActive(true);
            foreach (GameObject laserFX in _laserFX)
            {
                laserFX.SetActive(true);
            }
            
            RaycastHit2D hit1 = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), Mathf.Infinity);
            if (hit1.collider.gameObject.TryGetComponent<CorpsesBehavior>(out CorpsesBehavior corpsesBehavior))
            {
                LaserHitCorpses(corpsesBehavior);
            }
            if (hit1.collider.gameObject.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehaviour))
            {
                playerBehaviour.KillPlayerByLaser();
            }

            if (hit1) _laserFX[1].transform.position = hit1.point;
            RaycastHit2D hit2 = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), Mathf.Infinity, ~(_playerLayerMask + _ignoreLayer + _indicatorLayerMask + _dragTriggerLayerMask));
            if (hit2)
            {
                _lineRenderer.SetPosition(0, transform.position);

                _lineRenderer.SetPosition(1, hit2.point);
            }
        }
    }

    IEnumerator LaserVisualCoroutine()
    {
        yield return new WaitForSeconds(_laserVisualDuration);

        _lineRenderer.gameObject.SetActive(false);
        
        foreach (GameObject laserFX in _laserFX)
        {
            laserFX.SetActive(false);
        }

        yield return null;
    }

    IEnumerator TimeBeforeShootCoroutine(PlayerBehaviour playerBehaviour)
    {
        yield return new WaitForSeconds(_timeBeforeShoot);

        LaserHit(playerBehaviour);

        yield return null;
    }

    public void InitReset()
    {
        Debug.Log("Init reset laser");
    }

    public void ResetActive()
    {
        Debug.Log("reset laser");
        transform.parent.position = StartPosition;
        _canShoot = true;
        _hasDetected = false;
        _lineRenderer.gameObject.SetActive(false);
    }

    public void Desactive()
    {
        transform.parent.gameObject.SetActive(false);
    }

}
