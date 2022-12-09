using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FunctionalKey : Key
{
    [HideInInspector] public UnityEvent keyFunction;

    /// <summary>
    /// trigger the function when the key released
    /// </summary>
    protected override void StartCharge()
    {
        base.StartCharge();

        if (isFullyCharged)
        {
            keyFunction.Invoke();
            isFullyCharged = false;
        }
    }
}
