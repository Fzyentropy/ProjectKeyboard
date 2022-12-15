using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class FootballScore : MonoBehaviour
{
    [SerializeField] private MMF_Player goalFeedback;
    private float SScore = 0;
    public TextMeshProUGUI ScoreText; 
    // Start is called before the first frame update
    void Start()
    {
        SScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // ScoreText.text = SScore.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            SScore += 1f;
            goalFeedback.PlayFeedbacks();
            // StartCoroutine(KickOff());
        }
    }

    // IEnumerator KickOff()
    // {
    //     yield return new WaitForSeconds(2);
    //
    //     GameObject.FindWithTag("Ball").transform.position = new Vector3(7.5f, 3.5f, -3.5f);

        // yield return null;
    // }
}
