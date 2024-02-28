using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpsesBehavior : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private float _inverseGravitySpeed;
    [SerializeField] private float _inverseGravityCooldown;
    private Coroutine _inverseGravityCoroutine;
    [SerializeField] private float _jumpForce;

    public Rigidbody2D Rb { get => _rb; set => _rb = value; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    //public void CreateCorpse(Vector3 position)
    //{
    //    transform.position = position;

    //    Debug.Log("Create corpse");
    //}

    private void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        body.AddForce(dir.normalized * explosionForce * wearoff);
    }
    public void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        Debug.Log("jumping");
    }
    public void Bump(Vector3 bumperPosition)
    {
        AddExplosionForce(_rb, 500f, bumperPosition, 5f);
    }
    public void InverseGravity()
    {
        _rb.gravityScale *= -1;

        if (_inverseGravityCoroutine == null)
        {
            _inverseGravityCoroutine = StartCoroutine(ChangingGravityCoroutine());
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
}
