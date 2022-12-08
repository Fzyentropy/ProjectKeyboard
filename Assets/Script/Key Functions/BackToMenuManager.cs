using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Singletons;

public class BackToMenuManager : MonoBehaviour
{
    /// <summary>
    /// 1. zoom out
    /// 2. destroy level
    /// </summary>
    public void BackToMenu()
    {
        GameManager.Instance.OnBackToMenu.Invoke();
    }
}