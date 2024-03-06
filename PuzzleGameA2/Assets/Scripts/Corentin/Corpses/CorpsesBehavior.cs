using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class CorpsesBehavior : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private float _inverseGravitySpeed;
    [SerializeField] private float _inverseGravityCooldown;
    private Coroutine _inverseGravityCoroutine;
    [SerializeField] private float _jumpForce;

    private bool _inMotion;

    private DeathType _deathType;

    private Coroutine _desintegrateCooldownCoroutine;
    [SerializeField] private float _desintegrationCooldown;

    [SerializeField] private UnityEvent OnCorpsesDamage;

    public Rigidbody2D Rb { get => _rb; set => _rb = value; }
    public DeathType DeathType { get => _deathType; set => _deathType = value; }

    private void Awake()
    {
        _rb = transform.parent.GetComponent<Rigidbody2D>();
        LevelManager.Instance.GetCurrentLevelController.OnLevelUnload += DestroySelf;
    }


    public void DesintagratedByLaser()
    {
        if (_desintegrateCooldownCoroutine == null)
        {
            _desintegrateCooldownCoroutine = StartCoroutine(DesintegrateCooldownCoroutine());
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance == null) return;
        LevelManager.Instance.GetCurrentLevelController.OnLevelUnload -= DestroySelf;

        if (_inMotion)
        {
            PlayerManager.Instance.CorpsesInMotionCount--;
        }
    }

    private void Start()
    {
        OnCorpsesDamage?.Invoke();
        
        PlayerManager.Instance.CorpsesInMotionCount++;
        _inMotion = true;
    }
    private void Update()
    {
        if (_rb.velocity == Vector2.zero && _inMotion)
        {
            _inMotion = false;
            PlayerManager.Instance.CorpsesInMotionCount--;
            _rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        else if (_rb.velocity != Vector2.zero && !_inMotion)
        {
            _inMotion = true;
            PlayerManager.Instance.CorpsesInMotionCount++;
        }
    }


    //public void CreateCorpse(Vector3 position)
    //{
    //    transform.position = position;

    //    Debug.Log("Create corpse");
    //}

    public void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce * _rb.gravityScale);
        Debug.Log("jumping");
    }
    public void SideJump(int dir)
    {
        _rb.velocity = new Vector2(_jumpForce * dir, _jumpForce * _rb.gravityScale);
        Debug.Log("Side Jumping");
    }
    public void InverseGravity()
    {
        _rb.gravityScale *= -1;

        if (_inverseGravityCoroutine != null)
        {
            StopCoroutine(_inverseGravityCoroutine);
        }
        _inverseGravityCoroutine = StartCoroutine(ChangingGravityCoroutine());
    }
    IEnumerator ChangingGravityCoroutine()
    {
        float percent = 0f;

        Vector3 scaleY = transform.localScale;

        float targetScaleY = -scaleY.y;

        while (percent < 1f)
        {
            scaleY.y = Mathf.Lerp(scaleY.y, targetScaleY, percent);

            transform.localScale = scaleY;

            percent += Time.deltaTime * _inverseGravitySpeed;

            yield return null;
        }

        transform.localScale = new Vector3(transform.localScale.x, targetScaleY, transform.localScale.z);

        yield return new WaitForSeconds(_inverseGravityCooldown);

        _inverseGravityCoroutine = null;

        yield return null;
    }

    IEnumerator DesintegrateCooldownCoroutine()
    {
        yield return new WaitForSeconds(_desintegrationCooldown);

        Destroy(transform.parent.gameObject);

        yield return null;
    }
}
