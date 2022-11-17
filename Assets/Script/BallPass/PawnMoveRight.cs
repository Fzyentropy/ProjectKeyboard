using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMoveRight : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (transform.position.x+moveSpeed, transform.position.y,transform.position.z);
    }


    


}
