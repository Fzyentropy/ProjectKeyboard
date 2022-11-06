using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField]
    private Vector3 G = new Vector3(0,-1000f,0);
    [SerializeField]
    private Rigidbody objectRb;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AddGravity();
    }


    private void AddGravity()
    {
        objectRb.AddForce(G, ForceMode.Force);
    }
    
    
}
