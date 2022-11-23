using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class RingsScore : MonoBehaviour
{
    private float ringsScore;
    [SerializeField] private TextMeshProUGUI ringsScoreText;
    [SerializeField] private GameObject leftRing;
    [SerializeField] private GameObject rightRing;
    [SerializeField] private GameObject collisionBoxB;
    [SerializeField] private GameObject collisionBoxA;
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
            leftRing.transform.position.y - 1f, leftRing.transform.position.z + 1.5f);
        
        collisionBoxB.transform.position = new Vector3(
            (leftRing.transform.position.x + rightRing.transform.position.x) / 2,
            leftRing.transform.position.y - 1f, leftRing.transform.position.z - 1.5f);
        
        transform.position = new Vector3(
            (leftRing.transform.position.x + rightRing.transform.position.x) / 2,
            leftRing.transform.position.y - 4f, leftRing.transform.position.z);

        // if (gameObject.transform.rotation.x >= 0.98 || gameObject.transform.rotation.x <= -0.98)
        // {
        //     ringsScore += 1f;
        //     Debug.Log("Turned!");
        // }
        
        ringsScoreText.text = ringsScore.ToString();
        
        {
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (collisionBoxA.GetComponent<RingsScoreA>().hitA == true &&
                collisionBoxB.GetComponent<RingsScoreB>().hitB == true) 
            {
                ringsScore += 1f;
                collisionBoxA.GetComponent<RingsScoreA>().hitA = false;
                collisionBoxB.GetComponent<RingsScoreB>().hitB = false;
            }

            if (collisionBoxA.GetComponent<RingsScoreA>().hitA == true &&
                collisionBoxB.GetComponent<RingsScoreB>().hitB == false)
            {
                collisionBoxA.GetComponent<RingsScoreA>().hitA = false;
            }

            if (collisionBoxB.GetComponent<RingsScoreB>().hitB == true &&
                collisionBoxA.GetComponent<RingsScoreA>().hitA == false) 
            {
                collisionBoxB.GetComponent<RingsScoreB>().hitB = false;
            }
        }
    }
}
