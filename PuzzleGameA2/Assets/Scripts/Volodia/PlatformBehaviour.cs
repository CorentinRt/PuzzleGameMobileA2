using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    [SerializeField] private ShapePower _shapePower;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerBehaviour>().TouchingPlateforme(_shapePower);
        }
    }
}

