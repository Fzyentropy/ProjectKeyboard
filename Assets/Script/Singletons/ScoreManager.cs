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

    private void ChangeTotalScore(Billboard.Player winner)
    {
        if (winner.Equals(Billboard.Player.Left)) leftScore++;
        else if (winner.Equals(Billboard.Player.Right)) rightScore++;

        leftScoreTextField.text = leftScore.ToString();
        rightScoreTextField.text = rightScore.ToString();
    }
}
