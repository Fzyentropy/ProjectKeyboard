using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Billboard : MonoBehaviour
{
    public TextMeshProUGUI leftScoreTextField;
    public TextMeshProUGUI rightScoreTextField;
    public TextMeshProUGUI timeTextField;
    public TextMeshProUGUI dialogueTextField;

    public float timeLimit = 90f;

    void Start()
    {
        leftScoreTextField.text = "0";
        rightScoreTextField.text = "0";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BeginTimer()
    {
        Utils.Timer(timeLimit, timeTextField.text);
    }
}
