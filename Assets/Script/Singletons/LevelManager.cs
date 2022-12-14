using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using ExternalPropertyAttributes;
using System.Collections;
using Script.SceneObjects;

public class LevelManager : MonoBehaviour
{
    public LevelSetup levelSetups;
    [SerializeField] private GameObject titleKeyboard;
    [SerializeField] private GameObject keyboardHolder;

    [InfoBox("Title Keyboard Settings")]
    private float titleKeyboardScale;
    private float titleKeyboardTravelDistance;
    public Dictionary<Vector2, GameObject> functionalTitleKeysCoordinations = new Dictionary<Vector2, GameObject>();

    [InfoBox("Level Keyboards Settings")]
    [HideInInspector] public List<GameObject> keyboards = new List<GameObject>();
    [HideInInspector] public List<Vector2> levelKeyCoordinations = new List<Vector2>();
    private List<Vector3> levelKeyboardPosition = new List<Vector3>();

    private GameObject activeKeyboard = null;
    private GameObject activePlayableObject = null;

    void Start()
    {
        InitializeLevel();
        GameManager.Instance.OnLevelStarts += StartLevel;
        GameManager.Instance.OnBackToMenu += DestroyLevel;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnLevelStarts -= StartLevel;
        GameManager.Instance.OnBackToMenu -= DestroyLevel;
    }

    /// <summary>
    /// When the game is opened, initialize levels, but do not start
    /// </summary>
    private void InitializeLevel()
    {
        // get basic info of title keyboard
        GetTitleKeyboardInfo();

        SetUpLevels();
    }

    private void GetTitleKeyboardInfo()
    {
        KeyboardInitializer titleKeyboardInitializer = titleKeyboard.GetComponent<KeyboardInitializer>();
        titleKeyboardInitializer.keyboardIndex = -1;
        functionalTitleKeysCoordinations = titleKeyboardInitializer.functionalKeysCoordinations;
        titleKeyboardScale = titleKeyboardInitializer.keyScale;
        titleKeyboardTravelDistance = titleKeyboardInitializer.keyTravelDistance;
    }

    /// <summary>
    /// get basic level info
    /// </summary>
    private void SetUpLevels()
    {
        List<LevelSetup.LevelStructure> levels = levelSetups.LevelTable;

        foreach (var level in levels)
        {
            // get each level keyboard
            GameObject currentKb = level.LevelKeyboard;
            keyboards.Add(currentKb);

            // level key coordination on title keyboard
            Vector2 levelKeyCoordination = level.LevelKeyCoordination;
            levelKeyCoordinations.Add(levelKeyCoordination);

            int levelIndex = levels.IndexOf(level);
            GameObject levelKey = functionalTitleKeysCoordinations[levelKeyCoordination];
            Vector3 levelKeyPosition = levelKey.transform.position;

            currentKb.GetComponent<KeyboardInitializer>().keyboardIndex = levelIndex;

            // camera setup
            GameManager.Instance.CameraManager.InitializeCameras(levelIndex, levelKeyPosition);

            // save the world position of each level keyboard
            levelKeyboardPosition.Add(GetLevelKeyboardWorldPosition(levelKeyCoordination));
        }
    }

    /// <summary>
    /// Start a level
    /// 1. zoom in to the key
    /// 2. generate new keyboard
    /// 3. cutscene, UI
    /// </summary>
    /// <param name="levelIndex"></param>
    private void StartLevel(int levelIndex)
    {
        StartCoroutine(ScheduleLevelStart(levelIndex));
    }

    private IEnumerator ScheduleLevelStart(int levelIndex)
    {
        yield return new WaitForSeconds(2);
        // generate the keyboard
        GenerateKeyboard(levelIndex, levelKeyboardPosition[levelIndex]);
        // wait until keyboard rising
        yield return new WaitForSeconds(1.5f);
        // generate playable object
        GeneratePlayableObject(levelIndex);
        // init scoring system
        // StartLevelScoring(levelIndex);
    }

    private void GeneratePlayableObject(int levelIndex)
    {
        print("generate playable");
        LevelSetup.LevelStructure currentLevel = levelSetups.LevelTable[levelIndex];
        GameObject levelKey = functionalTitleKeysCoordinations[currentLevel.LevelKeyCoordination];
        Vector3 spawnPos = levelKey.transform.position + currentLevel.playableObjectPosition;
        spawnPos.y += 12.5f;
        GameObject playableObject = Instantiate(currentLevel.playableGameObject,
            spawnPos, Quaternion.identity);
        activePlayableObject = playableObject;
        print("playbal "+playableObject);
        print(activePlayableObject);
        playableObject.GetComponent<PlayableObject>().spawnPosition = spawnPos;
    }

    /// <summary>
    /// Generate disabled keyboard on each position
    /// </summary>
    /// <param name="spawnPosition"></param>
    private void GenerateKeyboard(int levelIndex, Vector3 spawnPosition)
    {
        if (activeKeyboard is null)
        {
            GameObject kb = Instantiate(keyboards[levelIndex], spawnPosition, Quaternion.identity,
                keyboardHolder.transform);
            kb.transform.position = spawnPosition;
            activeKeyboard = kb;
        }
    }

    private void DestroyLevel()
    {
        // activeKeyboard.SetActive(false);
        print(activePlayableObject);
        Destroy(activeKeyboard);
        Destroy(activePlayableObject);
        activePlayableObject = null;
        activeKeyboard = null;
        // activeKeyboard.transform.DOMoveY(activeKeyboard.transform.position.y - 10, 1f)
        //     .OnComplete(() => { activeKeyboard.SetActive(false); activeKeyboard = null; });
    }

    public Vector3 GetLevelKeyboardWorldPosition(Vector2 levelKeyCoordination)
    {
        Vector3 levelKeyPosition = functionalTitleKeysCoordinations[levelKeyCoordination].transform.position;

        Vector3 levelKeyboardPosition = levelKeyPosition + new Vector3(0, titleKeyboardTravelDistance/2 + titleKeyboardScale + 0.5f, 0);

        // place the keyboard in the center of the key
        Vector3 centerOffset = new Vector3(-7.5f, 0f, 2.5f);

        levelKeyboardPosition += centerOffset;

        return levelKeyboardPosition;
    }

    private void EndLevel()
    {
        // get winner

        // wait for secs

        // back to main scene

        // destroy level keyboard

        // delete level from list
    }
}
