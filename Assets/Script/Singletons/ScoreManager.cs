using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI leftScoreTextField;
    public TextMeshProUGUI rightScoreTextField;

    private int leftScore = 0;
    private int rightScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        ResetTotalScore();
        GameManager.Instance.OnLevelEnds += ChangeTotalScore;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnLevelEnds -= ChangeTotalScore;
    }

    private void ResetTotalScore()
    {
        leftScore = 0;
        rightScore = 0;
        leftScoreTextField.text = "0";
        rightScoreTextField.text = "0";
    }

    private void ChangeTotalScore(int winnerIndex)
    {
        if (winnerIndex == 0) leftScore++;
        else if (winnerIndex == 1) rightScore++;

        leftScoreTextField.text = leftScore.ToString();
        rightScoreTextField.text = rightScore.ToString();
    }
}
