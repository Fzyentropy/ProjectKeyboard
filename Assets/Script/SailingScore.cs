using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class SailingScore : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    // Start is called before the first frame update
    void Start()
    {
        resultText.text = "Create the wave and keep pushing!"; //show text on billboard
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1") //if player 1 reach the finishing line first
        {
            resultText.text = "Red wins the game!"; //show player 1 win
            gameObject.SetActive(false); //deactivate finishing line
        }
        if (other.tag == "Player2")//if player 2 reach the finishing line first
        {
            resultText.text = "Blue wins the game!";//show player 2 win
            gameObject.SetActive(false);//deactivate finishing line
        }
    }
}
