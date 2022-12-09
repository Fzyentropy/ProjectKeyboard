using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    public void StartLevel(int levelIndex)
    {
        GameManager.Instance.OnLevelStarts.Invoke(levelIndex);
    }
}
