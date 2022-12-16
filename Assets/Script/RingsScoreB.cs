using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class RingsScoreB : MonoBehaviour
{
    [HideInInspector] public bool hitB = false;
    [SerializeField] private MMF_Player bFeedback;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)//if player hit box B, then set bool to true
    {
        if (other.CompareTag("Ball"))
        {
            hitB = true;
            bFeedback.PlayFeedbacks();
        }
    }
}
