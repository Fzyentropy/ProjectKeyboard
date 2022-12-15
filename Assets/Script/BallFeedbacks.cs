using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class BallFeedbacks : MonoBehaviour
{
    private Rigidbody ballRb;
    public MMF_Player hitPoleFeedback;
    public MMF_Player landFeedback;
    public MMF_Player kickFeedback;
    public MMF_Player goalFeedback;
    // Start is called before the first frame update
    void Start()
    {
        ballRb = GetComponent<Rigidbody>();
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Key"))
        {
            kickFeedback.PlayFeedbacks();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Pole")) hitPoleFeedback?.PlayFeedbacks();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftScoreField") || other.CompareTag("RightScoreField"))
        {
            goalFeedback.PlayFeedbacks();
        }
    }
}
