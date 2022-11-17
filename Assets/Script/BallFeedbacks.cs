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
    // Start is called before the first frame update
    void Start()
    {
        ballRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Key") & transform.position.y < 3.8f)
        {
            kickFeedback?.PlayFeedbacks();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Pole")) hitPoleFeedback?.PlayFeedbacks();
    }
}
