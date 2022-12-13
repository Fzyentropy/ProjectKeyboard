using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class SumoScore : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    // Start is called before the first frame update
    void Start()
    {
        resultText.text = "Push your opponent out of the keyboard!";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            resultText.text = "Blue wins the game!";
            Debug.Log("P1");
            gameObject.SetActive(false);
        }
        if (other.tag == "Player2")
        {
            resultText.text = "Red wins the game!";
            Debug.Log("P2");
            gameObject.SetActive(false);
        }
    }
}
