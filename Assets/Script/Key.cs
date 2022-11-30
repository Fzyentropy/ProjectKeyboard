using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;

public class Key : MonoBehaviour
{
    [Header("Basic Attribute")]
    [HideInInspector] public KeyCode keyName;
    [HideInInspector] public float initDelay;
    private Material keyMaterial;

    [Header("Key Movement")]
    private Rigidbody keyRb;
    
    [HideInInspector] public float travelDistance = 0f;
    
    private float originalY;
    private float destinationY;
    
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
    
    // [BoxGroup("Key Charge Settings")]

    [Header("Feedbacks")] 
    [SerializeField] private MMF_Player chargeFeedback;

    private void Start()
    {
        KeyInitializer();
    }

    private void Update()
    {
        PressKey();
        
        ReleaseKey();
    }
    
    /// <summary>
    /// Initialize physics settings of keys
    /// </summary>
    private void KeyInitializer()
    {
        keyRb = this.AddComponent<Rigidbody>();
        keyRb.constraints = RigidbodyConstraints.FreezeAll;
        keyRb.constraints = ~RigidbodyConstraints.FreezePositionY;
        keyRb.useGravity = false;
        keyRb.isKinematic = true;
        
        // the start and end point of each key when pressed down
        originalY = travelDistance;
        destinationY = 0;
        
        // charge related
        if (isChargeable) InitializeChargeSetting();
        
        // move to its own height
        StartCoroutine(SetInitHeight());
    }

    private void InitializeChargeSetting()
    {
        maxProtrudeDistance += travelDistance;
        keyMaterial = GetComponent<Renderer>().material;
        startColor = keyMaterial.color;
    }
    
    private IEnumerator SetInitHeight()
    {
        yield return new WaitForSeconds(initDelay);
        print(originalY);
        transform.DOMoveY(originalY, 1f);
    }
    
    private void PressKey()
    {
        if (keyName == KeyCode.Space)
        {
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) ||
                Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt) ||
                Input.GetKey(KeyCode.LeftMeta) ||
                Input.GetKey(KeyCode.RightMeta) || Input.GetKey(KeyCode.Space))
            {
                print(keyName);
                
                if (isChargeable) StartCharge();

                keyRb.DOMoveY(destinationY, pressDownTime);
            }
        }
        else
        {
            if (Input.GetKey(keyName))
            {
                print(keyName);
                
                if (isChargeable) StartCharge();
                        
                keyRb.DOMoveY(destinationY, pressDownTime);
            }
        }
    }
    
    /// <summary>
    /// Add a down force to key when it is pressed
    /// </summary>
    private void ReleaseKey()
    {
        if (keyName == KeyCode.Space)
        {
            if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl) ||
                Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.RightAlt) || Input.GetKeyUp(KeyCode.LeftMeta) ||
                Input.GetKeyUp(KeyCode.RightMeta)|| Input.GetKeyUp(KeyCode.Space))
            {
                if (isChargeable) StopCharge();
                
                print(keyName);
                DG.Tweening.Sequence moveUpSequence = DOTween.Sequence();
                moveUpSequence.Append(keyRb.DOMoveY(originalY + protrudeDistance, bounceUpTime))
                    .Append(keyRb.DOMoveY(originalY, 0.1f));
                protrudeDistance = 0f;
            }
        }
        else
        {
            if (Input.GetKeyUp(keyName))
            {
                if (isChargeable) StopCharge();
                
                print(keyName);
                DG.Tweening.Sequence moveUpSequence = DOTween.Sequence();
                moveUpSequence.Append(keyRb.DOMoveY(originalY + protrudeDistance, bounceUpTime))
                    .Append(keyRb.DOMoveY(originalY, 0.1f));
            }
        }
    }

    private void StartCharge()
    {
        // start charge
        if (chargeTime < maxChargeTime) chargeTime += Time.deltaTime;
        else chargeTime = maxChargeTime;
                
        keyMaterial.color = Color.Lerp(startColor, chargedColor, chargeTime);
    }
    
    /// <summary>
    /// calculate the protrude distance after charging 
    /// </summary>
    private void StopCharge()
    {
        keyMaterial.color = startColor;
        protrudeDistance = (chargeTime * maxProtrudeDistance) / maxChargeTime;
        print("protrude distance " + protrudeDistance);
        chargeTime = 0f;
    }
}
