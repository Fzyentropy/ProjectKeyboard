using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class KeyExitCollision : MonoBehaviour
{
    [SerializeField] private MMF_Player keyExitFeedback;
    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.CompareTag("Key"))
        {
            keyExitFeedback.PlayFeedbacks();
        }
    }
}
