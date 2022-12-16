using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitGroundReset : MonoBehaviour
{
    [SerializeField] private GameObject footBall;
    private bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") & hit == false)
        {
            StartCoroutine(Kickoff()); //if ball hit the ground outside keyboard, start “kick off"
            hit = true; //disable the reset for a while
            footBall.tag = "Untagged"; //disable the reset for a while
        }
    }

    IEnumerator Kickoff()
    {
        yield return new WaitForSeconds(1); //wait for 1 second
        footBall.transform.position = new Vector3(7.5f, 3.5f, -3.5f); //reset ball to a position
        hit = false; //set reset on again
        footBall.tag = "Ball";//set reset on again
    }
}
