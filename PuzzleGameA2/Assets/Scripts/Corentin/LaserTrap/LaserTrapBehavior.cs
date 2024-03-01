using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserTrapBehavior : ItemsBehaviors, IResetable
{
    [SerializeField] private float _timeBeforeShoot;

    [SerializeField] private bool _hasInfiniteRange;
    [SerializeField] private float _laserRange;

    [SerializeField] private float _laserVisualDuration;

    [SerializeField] private LineRenderer _lineRenderer;

    [SerializeField] private LayerMask _playerLayerMask;

    private bool _canShoot;
    private bool _hasDetected;

    public Vector3 StartPosition { get; set; }

    private void LaserHit(PlayerBehaviour playerBehaviour)
    {
        _canShoot = false;

        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), Mathf.Infinity, ~_playerLayerMask);

        if (hit)
        {
            //Debug.Log("Fire ray on : " + hit.transform.gameObject.name);

            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.green, 5f);

            _lineRenderer.gameObject.SetActive(true);

            _lineRenderer.SetPosition(0, transform.position);

            _lineRenderer.SetPosition(1, hit.point);

            StartCoroutine(LaserVisualCoroutine());
        }
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), Mathf.Infinity);
        if (raycastHit)
        {
            if (raycastHit.transform.CompareTag("Player"))
            {
                playerBehaviour.KillPlayer();
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
    }

    // Update is called once per frame
    void Update()
    {
        if (_canShoot && !_hasDetected)
        {
            RaycastHit2D hit;
            if (_hasInfiniteRange)
            {
                hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), Mathf.Infinity);
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * _laserRange, Color.red, 1f);
            }
            else
            {
                hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), _laserRange);
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * _laserRange, Color.red, 1f);
            }

            if (hit.collider.gameObject.CompareTag("Player"))
            {
                if (hit.collider.gameObject.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehaviour))
                {
                    _hasDetected = true;

                    StartCoroutine(TimeBeforeShootCoroutine(playerBehaviour));
                }
            }
        }

        //Debug.Log("test name : " + this);
    }

    IEnumerator LaserVisualCoroutine()
    {
        yield return new WaitForSeconds(_laserVisualDuration);

        _lineRenderer.gameObject.SetActive(false);

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
