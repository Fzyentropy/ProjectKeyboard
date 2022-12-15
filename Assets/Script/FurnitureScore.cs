using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class FurnitureScore : MonoBehaviour
{
    private float FScore = 0;
    public float NumberOfBall = 6;

    public TextMeshProUGUI ScoreText; 
    // Start is called before the first frame update
    void Start()
    {
        FScore = NumberOfBall;
    }

    // Update is called once per frame
    void Update()
    {
        // ScoreText.text = FScore.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
            FScore -= 1f;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Ball")
            FScore += 1f;
    }
}
