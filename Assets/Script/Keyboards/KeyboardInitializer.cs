using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardInitializer : SerializedMonoBehaviour
{
    [Header("Keyboard Settings")]
    // all keys on keyboard
    // public static List<GameObject> keysOnKeyboard = new List<GameObject>();
    // [SerializeField] private GameObject keyBoard;
    [SerializeField] private Vector3 keyboardPos;
    public int keyboardIndex = -1;
    // keys in use
    [SerializeField] private ActiveKeys activeKeys;
    [SerializeField] private List<int> keyCountEachRow = new List<int>() { 14, 14, 13, 12, 1 };

    // position of the first generated key
    [SerializeField] private Vector3 startingKeyPos;
    // gap between keys
    [SerializeField] private float keyboardGapX = 0;
    [SerializeField] private float keyboardGapZ = 0;

    public bool allowMultipleInput = true;
    [HideInInspector] public bool isAnyKeyPressed = false;

    [Range(1, 25)]
    public float keyScale = 25;

    [Header("Key Settings")]
    [Range(0f, 2f)]
    public float keyTravelDistance;
    private float keyAltitude;
    [SerializeField] private Material leftKeysMat;
    [SerializeField] private Material rightKeysMat;
    
    [SerializeField] private float pressDownTime = 0.2f;
    [SerializeField] private float bounceUpTime = 0.1f;
    
    [SerializeField] private float maxProtrudeDistance = 1f;

    [SerializeField] private bool isChargeable = false;
    
    [ShowIf("isChargeable")]
    [BoxGroup("Key Charge Settings")]
    [SerializeField] private float maxChargeTime;
    [ShowIf("isChargeable")]
    [BoxGroup("Key Charge Settings")]
    [SerializeField] private Color chargedColor;
    
    
    // use length as the key to look up key models
    [DictionaryDrawerSettings(KeyLabel = "Key Type", ValueLabel = "Key Models")]
    public Dictionary<KeyTypes, GameObject> keyModelsDictionary = new Dictionary<KeyTypes, GameObject>();
    
    // row, column of special keys
    [DictionaryDrawerSettings(KeyLabel = "Key Row and Column", ValueLabel = "Key Type")]
    public Dictionary<Vector2, KeyTypes> specialKeys = new Dictionary<Vector2, KeyTypes>();

    // functional keys
    [DictionaryDrawerSettings(KeyLabel = "Key Row and Column", ValueLabel = "Functions")]
    public Dictionary<Vector2, UnityEvent> functionalKeys = new Dictionary<Vector2, UnityEvent>();
    [HideInInspector] public Dictionary<Vector2, GameObject> functionalKeysCoordinations = new Dictionary<Vector2, GameObject>();
    
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
        Space,
        Soccer,
        AmericanFootball,
        Sailing,
        SumoWrestling,
        GymnasticRings,
        Esc
    }

    void Awake()
    {
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
        transform.position = keyboardPos;

        keyTravelDistance *= keyScale;
        keyboardGapX *= keyScale;
        keyboardGapZ *= keyScale;
        maxProtrudeDistance *= keyScale;

        // get the real size of each key types
        foreach (var typeModelPair in keyModelsDictionary)
        {
            keySize.Add(typeModelPair.Key, typeModelPair.Value.GetComponent<MeshRenderer>().bounds.size * keyScale);
        }
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

        // place escape key
        SetUpEscapeKey();
        
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
                GameObject currentKey = Instantiate(keyModelsDictionary[currentKeyType], currentKeyPos, Quaternion.identity, transform);

                // adjust the key scale
                currentKey.transform.localScale *= keyScale;

                if (row != keyCountEachRow.Count - 1)
                    SetKeyMaterial(currentKey);

                // set functions on keys
                SetFunction(currentKey, currentKeyLoc);

                // set basic key attributes
                SetKeyBasicAttribute(currentKey, row, placedKeysNum);
                
                placedKeysNum++;

                // keysOnKeyboard.Add(currentKey);

                // move to the next key and save the current key size as the previous one
                previousKeySize = currentKeySize;
            }
            // reset the column
            currentKeyPos.x = startingKeyPos.x;
            // move to the next row
            currentKeyPos.z -= currentKeySize.z + keyboardGapZ;
        }
    }

    private void SetUpEscapeKey()
    {
        Vector2 escapeKeyLoc = new Vector2(-1, 0);
        Vector3 escapeKeyPos = new Vector3(startingKeyPos.x + 0.5f * keyScale, 0f, startingKeyPos.z + keyScale + keyboardGapZ);
        GameObject escapeKey = Instantiate(keyModelsDictionary[KeyTypes.Esc], escapeKeyPos, Quaternion.identity, transform);
        escapeKey.transform.localScale *= keyScale;
        SetFunction(escapeKey, escapeKeyLoc);

        Key keyAttribute = escapeKey.GetComponent<Key>();
        keyAttribute.keyName = KeyCode.Escape;
        keyAttribute.keyboardIndex = keyboardIndex;
        keyAttribute._keyboardInitializer = this;

        keyAttribute.travelDistance = keyTravelDistance;
        keyAttribute.initDelay = 0;

        keyAttribute.pressDownTime = pressDownTime;
        keyAttribute.bounceUpTime = bounceUpTime;

        keyAttribute.isChargeable = isChargeable;

        keyAttribute.maxChargeTime = maxChargeTime;
        keyAttribute.maxProtrudeDistance = maxProtrudeDistance;
        keyAttribute.chargedColor = chargedColor;

    }

    private void SetKeyBasicAttribute(GameObject currentKey, int row, int placedKeysNum)
    {
        Key keyAttribute = currentKey.GetComponent<Key>();

        keyAttribute.keyName = activeKeys.activeKeysInSequence[placedKeysNum];
        keyAttribute.keyboardIndex = keyboardIndex;
        keyAttribute._keyboardInitializer = this;

        keyAttribute.travelDistance = keyTravelDistance;
        keyAttribute.initDelay = 0.5f * row;

        keyAttribute.pressDownTime = pressDownTime;
        keyAttribute.bounceUpTime = bounceUpTime;

        keyAttribute.isChargeable = isChargeable;

        keyAttribute.maxChargeTime = maxChargeTime;
        keyAttribute.maxProtrudeDistance = maxProtrudeDistance;
        keyAttribute.chargedColor = chargedColor;
    }

    private void SetKeyMaterial(GameObject currentKey)
    {
        if (currentKey.transform.position.x < (keySize[KeyTypes.Letters].x * 15 + 13 * keyboardGapX) / 2)
            currentKey.GetComponent<MeshRenderer>().material = leftKeysMat;
        else currentKey.GetComponent<MeshRenderer>().material = rightKeysMat;
    }

    /// <summary>
    /// exit, change to another keyboard, zoom in, cn
    /// </summary>
    private void SetFunction(GameObject currentKey, Vector2 currentKeyLoc)
    {
        if (functionalKeys.ContainsKey(currentKeyLoc))
        {
            FunctionalKey functionalKey = currentKey.AddComponent<FunctionalKey>();
            functionalKey.keyFunction = functionalKeys[currentKeyLoc];
            // print(functionalKeys[currentKeyLoc].GetType());
            functionalKeysCoordinations.Add(currentKeyLoc, currentKey);
        }
        else
        {
            currentKey.AddComponent<Key>();
        }
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
