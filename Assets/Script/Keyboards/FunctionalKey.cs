using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FunctionalKey : Key
{
    [HideInInspector] public UnityEvent keyFunction;
    private bool isInvoked = false;

    /// <summary>
    /// trigger the function when the key released
    /// </summary>
    protected override void StartCharge()
    {
        base.StartCharge();

        InvokeFunction();
    }

    protected override void KeyMoveUp()
    {
        base.KeyMoveUp();
        isInvoked = false;
    }

    private void InvokeFunction()
    {
        if (isInvoked) return;

        if (isFullyCharged)
        {
            keyFunction.Invoke();
            isInvoked = true;
        }
    }
}
