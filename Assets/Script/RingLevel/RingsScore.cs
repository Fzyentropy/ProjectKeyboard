using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.ProBuilder.MeshOperations;

public class RingsScore : MonoBehaviour
{
    // private float ringsScore;
    public Billboard.Player Player;
    // [SerializeField] private TextMeshProUGUI ringsScoreText;
    [SerializeField] private GameObject leftRing;
    [SerializeField] private GameObject rightRing;
    [SerializeField] private GameObject collisionBoxB;
    [SerializeField] private GameObject collisionBoxA;
    [SerializeField] private GameObject collisionBoxC;
    [SerializeField] private MMF_Player feedBack;

    // Update is called once per frame
    void Update()
    {
        collisionBoxA.transform.position = new Vector3(
            (leftRing.transform.position.x + rightRing.transform.position.x) / 2,
            leftRing.transform.position.y - 1/25f, leftRing.transform.position.z + 3/50f);
        
        collisionBoxB.transform.position = new Vector3(
            (leftRing.transform.position.x + rightRing.transform.position.x) / 2,
            leftRing.transform.position.y - 1/25f, leftRing.transform.position.z - 3/50f);

        collisionBoxC.transform.position =
            new Vector3((leftRing.transform.position.x + rightRing.transform.position.x) / 2,
                leftRing.transform.position.y + 1/25f, leftRing.transform.position.z);
        
        transform.position = new Vector3(
            (leftRing.transform.position.x + rightRing.transform.position.x) / 2,
            leftRing.transform.position.y - 4/25f, leftRing.transform.position.z);

        // ringsScoreText.text = ringsScore.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (collisionBoxA.GetComponent<RingsScoreA>().hitA &
                collisionBoxB.GetComponent<RingsScoreB>().hitB &
                collisionBoxC.GetComponent<RingsScoreC>().hitC)
            {
                // ringsScore++;
                GameManager.Instance.OnScores.Invoke(Player, 3);
                feedBack.PlayFeedbacks();
                collisionBoxA.GetComponent<RingsScoreA>().hitA = false;
                collisionBoxB.GetComponent<RingsScoreB>().hitB = false;
                collisionBoxC.GetComponent<RingsScoreC>().hitC = false;
            }
            else
            {
                collisionBoxA.GetComponent<RingsScoreA>().hitA = false;
                collisionBoxA.GetComponent<RingsScoreB>().hitB = false;
                collisionBoxA.GetComponent<RingsScoreC>().hitC = false;
            }
        }
    }
}
