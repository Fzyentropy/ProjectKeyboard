using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitGroundReset : MonoBehaviour
{
    [SerializeField] private GameObject footBall;
    private bool hit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") & hit == false)
        {
            StartCoroutine(Kickoff());
            hit = true;
            footBall.tag = "Untagged";
        }
    }

    IEnumerator Kickoff()
    {
        yield return new WaitForSeconds(1);
        footBall.transform.position = new Vector3(7.5f, 3.5f, -3.5f);
        hit = false;
        footBall.tag = "Ball";
    }
}
