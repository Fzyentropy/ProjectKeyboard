using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class AddWaveForce : MonoBehaviour
{
    [SerializeField] private float startTime;
    [SerializeField] private float waveForce;
    private Rigidbody rb;
    private bool canAddForce = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke(nameof(ChangeState), startTime);
    }

    private void ChangeState()
    {
        canAddForce = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canAddForce) WaveForce();
        // Invoke("WaveForce",startTime);
    }

    private void WaveForce()
    {
        if (transform.position.y > 1f)
        {
            rb.AddForce(Vector3.right * waveForce, ForceMode.Impulse);
        }
    }
}
