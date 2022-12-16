using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;    
using UnityEngine.SceneManagement;

public class Utils : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timeLimit = 30;
    private float timeLeft;
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timeLimit; //reset timer
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    public void Timer()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = "Time left: " + Mathf.FloorToInt(timeLeft); //show timer on billboard
        if (timeLeft <= 0f)
        {
            timerText.text = "Time's Up!"; //if timer goes to 0, show time's up
        }
    }
}


