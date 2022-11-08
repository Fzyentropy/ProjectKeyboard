using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// TO DO:
/// 1. mass
/// 2. generate keys
/// 3. different layout
/// 4. improve inspector
/// 5. check component availability
public class Key : MonoBehaviour
{
    [Header("Basic Attribute")]
    [HideInInspector] public KeyCode keyName;
    private AudioSource audioSource;

    [Header("Physics Settings")]
    private Rigidbody keyRb;
    private Vector3 pressForce;
    // private Vector3 supportingForce;
    private Vector3 bounceForce;
    [SerializeField] private float pressForceMagnitude = 20f;
    [SerializeField] private float bounceForceMagnitude = 500f;
    [SerializeField] private float mass;

    private void Awake()
    {
        KeyInitializer();
    }

    private void FixedUpdate()
    {
        // keyRb.AddForce(supportingForce, ForceMode.Acceleration);

        PressKey();
        
        ReleaseKey();
    }
    
    /// <summary>
    /// Initialize physics settings of keys
    /// </summary>
    private void KeyInitializer()
    {
        audioSource = GetComponent<AudioSource>();
        
        keyRb = this.AddComponent<Rigidbody>();
        keyRb.constraints = RigidbodyConstraints.FreezeAll;
        keyRb.constraints = ~RigidbodyConstraints.FreezePositionY;
        keyRb.mass = mass;

        // supportingForce = Vector3.up * mass * 10;
        bounceForce = Vector3.up * bounceForceMagnitude * mass;
        pressForce = Vector3.down * pressForceMagnitude * mass;
    }
    
    /// <summary>
    /// Add a down force to key when it is pressed
    /// </summary>
    private void PressKey()
    {
        if (Input.GetKey(keyName))
        {
            print(keyName);
            keyRb.AddForce(pressForce, ForceMode.Impulse);
            audioSource.Play();
        }
    }
    
    /// <summary>
    /// Add a constant bounce force to each key
    /// </summary>
    private void ReleaseKey()
    {
        keyRb.AddForce(bounceForce, ForceMode.Acceleration);
    }
}
