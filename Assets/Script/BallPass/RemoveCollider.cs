using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCollider : MonoBehaviour
{

    [SerializeField] private float removeHeight = 2f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= removeHeight) this.GetComponent<CapsuleCollider>().isTrigger = true;
    }
}
