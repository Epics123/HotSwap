using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwap : MonoBehaviour
{

    public Transform closeObject, swapObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SwapObjects();
        }
    }


    void SwapObjects()
    {
        Vector3 tmp;

        tmp = closeObject.position;
        closeObject.position = swapObject.position;
        swapObject.position = tmp;
    }
}
