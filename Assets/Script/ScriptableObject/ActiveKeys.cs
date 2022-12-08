using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "ActiveKeys", menuName = "ActiveKeys", order = 1)]
public class ActiveKeys : ScriptableObject
{
    [InfoBox("This is a sequence of physical keys, these keys will be bound to the in-game keys in sequence")]
    public List<KeyCode> activeKeysInSequence = new List<KeyCode>();
}
