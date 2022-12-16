using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class SoccerScore : MonoBehaviour
{
    private float SScore = 0;
    public TextMeshProUGUI ScoreText;
    [SerializeField] private MMF_Player goalFeedback;
    [SerializeField] private GameObject soccer;
    // Start is called before the first frame update
    void Start()
    {
        SScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = SScore.ToString();//send score to billboard
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) //when goal, add score, play feedback, start reset process
        {
            SScore += 1f;
            goalFeedback.PlayFeedbacks();
            StartCoroutine(KickOff());
            soccer.tag = "Untagged";
        }
    }

    IEnumerator KickOff() //reset ball position after 2 seconds
    {
        yield return new WaitForSeconds(2);
        soccer.transform.position = new Vector3(7.5f, 3.5f, -3.5f);
        soccer.tag = "Ball";
        // yield return null;
    }
}
