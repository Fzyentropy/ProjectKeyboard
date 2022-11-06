using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// TO Do:
/// 1. higher gravity
public class ArtificialGravity : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float dragMultiplier = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // drag = constant * velocity^2
    void Update()
    {
        rb.drag = dragMultiplier * Mathf.Pow(rb.velocity.y, 2);
    }
}
