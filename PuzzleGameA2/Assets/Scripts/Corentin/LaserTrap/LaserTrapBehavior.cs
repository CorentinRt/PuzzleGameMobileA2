using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserTrapBehavior : MonoBehaviour, IResetable
{
    [SerializeField] private bool _hasInfiniteRange;
    [SerializeField] private float _laserRange;

    [SerializeField] private float _laserVisualDuration;

    [SerializeField] private LineRenderer _lineRenderer;

    private bool _canShoot;

    public Vector3 StartPosition { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void LaserHit(GameObject gameObject)
    {
        _canShoot = false;

        _lineRenderer.gameObject.SetActive(true);

        _lineRenderer.SetPosition(0, transform.position);

        _lineRenderer.SetPosition(1, gameObject.transform.position);

        StartCoroutine(LaserVisualCoroutine());
    }

    private void Start()
    {
        _canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_canShoot)
        {
            RaycastHit2D hit;
            if (_hasInfiniteRange)
            {
                hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), Mathf.Infinity);
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * _laserRange, Color.red, 1f);
            }
            else
            {
                hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), _laserRange);
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * _laserRange, Color.red, 1f);
            }

            if (hit.collider.gameObject.CompareTag("Player"))
            {
                if (hit.collider.gameObject.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehaviour))
                {
                    Debug.Log("laser Hit player");
                    LaserHit(playerBehaviour.gameObject);
                    playerBehaviour.KillPlayer();
                }
            }
        }
    }

    IEnumerator LaserVisualCoroutine()
    {
        yield return new WaitForSeconds(_laserVisualDuration);

        _lineRenderer.gameObject.SetActive(false);

        yield return null;
    }

    public void InitReset()
    {
        StartPosition = transform.parent.position;
    }

    public void ResetActive()
    {
        transform.parent.position = StartPosition;
        _lineRenderer.gameObject.SetActive(false);
    }

    public void Desactive()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
