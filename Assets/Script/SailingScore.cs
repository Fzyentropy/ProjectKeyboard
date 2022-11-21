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
        resultText.text = "Create the wave and keep pushing!";
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            resultText.text = "Red wins the game!";
            gameObject.SetActive(false);
        }
        if (other.tag == "Player2")
        {
            resultText.text = "Blue wins the game!";
            gameObject.SetActive(false);
        }
    }
}
