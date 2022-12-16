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
        resultText.text = "Push your opponent out of the keyboard!"; //show text on billboard
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1") //if player 1 touches ground first, show player 2 win, diactivate trigger box
        {
            resultText.text = "Blue wins the game!";
            Debug.Log("P1");
            gameObject.SetActive(false);
        }
        if (other.tag == "Player2") //if player 2 touches ground first, show player 2 win, diactivate trigger box
        {
            resultText.text = "Red wins the game!";
            Debug.Log("P2");
            gameObject.SetActive(false);
        }
    }
}
