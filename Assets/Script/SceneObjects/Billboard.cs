using System.Collections;
using System;
using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;

public class Billboard : MonoBehaviour
{
    public int BillBoardID;

    public bool hasTimeLimit = true;
    [ShowIf("hasTimeLimit")]
    public TextMeshProUGUI timeTextField;
    [ShowIf("hasTimeLimit")]
    [SerializeField] private float timeLimit = 10f;

    public bool hasScore = true;
    [ShowIf("hasScore")]
    public TextMeshProUGUI leftScoreTextField;
    [ShowIf("hasScore")]
    public TextMeshProUGUI rightScoreTextField;

    public TextMeshProUGUI dialogueTextField;



    void Start()
    {
        InitializeBillboard();
        GameManager.Instance.OnLevelStarts += InitializeBillboard;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnLevelStarts -= InitializeBillboard;
    }

    public void InitializeBillboard()
    {
        leftScoreTextField.text = "0";
        rightScoreTextField.text = "0";
        timeTextField.text = timeLimit.ToString();
        dialogueTextField.text = "";
    }

    public void InitializeBillboard(int levelIndex)
    {
        if (levelIndex != BillBoardID) return;

        leftScoreTextField.text = "0";
        rightScoreTextField.text = "0";
        timeTextField.text = timeLimit.ToString();
        dialogueTextField.text = "";

        if (hasTimeLimit) StartCoroutine(StartTimer());
    }

    public IEnumerator StartTimer()
    {
        float counter = timeLimit;
        while (counter > 0)
        {
            timeTextField.text = Mathf.RoundToInt(counter).ToString();
            yield return new WaitForSeconds(Time.deltaTime);
            counter -= Time.deltaTime;
        }

        int winner = CheckWinner();
        GameManager.Instance.OnLevelEnds.Invoke(winner);
        StartCoroutine(Timer(5f));
        GameManager.Instance.OnBackToMenu.Invoke();
    }
    IEnumerator Timer(float timeLimit)
    {
        // if (timeLimit <= 0) yield return null;

        yield return new WaitForSeconds(timeLimit);
    }


    /// <summary>
    /// Get the winner of the level
    /// 0: left
    /// 1: right
    /// 2: tie
    /// </summary>
    /// <returns></returns>
    private int CheckWinner()
    {
        int rightScore = Convert.ToInt32(rightScoreTextField.text);
        int leftScore = Convert.ToInt32(leftScoreTextField.text);
        if (rightScore > leftScore)
        {
            dialogueTextField.text = "Right Player Wins!";
            return 1;
        }
        else if (rightScore < leftScore)
        {
            dialogueTextField.text = "Left Player Wins!";
            return 0;
        }
        else
        {
            dialogueTextField.text = "Draw!";
            return 2;
        }
    }
}
