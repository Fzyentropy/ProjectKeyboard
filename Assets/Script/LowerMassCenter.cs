using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerMassCenter : MonoBehaviour
{
    private Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.centerOfMass = new Vector3(0f, -1f, 0f); //move mass center of rigidbody to a lower position
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
