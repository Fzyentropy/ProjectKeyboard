using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointCollider : MonoBehaviour
{

    private int points;
    public TextMeshProUGUI text;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pawn")
        {
            points++;
            Debug.Log(points.ToString());
        }
    }

    private void FixedUpdate()
    {
        text.text = points.ToString();
    }
}
