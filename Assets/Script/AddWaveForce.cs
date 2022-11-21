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
    private Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("WaveForce",startTime);    
    }

    private void WaveForce()
    {
        if (transform.position.y > 1f) ;
        {
            rigid.AddForce(Vector3.right * waveForce, ForceMode.Impulse);
        }
    }
}
