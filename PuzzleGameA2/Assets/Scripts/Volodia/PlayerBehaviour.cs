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

    [SerializeField] private GameObject _corpse;
    [SerializeField] private LayerMask _layer;
    private Vector3 _startpoint;
    private bool _walking;
    private Rigidbody2D _rb;
    private int _direction; //-1 = left ; 1 = right
    [SerializeField] private float _jumpForce;

    [SerializeField] private float _mineForce;
    [SerializeField] private float _mineCooldown;
    private bool _tookMine;
    private Vector3 _mineTakenPos;

    [Button]
    public void StartWalking() => _walking = true;

    public void SetSpawnpoint(Vector3 spawnpoint) => _startpoint = spawnpoint;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _walking = false;
        _direction = 1;
    }

    private void FixedUpdate()
    {
        if (!_tookMine)
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
            else if(!_walking && transform.position.x <= _startpoint.x && _rb.velocity.y==0) _rb.velocity = new Vector2(_speed * 1 * Time.deltaTime, _rb.velocity.y);
            else _rb.velocity = new Vector2(0, _rb.velocity.y);
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
        GameObject corpse = Instantiate(_corpse, transform.position + new Vector3(_direction * 0.5f, -transform.localScale.y / 2, 0), transform.rotation);

        Destroy(gameObject);
    }

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
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        Debug.Log("jumping");
    }
    public void SideJump()
    {
        _rb.velocity = new Vector2(_jumpForce * _direction, _jumpForce);
        Debug.Log("Side Jumping");
    }
    public void Acceleration()
    {
        if (!_isAccelerating)
        {
            _isAccelerating = true;
            Debug.Log("Accelerate");
        }
    }
    public void InverseGravity()
    {
        _rb.gravityScale *= -1;

        if (_inverseGravityCoroutine == null)
        {
            _inverseGravityCoroutine = StartCoroutine(ChangingGravityCoroutine());
        }

    }

    public void StepOnMine(Vector3 minePos)
    {
        _mineTakenPos = minePos;
        StartCoroutine(CooldownBeforeExplosionMineCoroutine(minePos));
    }
    private void TakeMineExplosion(Vector3 minePos)
    {
        _tookMine = true;
        AddExplosionForce(_rb, _mineForce, minePos, 5f);
    }

    private void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        body.AddForce(dir.normalized * explosionForce * wearoff);
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

        TakeMineExplosion(minePos);
        KillPlayer();

        yield return null;
    }
}
