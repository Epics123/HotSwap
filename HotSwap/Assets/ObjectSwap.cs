﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwap : MonoBehaviour
{

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
        //if(Input.GetKeyDown(KeyCode.R))
        //{
        //    SwapObjects(leftHand.GetComponent<pointer>().objectHit, rightHand.GetComponent<pointer>().objectHit);
        //}
    }


    public void SwapObjects(GameObject left, GameObject right)
    {
        Vector3 tmp;

		if (left.GetComponent<ItemInterface>().getWeightClass().Equals(right.GetComponent<ItemInterface>().getWeightClass()))
		{
			tmp = left.transform.position;
			left.transform.position = right.transform.position;
			right.transform.position = tmp;
		}
    }
}
