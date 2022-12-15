using System.Collections;
using System;
using Script.SceneObjects;
using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Billboard : MonoBehaviour
{
    public int BillBoardID;

    public bool hasTimeLimit = true;
    [ShowIf("hasTimeLimit")]
    public TextMeshProUGUI timeTextField;
    [ShowIf("hasTimeLimit")]
    [SerializeField] private int timeLimit = 10;

    public bool hasScore = true;
    [ShowIf("hasScore")]
    public TextMeshProUGUI leftScoreTextField;
    [ShowIf("hasScore")]
    public TextMeshProUGUI rightScoreTextField;

    public TextMeshProUGUI dialogueTextField;

    private int leftScore = 0;
    private int rightScore = 0;

    private Coroutine startTimerCoroutine;

    public enum Player
    {
        Left,
        Right,
        Draw
    }

    void Start()
    {
        InitializeBillboard();
        GameManager.Instance.OnLevelStarts += InitializeBillboard;
        GameManager.Instance.OnBackToMenu += StopTimer;
        GameManager.Instance.OnScores += ChangeScores;
        GameManager.Instance.OnWins += CheckWinner;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnLevelStarts -= InitializeBillboard;
        GameManager.Instance.OnBackToMenu -= StopTimer;
        GameManager.Instance.OnScores -= ChangeScores;
        GameManager.Instance.OnWins -= CheckWinner;
    }

    private void InitializeBillboard()
    {
        leftScore = 0;
        rightScore = 0;
        if (leftScoreTextField != null) leftScoreTextField.text = "0";
        if (rightScoreTextField != null) rightScoreTextField.text = "0";
        if (timeTextField != null) timeTextField.text = timeLimit.ToString();
        if (dialogueTextField != null) dialogueTextField.text = "";
    }

    private void InitializeBillboard(int levelIndex)
    {
        if (levelIndex != BillBoardID) return;

        leftScore = 0;
        rightScore = 0;
        if (leftScoreTextField != null) leftScoreTextField.text = "0";
        if (rightScoreTextField != null) rightScoreTextField.text = "0";
        if (timeTextField != null) timeTextField.text = timeLimit.ToString();
        if (dialogueTextField != null) dialogueTextField.text = "";

        if (hasTimeLimit) startTimerCoroutine = StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        int counter = timeLimit;
        while (counter >= 0)
        {
            timeTextField.text = counter.ToString();
            yield return new WaitForSeconds(1);
            counter--;
        }

        // if (BillBoardID == GameManager.Instance.ControlManager.activeKeyboardIndex) yield return null;

        Player winner = CheckWinnerByScore();
        GameManager.Instance.OnLevelEnds.Invoke(winner);
        yield return new WaitForSeconds(1.5f);
        GameManager.Instance.OnBackToMenu.Invoke();
    }

    private void StopTimer()
    {
        if (startTimerCoroutine != null) StopCoroutine(startTimerCoroutine);
    }

    /// <summary>
    /// 0: left
    /// 1: right
    /// 2: tie
    /// </summary>
    private void ChangeScores(Player player, int levelIndex)
    {
        // print("enter change scores: " + BillBoardID + " " + levelIndex);
        if (BillBoardID != levelIndex) return;
        // print("same level index");
        switch (player)
        {
            case Player.Left:
                leftScore++;
                leftScoreTextField.text = leftScore.ToString();
                break;
            case Player.Right:
                rightScore++;
                rightScoreTextField.text = rightScore.ToString();
                break;
        }
    }

    private void CheckWinner(Player player, int levelIndex)
    {
        // print("enter check winner: " + BillBoardID + " " + levelIndex);
        if (BillBoardID != levelIndex) return;

        // print("check winnner: " + player);
        StartCoroutine(ScheduleWinningProcess(player));
    }

    private IEnumerator ScheduleWinningProcess(Player player)
    {
        dialogueTextField.text = $"{player} Player Wins!";
        GameManager.Instance.OnLevelEnds.Invoke(player);
        yield return new WaitForSeconds(1.5f);
        GameManager.Instance.OnBackToMenu.Invoke();
    }

    /// <summary>
    /// Get the winner of the level
    /// 0: left
    /// 1: right
    /// 2: tie
    /// </summary>
    /// <returns></returns>
    private Player CheckWinnerByScore()
    {
        if (rightScore > leftScore)
        {
            dialogueTextField.text = "Right Player Wins!";
            return Player.Right;
        }
        else if (rightScore < leftScore)
        {
            dialogueTextField.text = "Left Player Wins!";
            return Player.Left;
        }
        else
        {
            dialogueTextField.text = "Draw!";
            return Player.Draw;
        }
    }

    // private void TalkTrash(string Letter)
    // {
    //     dialogueTextField.text += Letter;
    //
    //     if (dialogText.Length >= 8)
    //     {
    //         dialogText = Letter;
    //     }
    // }
}
