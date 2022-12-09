using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;    
using UnityEngine.SceneManagement;

public static class Utils
{
    public static IEnumerator Timer(float timeLimit)
    {
        float counter = timeLimit;
        while (counter > 0)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            counter -= Time.deltaTime;
        }
    }

    public static IEnumerator Timer(float timeLimit, string text)
    {
        float counter = timeLimit;
        while (counter > 0)
        {
            text = Mathf.RoundToInt(counter).ToString();
            yield return new WaitForSeconds(Time.deltaTime);
            counter -= Time.deltaTime;
        }
    }

    public static void ChangeEnvironmentalText(TextMeshProUGUI textField, string text, bool isAddingText)
    {
        if (isAddingText)
        {
            textField.text += text;
            if (textField.text.Length >= 8)
            {
                textField.text = text;
            }
        }
        else
        {
            textField.text = text;
        }
    }
}


