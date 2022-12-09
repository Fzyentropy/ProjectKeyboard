using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class RingsScoreC : MonoBehaviour
{
    [HideInInspector] public bool hitC = false;
    [SerializeField] private MMF_Player topFeedback;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            hitC = true;
            topFeedback.PlayFeedbacks();
        }
    }
}
