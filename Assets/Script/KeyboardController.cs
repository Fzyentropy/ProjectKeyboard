using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardController : MonoBehaviour
{

    public string KeyName;
    public float KeyTop;
    public float KeyBottom;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        KeyMove();
    }

    public void KeyMove()
    {
            
        if(Input.GetKey(KeyCode.A))
        {
            KeyDown(GameObject.Find("KeyCubeA"));
        }
        else
        {
            KeyUp(GameObject.Find("KeyCubeA"));
        }

        if(Input.GetKey(KeyCode.S))
        {
            KeyDown(GameObject.Find("KeyCubeS"));
        }
        else
        {
            KeyUp(GameObject.Find("KeyCubeS"));
        }

        if(Input.GetKey(KeyCode.D))
        {
            KeyDown(GameObject.Find("KeyCubeD"));
        }
        else
        {
            KeyUp(GameObject.Find("KeyCubeD"));
        }

        if(Input.GetKey(KeyCode.F))
        {
            KeyDown(GameObject.Find("KeyCubeF"));
        }
        else
        {
            KeyUp(GameObject.Find("KeyCubeF"));
        }

    }


    public void KeyDown(GameObject KeyBlock)
    {
        if(KeyBlock.transform.position.y > KeyBottom)
        {
            KeyBlock.transform.position = new Vector3 (KeyBlock.transform.position.x, KeyBlock.transform.position.y - .1f,KeyBlock.transform.position.z);
        }
    }

    public void KeyUp(GameObject KeyBlock)
    {
        if(KeyBlock.transform.position.y <= KeyTop)
        {
            KeyBlock.transform.position = new Vector3 (KeyBlock.transform.position.x, KeyBlock.transform.position.y + .1f,KeyBlock.transform.position.z);
        }
    }






}
