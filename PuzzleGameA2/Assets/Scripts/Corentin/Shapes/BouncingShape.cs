using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingShape : MonoBehaviour
{
    [SerializeField] float _bouncingForce;

    [SerializeField] float _deadAngle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //CompareTag
        if (collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2D))
        {
            if (rb2D)
            {
                rb2D.AddForce(new Vector2(0f, _bouncingForce));
            }
        }
    }
}
