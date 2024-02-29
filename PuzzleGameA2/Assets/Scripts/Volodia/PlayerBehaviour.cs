using System.Collections;
using Enums;
using NaughtyAttributes;
using UnityEngine;
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

    [SerializeField] private GameObject _corpse;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Transform _groundCheckLeft;
    [SerializeField] private Transform _groundCheckRight;
    private Vector3 _startpoint;
    private bool _walking;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _capsuleCollider;
    private int _direction; //-1 = left ; 1 = right
    [SerializeField] private float _jumpForce;
    private LevelManager _levelManager;

    [SerializeField] private float _mineCooldown;

    public int Direction { get => _direction; set => _direction = value; }

    [Button]
    public void StartWalking() => _walking = true;

    public void SetSpawnpoint(Vector3 spawnpoint) => _startpoint = spawnpoint;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _walking = false;
        _direction = 1;
    }

    private void FixedUpdate()
    {
        Debug.DrawLine(_groundCheckLeft.position, _groundCheckRight.position, Color.yellow);
        Collider2D groundCheckColl = Physics2D.OverlapArea(_groundCheckLeft.position, _groundCheckRight.position);
        if (groundCheckColl && groundCheckColl != _capsuleCollider)
        {
            if (groundCheckColl.CompareTag("Floor"))
            {
                _isGrounded = true;
                if (_canStopJump)
                {
                    _canStopJump = false;
                    _isJumping = false;
                }
            }
        }
        else
        {
            _isGrounded = false;
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
            else if (!_walking && transform.position.x < _startpoint.x)
            {
                _rb.velocity = new Vector2(_speed * 1 * Time.deltaTime, _rb.velocity.y);
            }
            else _rb.velocity = new Vector2(0, _rb.velocity.y);
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
    }

    private Vector3 AdjustVelocityToSlope(Vector3 velocity)
    {
        RaycastHit2D hit;
        if (_rb.gravityScale >= 0f)
        {
            hit = Physics2D.Raycast(transform.position, Vector3.down, 1.5f, _layer);
            Debug.DrawRay(transform.position, Vector3.down, hit? Color.green : Color.red);
        }
        else
        {
            hit = Physics2D.Raycast(transform.position, Vector3.down, 1.5f, _layer);
            Debug.DrawRay(transform.position, Vector3.up, hit ? Color.green : Color.red);
        }
        if (hit);
        {
            Debug.DrawRay(transform.position, Vector2.Perpendicular(hit.normal) * -1);
            Debug.DrawRay(transform.position,hit.normal * Vector3.right, Color.black);
            var slopeRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            var adjustVelocity = slopeRotation * velocity;
            if (adjustVelocity.y < 0)
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
    }

    public void KillPlayer()
    {
        Instantiate(_corpse, transform.position + new Vector3(_direction * 0.5f, -transform.localScale.y / 2, 0), transform.rotation);
        
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
        _rb.AddForce(new Vector2(_jumpForce * _direction, _jumpForce), ForceMode2D.Impulse);
        Debug.Log("jumping");
    }
    public void SideJump(int dir)
    {
        //_rb.velocity = new Vector2(_jumpForce * dir, _jumpForce);
        StartCoroutine(JumpCooldown());
        _isJumping = true;
        _rb.velocity = Vector2.zero;
        _rb.AddForce(new Vector2(_jumpForce * dir, _jumpForce), ForceMode2D.Impulse);
        Debug.Log("Side Jumping");
    }
    public void Acceleration()
    {
        if (!_isAccelerating)
        {
            _isAccelerating = true;
            Debug.Log("Accelerate");
        }
        if (_accelerationDurationCoroutine != null)
        {
            _accelerationDurationCoroutine = null;
            _accelerationDurationCoroutine = StartCoroutine(AccelerationDurationCoroutine());
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
        yield return new WaitForSeconds(_accelerationDuration);

        _accelerationDurationCoroutine = null;

        yield return null;
    } 


    public void SetManager(LevelManager levelManager)
    {
        _levelManager = levelManager;
        levelManager.OnLevelUnload += UnloadLevel;
    }

}
