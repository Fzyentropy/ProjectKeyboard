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
        PrintText(); //show text on billboard
    }

    public static void TalkTrash(string Letter)
    {
        dialogText += Letter; //put new string input after the original string
        
        if (dialogText.Length >= 8) //if text length exceeds 8, clear the textbox
        {
            dialogText = Letter;
        }

    }

    private void PrintText()
    {
        trashTalkDialogBox.text = dialogText;
    }
}
