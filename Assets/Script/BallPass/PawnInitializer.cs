using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnInitializer : MonoBehaviour
{

    [SerializeField] private GameObject Pawn;
    [SerializeField] private float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitializePawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator InitializePawn()
    {
        yield return new WaitForSeconds(waitTime);
        while (true)
        {
            
            Instantiate(Pawn, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
    }
    
}
