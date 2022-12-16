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
    private float ringsScore;
    [SerializeField] private TextMeshProUGUI ringsScoreText;
    [SerializeField] private GameObject leftRing;
    [SerializeField] private GameObject rightRing;
    [SerializeField] private GameObject collisionBoxB;
    [SerializeField] private GameObject collisionBoxA;
    [SerializeField] private GameObject collisionBoxC;
    [SerializeField] private MMF_Player feedBack;
    // [SerializeField] private GameObject collisionBoxBottom;
    // private bool hitA;
    // private bool hitB;
    // Start is called before the first frame update
    void Start()
    {
        ringsScore = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        collisionBoxA.transform.position = new Vector3(
            (leftRing.transform.position.x + rightRing.transform.position.x) / 2,
            leftRing.transform.position.y - 1f, leftRing.transform.position.z + 1.5f); //set collision box A position
        
        collisionBoxB.transform.position = new Vector3(
            (leftRing.transform.position.x + rightRing.transform.position.x) / 2,
            leftRing.transform.position.y - 1f, leftRing.transform.position.z - 1.5f); //set collision box B position


        collisionBoxC.transform.position =
            new Vector3((leftRing.transform.position.x + rightRing.transform.position.x) / 2,
                leftRing.transform.position.y + 1f, leftRing.transform.position.z); //set collision box C position

        
        transform.position = new Vector3(
            (leftRing.transform.position.x + rightRing.transform.position.x) / 2,
            leftRing.transform.position.y - 4f, leftRing.transform.position.z); //set bottom collision box position
        // Debug.Log(collisionBoxB.GetComponent<RingsScoreB>().hitB);
        // if (gameObject.transform.rotation.x >= 0.98 || gameObject.transform.rotation.x <= -0.98)
        // {
        //     ringsScore += 1f;
        //     Debug.Log("Turned!");
        // }
        
        ringsScoreText.text = ringsScore.ToString(); //send score to the textbox
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (collisionBoxA.GetComponent<RingsScoreA>().hitA &
                collisionBoxB.GetComponent<RingsScoreB>().hitB &
                collisionBoxC.GetComponent<RingsScoreC>().hitC)
            {
                ringsScore += 1f;
                feedBack.PlayFeedbacks(); //if player go through A/B/C collision box without hitting the bottom box, then when hitting bottom box, plus 1 score
                collisionBoxA.GetComponent<RingsScoreA>().hitA = false;
                collisionBoxB.GetComponent<RingsScoreB>().hitB = false;
                collisionBoxC.GetComponent<RingsScoreC>().hitC = false; //then set ABC box bool to false
            }
            else
            {
                collisionBoxA.GetComponent<RingsScoreA>().hitA = false;
                collisionBoxA.GetComponent<RingsScoreB>().hitB = false;
                collisionBoxA.GetComponent<RingsScoreC>().hitC = false; //if any A/B/C is not triggered, when hitting bottom box, set all to false
            } 

        }

        // if (collisionBoxA.GetComponent<RingsScoreA>().hitA &
        //     collisionBoxB.GetComponent<RingsScoreB>().hitB &
        //     collisionBoxC.GetComponent<RingsScoreC>().hitC == false) ;
        //     {
        //         collisionBoxA.GetComponent<RingsScoreA>().hitA = false;
        //         collisionBoxA.GetComponent<RingsScoreB>().hitB = false;
        //         collisionBoxA.GetComponent<RingsScoreC>().hitC = false;
        //     }
        // }
    }
}
