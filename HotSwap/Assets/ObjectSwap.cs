using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwap : MonoBehaviour
{

    public Transform closeObject, swapObject;
	public GameObject leftHand;
	public GameObject rightHand;

    // Start is called before the first frame update
    void Start()
    {
		leftHand = GameObject.Find("Controller (left)");
		rightHand = GameObject.Find("Controller (right)");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SwapObjects(leftHand.GetComponent<pointer>().objectHit, rightHand.GetComponent<pointer>().objectHit);
        }
    }


    void SwapObjects(GameObject left, GameObject right)
    {
        Vector3 tmp;

		if (left.GetComponent<Item>().weight == right.GetComponent<Item>().weight)
		{
			tmp = closeObject.position;
			closeObject.position = swapObject.position;
			swapObject.position = tmp;
		}
    }
}
