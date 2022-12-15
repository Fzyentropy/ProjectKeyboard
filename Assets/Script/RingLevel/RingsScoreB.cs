using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class RingsScoreB : MonoBehaviour
{
    [HideInInspector] public bool hitB = false;
    [SerializeField] private MMF_Player bFeedback;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            hitB = true;
            bFeedback.PlayFeedbacks();
        }
    }
}
