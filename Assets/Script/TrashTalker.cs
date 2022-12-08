using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TrashTalker : MonoBehaviour
{ 
    public TextMeshProUGUI trashTalkDialogBox;
    private static string dialogText = "Start!";

    private void Update()
    {
        PrintText();
    }

    public static void TalkTrash(string Letter)
    {
        dialogText += Letter;
        
        if (dialogText.Length >= 8)
        {
            dialogText = Letter;
        }

    }

    private void PrintText()
    {
        trashTalkDialogBox.text = dialogText;
    }
}
