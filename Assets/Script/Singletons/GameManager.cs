using System;
using Script.SceneObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using Script.Singletons;
using Sirenix.OdinInspector;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [LabelText("Managers")]
    public LevelManager LevelManager { get; private set; }
    public CameraManager CameraManager { get; private set; }
    public ControlManager ControlManager { get; private set; }
    public ScoreManager ScoreManager { get; private set; }

    [LabelText("Actions")]
    public Action<int> OnLevelStarts;
    public Action<Billboard.Player> OnLevelEnds;
    public Action OnBackToMenu;
    public Action<Billboard.Player, int> OnScores;
    public Action<Billboard.Player, int> OnWins;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        LevelManager = GetComponentInChildren<LevelManager>();
        CameraManager = GetComponentInChildren<CameraManager>();
        ControlManager = GetComponentInChildren<ControlManager>();
        ScoreManager = GetComponentInChildren<ScoreManager>();
    }

    private void Update()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        // reset when mouse middle button is pressed
        if (Input.GetMouseButtonDown(2)) SceneManager.LoadScene(0);
    }
}
