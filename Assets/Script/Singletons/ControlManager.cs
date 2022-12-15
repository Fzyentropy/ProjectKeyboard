using System;
using UnityEngine;

namespace Script.Singletons
{
    public class ControlManager : MonoBehaviour
    {
        [HideInInspector] public int activeKeyboardIndex = -1;
        private int lastActiveKeyboardIndex = -1;

        private void Start()
        {
            GameManager.Instance.OnLevelStarts += SwitchControlKeyboard;
            GameManager.Instance.OnBackToMenu += SwitchControlKeyboard;
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnLevelStarts -= SwitchControlKeyboard;
            GameManager.Instance.OnBackToMenu -= SwitchControlKeyboard;
        }

        private void Update()
        {
            // print((activeKeyboardIndex));
            if (GameManager.Instance.CameraManager.cameraBrain.IsBlending) StopReceiveControl();
            else StartReceiveControl();
        }

        /// <summary>
        /// control the title keyboard
        /// </summary>
        private void SwitchControlKeyboard()
        {
            activeKeyboardIndex = -1;
            lastActiveKeyboardIndex = -1;
        }

        private void SwitchControlKeyboard(int LevelIndex)
        {
            activeKeyboardIndex = LevelIndex;
            lastActiveKeyboardIndex = LevelIndex;
        }

        public void StartReceiveControl()
        {
            // print("start input" + activeKeyboardIndex);
            activeKeyboardIndex = lastActiveKeyboardIndex;
        }

/// <summary>
/// 1 换目的地键盘index
/// 2 保存到last active
/// 3 转换到active
/// </summary>
        public void StopReceiveControl()
        {
            // print("stop input" + activeKeyboardIndex);
            if (activeKeyboardIndex != -2) lastActiveKeyboardIndex = activeKeyboardIndex;
            // no input could be received
            activeKeyboardIndex = -2;
        }
    }
}