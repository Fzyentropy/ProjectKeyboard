using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingsScoreA : MonoBehaviour
{
    [HideInInspector] public bool hitA = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other) //if player hit box A, then set bool to true
    {
        if (other.CompareTag("Ball"))
        {
            hitA = true;
        }
    }
}
