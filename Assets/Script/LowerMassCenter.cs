using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerMassCenter : MonoBehaviour
{
    private Rigidbody rb;

    public float centerOffset = -1.5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, centerOffset, 0f);
    }
}
