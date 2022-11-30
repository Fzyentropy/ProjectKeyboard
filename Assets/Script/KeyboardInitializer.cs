using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using OpenCover.Framework.Model;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

/// <summary>
/// TO DO:
/// 1. fix the key starting point
/// 2. keyboard generating position
/// 3. Material change adapt to the key starting position
/// 4. collider completely in trigger 
/// </summary>

public class KeyboardInitializer : SerializedMonoBehaviour
{
    [Header("Keyboard Settings")]
    [SerializeField] private GameObject keyBoard;
    [SerializeField] private Vector3 keyboardPos;

    // keys in use
    [SerializeField] private ActiveKeys activeKeys;
    
    // all keys on keyboard
    public static List<GameObject> keysOnKeyboard = new List<GameObject>();
    
    [InfoBox("The number of keys in each row")]
    [SerializeField] private List<int> keyCountEachRow = new List<int>() { 14, 14, 13, 12, 1 };

    // position of the first generated key
    [SerializeField] private Vector3 startingKeyPos;

    // gap between keys
    [SerializeField] private float keyboardGapX = 0;
    [SerializeField] private float keyboardGapZ = 0;

    [Header("Key Settings")] [Range(0f, 2f)]
    [SerializeField] private float keyTravelDistance;
    private float keyAltitude;
    [SerializeField] private Material leftKeysMat;
    [SerializeField] private Material rightKeysMat;
    
    [SerializeField] private float pressDownTime = 0.2f;
    [SerializeField] private float bounceUpTime = 0.1f;

    [SerializeField] private bool isChargeable = false;
    
    [ShowIf("isChargeable")]
    [BoxGroup("Key Charge Settings")]
    [SerializeField] private float maxChargeTime, maxProtrudeDistance = 1f;
    private float chargeTime = 0f;
    private float protrudeDistance = 0f;
    private Color startColor;
    [SerializeField] private Color chargedColor;
    
    
    // use length as the key to look up key models
    [DictionaryDrawerSettings(KeyLabel = "Key Type", ValueLabel = "Key Models")]
    public Dictionary<KeyTypes, GameObject> keyModelsDictionary = new Dictionary<KeyTypes, GameObject>();
    
    // row, column of special keys
    [DictionaryDrawerSettings(KeyLabel = "Key Row and Column", ValueLabel = "Key Type")]
    public Dictionary<Vector2, KeyTypes> specialKeys = new Dictionary<Vector2, KeyTypes>();
    
    private Dictionary<KeyTypes, Vector3> keySize = new Dictionary<KeyTypes, Vector3>();
    
    
    public enum KeyTypes
    {
        Letters,
        Ctrl,
        Tab,
        CapsLock,
        BackSpace,
        Enter,
        RightShift,
        Space
    }

    void Awake()
    {
        // typeof(KeyCode.A)
        InitializeKeyboard();
    }

    private void InitializeKeyboard()
    {
        SetKeys();
        
        PlaceKeyOnKeyboard();
    }
    
    // counts: 14, 14, 13, 12, 1 (54)
    
    /// <summary>
    /// Basic Setting of Keys
    /// </summary>
    private void SetKeys()
    {
        // set keyboard
        // if (!keyBoard.Equals(null))
        Instantiate(keyBoard, keyboardPos, Quaternion.Euler(0, 0, 0));
        
        // get the real size of each key types
        foreach (var typeModelPair in keyModelsDictionary)
        {
            keySize.Add(typeModelPair.Key, typeModelPair.Value.GetComponent<MeshRenderer>().bounds.size);
        }
        
        // get key height
        // float keyHeight = keySize[KeyTypes.Letters].y;
        // calculate and move the key altitude: the distance to the ground (y=0)
        // keyAltitude = keyboardPos.y + keyTravelDistance + keyHeight/2;
    }
    
    /// <summary>
    /// Place keys on keyboard
    /// </summary>
    private void PlaceKeyOnKeyboard()
    {
        // set the altitude of the starting key 
        startingKeyPos.y = keyAltitude;
        
        // the current key that is placing
        Vector3 currentKeyPos = new Vector3(startingKeyPos.x, 0, startingKeyPos.z);
        Vector3 currentKeySize = Vector3.zero;
        Vector3 previousKeySize = Vector3.zero;
        int placedKeysNum = 0;
        
        // place keys on keyboard
        for (int row = 0; row < keyCountEachRow.Count; row++)
        {
            int currentRowKeyCount = keyCountEachRow[row];
            for (int col = 0; col < currentRowKeyCount; col++)
            {
                // row and column of current key
                Vector2 currentKeyLoc = new Vector2(row, col);
                
                // set the default key type
                KeyTypes currentKeyType = KeyTypes.Letters;
                
                // check if is special key
                if (specialKeys.ContainsKey(currentKeyLoc))
                    currentKeyType = specialKeys[currentKeyLoc];
                
                currentKeySize = keySize[currentKeyType];
          
                if (col != 0)
                    // move to the next placing position horizontally
                    currentKeyPos.x += previousKeySize.x / 2 + currentKeySize.x / 2 + keyboardGapX;
                else
                    currentKeyPos.x += currentKeySize.x / 2;

                // place the key
                GameObject currentKey = Instantiate(keyModelsDictionary[currentKeyType], currentKeyPos, Quaternion.Euler(0, 0,0), keyBoard.transform);
                
                if (row != keyCountEachRow.Count - 1)
                    SetKeyMaterial(currentKey);
                
                // set basic key attributes
                SetKeyBasicAttribute(currentKey, placedKeysNum, col);
                
                placedKeysNum++;

                keysOnKeyboard.Add(currentKey);

                // move to the next key and save the current key size as the previous one
                previousKeySize = currentKeySize;
            }
            // reset the column
            currentKeyPos.x = startingKeyPos.x;
            // move to the next row
            currentKeyPos.z -= currentKeySize.z + keyboardGapZ;
        }
    }

    private void SetKeyBasicAttribute(GameObject currentKey, int placedKeysNum, int col)
    {
        Key keyAttribute = currentKey.GetComponent<Key>();
        keyAttribute.keyName = activeKeys.activeKeysInSequence[placedKeysNum];
        keyAttribute.travelDistance = keyTravelDistance;
        keyAttribute.initDelay = 0.2f * (col + 1);

        keyAttribute.pressDownTime = pressDownTime;
        keyAttribute.bounceUpTime = bounceUpTime;

        keyAttribute.isChargeable = isChargeable;

        keyAttribute.maxChargeTime = maxChargeTime;
        keyAttribute.maxProtrudeDistance = maxProtrudeDistance;
        keyAttribute.chargeTime = chargeTime;
        keyAttribute.protrudeDistance = protrudeDistance;
        keyAttribute.chargedColor = chargedColor;
    }

    private void SetKeyMaterial(GameObject currentKey)
    {
        if (currentKey.transform.position.x < (keySize[KeyTypes.Letters].x * 15 + 13 * keyboardGapX) / 2)
            currentKey.GetComponent<MeshRenderer>().material = leftKeysMat;
        else currentKey.GetComponent<MeshRenderer>().material = rightKeysMat;
    }
    
    

    // width = 15 units, Height = 5 units
    // key types: 1 unit, 1.25 units, 1.5 units, 1.75 units, 2 units, 2.25 units, 2.75 units, 6.25 units
    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Vector3 currentKeyPos = startingKeyPos;
    //     
    //     for (int i = 0; i < 5; i++)
    //     {
    //         for (int j = 0; j < 15; j++)
    //         {
    //             Gizmos.DrawWireCube(currentKeyPos, new Vector3(1, 1, 1));
    //             currentKeyPos.x++;
    //         }
    //
    //         currentKeyPos.x = startingKeyPos.x;
    //         currentKeyPos.z++;
    //     }
    // }
}
