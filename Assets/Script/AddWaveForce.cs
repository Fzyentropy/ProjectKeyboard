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
        //get boat Rigidbody
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //start adding magic wave force after several seconds
        Invoke("WaveForce",startTime);    
    }

    private void WaveForce()
    {
        //push boat right if the boat is popped up high enough
        if (transform.position.y > 1f) ;
        {
            rigid.AddForce(Vector3.right * waveForce, ForceMode.Impulse);
        }
    }
}
