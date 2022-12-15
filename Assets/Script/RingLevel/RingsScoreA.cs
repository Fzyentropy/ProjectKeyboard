using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingsScoreA : MonoBehaviour
{
    [HideInInspector] public bool hitA = false;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            hitA = true;
        }
    }
}
