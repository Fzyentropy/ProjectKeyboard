using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "LevelSetUp", menuName = "LevelSetUp", order = 2)]
public class LevelSetup : ScriptableObject
{
    [TableList(ShowPaging = true, ShowIndexLabels = true)]
    public List<LevelStructure> LevelTable = new List<LevelStructure>();

    [Serializable]
    public class LevelStructure
    {
        [BoxGroup("Levels", ShowLabel = false)]
        public Vector2 LevelKeyCoordination;
        public GameObject LevelKeyboard;
        public GameObject playableGameObject;
        public Vector3 playableObjectPosition;
    }
}
