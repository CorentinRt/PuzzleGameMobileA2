using System;
using System.Collections;
using Enums;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerBehaviour : MonoBehaviour
{
    [FormerlySerializedAs("speed")] [SerializeField] private float _speed;
    [SerializeField] private float _accelerationSpeed;

    [SerializeField] private float _inverseGravitySpeed;
    [SerializeField] private float _inverseGravityCooldown;
    private Coroutine _inverseGravityCoroutine;

    private bool _isAccelerating;
    [SerializeField] private float _accelerationDuration;
    private Coroutine _accelerationDurationCoroutine;

    private bool _isJumping;
    private bool _canStopJump;

    private bool _isGrounded;

    private bool _isDead;

    [SerializeField] private GameObject _corpseContainer;

    [SerializeField] private GameObject _playerContainer;

    [SerializeField] private CapsuleCollider2D _capsuleColliderPlayer;


    [SerializeField] private GameObject _corpse;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Transform _groundCheckLeft;
    [SerializeField] private Transform _groundCheckRight;
    private Vector3 _startpoint;
    private bool _walking;
    private Rigidbody2D _rb;
    private int _direction; //-1 = left ; 1 = right
    [SerializeField] private float _jumpForce;
    private LevelManager _levelManager;

    [SerializeField] private float _mineCooldown;
    private bool _isWalkingOnCorpse;

    [Space(20)]

    [SerializeField] private PlayersAnimationManager _playersAnimationManager;
    [SerializeField] private Transform _playerVisuals;

    [Space(20)]

    [SerializeField] private UnityEvent OnPlayerChangeDirection;
    [SerializeField] private UnityEvent OnPlayerAccelerate;
    [SerializeField] private UnityEvent OnPlayerJump;
    [SerializeField] private UnityEvent OnPlayerInverseGravity;


    public int Direction { get => _direction; set => _direction = value; }

    private bool _isLanding;
    public bool IsDead { get => _isDead; set => _isDead = value; }

    [Button]
    public void StartWalking() => _walking = true;

    public void SetSpawnpoint(Vector3 spawnpoint) => _startpoint = spawnpoint;
    private void Awake()
    {
        _rb = transform.parent.GetComponent<Rigidbody2D>();
         //_capsuleColliderPlayer = GetComponent<CapsuleCollider2D>();
        _walking = false;
        _direction = 1;
    }

    private void Start()
    {
        if (_startpoint.x < transform.position.x) _direction = -1;
        else _direction = 1;
        Vector3 tempVector = transform.parent.localScale;
        tempVector.x *= _direction;
        transform.parent.localScale = tempVector;
        _playersAnimationManager.OnStopLanding += StopLanding;
    }

    private void StopLanding()
    {
        Debug.Log("hey");
        _isLanding = false;
        _isGrounded = true;
    }

    private void FixedUpdate()
    {
        _isWalkingOnCorpse = false;
        Debug.DrawLine(_groundCheckLeft.position, _groundCheckRight.position, Color.yellow);
        Collider2D groundCheckColl = Physics2D.OverlapArea(_groundCheckLeft.position, _groundCheckRight.position);
        if (groundCheckColl && groundCheckColl != _capsuleColliderPlayer)
        {
            if (groundCheckColl.CompareTag("Floor") || groundCheckColl.CompareTag("Player"))
            {
                _playersAnimationManager.EndMidJumpAnimation();
                
                if (_canStopJump)
                {
                    _isLanding = true;
                    _isJumping = false;
                }
                if (!_isLanding) _isGrounded = true;

                if (groundCheckColl.CompareTag("Player")) _isWalkingOnCorpse = true;
            }
        }
        else
        {
            _playersAnimationManager.StartMidJumpAnimation();
            _isGrounded = false;
        }

        if (_isDead)
        {
            if (_rb.velocity.x!=0) _rb.velocity = new Vector2(0f, _rb.velocity.y);
            return;
        }
        
        if (_isGrounded && !_isJumping)
        {
            if (_walking)
            {
                if (!_isAccelerating)
                {
                    Vector2 velocity = AdjustVelocityToSlope(new Vector2(_speed * _direction * Time.deltaTime, _rb.velocity.y));
                    _rb.velocity = velocity;
                }
                else
                {
                    Vector2 velocity = AdjustVelocityToSlope(new Vector2(_accelerationSpeed * _direction * Time.deltaTime, _rb.velocity.y));
                    _rb.velocity = velocity;
                }
            }
            else if (!_walking)
            {
                if ((_direction == 1 && _startpoint.x > transform.position.x) || (_direction == -1 && _startpoint.x < transform.position.x)) _rb.velocity = new Vector2(_speed * _direction * Time.deltaTime, _rb.velocity.y);
                else _rb.velocity = new Vector2(0, _rb.velocity.y);
            }
        }
        else
        {
            if (!_isJumping)
            {
                _rb.velocity = new Vector2(0f, _rb.velocity.y);
            }
            else
            {
                Debug.Log("Still jumping");
            }
        }

        if (_isWalkingOnCorpse) _rb.velocity = new Vector2(_rb.velocity.x, 0f);
    }
    private void Update()
    {
        if (GameManager.Instance.CurrentPhase == PhaseType.GameEndPhase)
        {
            _playersAnimationManager.PlayWinAnimation();
        }
        if (_rb.velocity.x != 0f)
        {
            _playersAnimationManager.StartRunAnimation();
        }
        else
        {
            _playersAnimationManager.EndRunAnimation();
        }

        if (PlayerManager.Instance.OnePlayerDied)
        {
            _playersAnimationManager.EnableFearAnimation();
        }
        else
        {
            _playersAnimationManager.DisableFearAnimation();
        }
    }

    private Vector3 AdjustVelocityToSlope(Vector3 velocity)
    {
        RaycastHit2D hit;
        if (_rb.gravityScale >= 0f)
        {
            hit = Physics2D.Raycast(transform.position, Vector3.down, 2f, _layer);
            Debug.DrawRay(transform.position, Vector3.down, hit? Color.green : Color.red);
        }
        else
        {
            hit = Physics2D.Raycast(transform.position, Vector3.down, 2f, _layer);
            Debug.DrawRay(transform.position, Vector3.up, hit ? Color.green : Color.red);
        }
        if (hit);
        {
            Debug.DrawRay(transform.position, Vector2.Perpendicular(hit.normal) * -1);
            Debug.DrawRay(transform.position,hit.normal * Vector3.right, Color.black);
            var slopeRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            var adjustVelocity = slopeRotation * velocity;
            if (adjustVelocity.y < 0 && Mathf.Abs(_rb.velocity.x)>0.2)
            {
                return adjustVelocity;
            }
        }
        return velocity;
    }

    public void ChangeDirection()
    {
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        _direction *= -1;
        //Vector3 tempVector = _playerVisuals.localScale;
        Vector3 tempVector = transform.parent.localScale;
        tempVector.x *= -1f;

        //_playerVisuals.localScale = tempVector;
        //transform.localScale = tempVector;
        //_corpseContainer.transform.localScale = tempVector;

        transform.parent.localScale = tempVector;
    }

    private void CreateCorpse()
    {
        _corpseContainer.SetActive(true);
        _rb.mass = 50;
        _playersAnimationManager.PlayDeathBySpikeAnimation();
    }


    public void KillPlayerByLaser()
    {
        _isDead = true;
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        _playersAnimationManager.PlayDeathByLaserAnimation();
        CreateCorpse();

        _corpseContainer.GetComponent<CorpsesBehavior>().DesintagratedByLaser();

        Destroy(gameObject);
    }
    public void KillPlayer()
    {
        //Instantiate(_corpse, transform.position + new Vector3(_direction * 0.5f, -transform.localScale.y / 2, 0), transform.rotation);
        _isDead = true;
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        CreateCorpse();

        Destroy(gameObject);
    }
    public void KillPlayerWithoutCorpses()
    {
        _isDead = true;

        _rb.gravityScale = 0f;

        Destroy(gameObject);
    }
    public void UnloadLevel() => Destroy(gameObject);

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Death"))
        {
            KillPlayer();
        }
        else if (other.gameObject.CompareTag("FinalDoor"))
        {
            GameObject.FindWithTag("GameController").GetComponent<GameManager>().ChangeGamePhase(PhaseType.GameEndPhase);
        }
        else if (other.gameObject.CompareTag("Plateforme"))
        {
            /*
            ShapePower shapePower = other.gameObject.GetComponent<ShapeManagerNoCanvas>().GetShapePower();
            switch (shapePower)
            {
                case ShapePower.None:
                    break;
                case ShapePower.Jump:
                    Jump();
                    break;
                case ShapePower.ChangeDirection:
                    ChangeDirection();
                    break;
                case ShapePower.SideJump:
                    SideJump();
                    break;
                case ShapePower.Acceleration:
                    Acceleration();
                    break;
            }
            */
        }
    }
    public void Jump()
    {
        //_rb.velocity = new Vector2(_rb.velocity.x * _direction, _jumpForce);
        StartCoroutine(JumpCooldown());
        _isJumping = true;
        _rb.velocity = Vector2.zero;
        _rb.AddForce(new Vector2(_jumpForce * _direction, _jumpForce * _rb.gravityScale), ForceMode2D.Impulse);
        Debug.Log("jumping");
    }
    public void SideJump(int dir)
    {
        //_rb.velocity = new Vector2(_jumpForce * dir, _jumpForce);
        _playersAnimationManager.PlayStartJumpAnimation();

        StartCoroutine(JumpCooldown());
        _isJumping = true;
        _rb.velocity = Vector2.zero;
        _rb.AddForce(new Vector2(_jumpForce * dir, _jumpForce * _rb.gravityScale), ForceMode2D.Impulse);
        Debug.Log("Side Jumping");
    }
    public void Acceleration()
    {
        _playersAnimationManager.PlaySpeedBoostAnimation();

        if (_accelerationDurationCoroutine != null)
        {
            StopCoroutine( _accelerationDurationCoroutine );
            _accelerationDurationCoroutine = null;
            _accelerationDurationCoroutine = StartCoroutine(AccelerationDurationCoroutine());
        }
        if (!_isAccelerating)
        {
            _isAccelerating = true;
            _accelerationDurationCoroutine = StartCoroutine(AccelerationDurationCoroutine());
            Debug.Log("Accelerate");
        }
    }
    public void InverseGravity()
    {
        _rb.gravityScale *= -1;

        if (_inverseGravityCoroutine != null)
        {
            StopCoroutine( _inverseGravityCoroutine );
        }
        _inverseGravityCoroutine = StartCoroutine(ChangingGravityCoroutine());

    }

    public void StepOnMine(Vector3 minePos)
    {
        StartCoroutine(CooldownBeforeExplosionMineCoroutine(minePos));
    }

    //Temporary Function
    public void TouchingPlateforme(ShapePower shapePower)
    {
        switch (shapePower)
        {
            case ShapePower.Jump:
                Jump();
                break;
            case ShapePower.ChangeDirection:
                ChangeDirection();
                break;
        }
    }

    IEnumerator ChangingGravityCoroutine()
    {
        float percent = 0f;

        Vector3 scaleY = transform.parent.localScale;

        float targetScaleY = -scaleY.y;

        while (percent < 1f)
        {
            scaleY.y = Mathf.Lerp(scaleY.y, targetScaleY, percent);

            transform.parent.localScale = scaleY;

            percent += Time.deltaTime * _inverseGravitySpeed;

            yield return null;
        }

        transform.parent.localScale = new Vector3(transform.parent.localScale.x, targetScaleY, transform.parent.localScale.z);

        yield return new WaitForSeconds(_inverseGravityCooldown);

        _inverseGravityCoroutine = null;

        yield return null;
    }

    IEnumerator CooldownBeforeExplosionMineCoroutine(Vector3 minePos)
    {
        yield return new WaitForSeconds(_mineCooldown);

        KillPlayer();

        yield return null;
    }
    IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(0.4f);

        _canStopJump = true;

        yield return null;
    }

    IEnumerator AccelerationDurationCoroutine()
    {
        Debug.Log("Start Acceleration");

        yield return new WaitForSeconds(_accelerationDuration);

        Debug.Log("Stop acceleration");
        _isAccelerating = false;

        _playersAnimationManager.PlayIdleAnimation();

        _accelerationDurationCoroutine = null;

        yield return null;
    } 


    public void SetManager(LevelManager levelManager)
    {
        _levelManager = levelManager;
        LevelManager.Instance.GetCurrentLevelController.OnLevelUnload += UnloadLevel;
    }

    private void OnDestroy()
    {
        _levelManager.GetCurrentLevelController.OnLevelUnload -= UnloadLevel;
        _playersAnimationManager.OnStopLanding -= StopLanding;
    }
}
