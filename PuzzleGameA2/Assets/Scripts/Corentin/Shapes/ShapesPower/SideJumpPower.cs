using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideJumpPower : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Shape Side Jump Player");
            collision.GetComponent<PlayerBehaviour>().SideJump();
        }
    }
}
