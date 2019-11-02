using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	GameManager instance;
	ObjectSwap swapper;
	GameObject leftHand;
	GameObject rightHand;


	private void Awake()
	{
		instance = this;
	}

	// Start is called before the first frame update
	void Start()
    {
		swapper = gameObject.GetComponent<ObjectSwap>();
		leftHand = GameObject.Find("Controller (left)");
		rightHand = GameObject.Find("Controller (right)");
	}

    // Update is called once per frame
    void Update()
    {
		if (leftHand.GetComponent<pointer>().objectHit != null && rightHand.GetComponent<pointer>().objectHit != null)
		{
			swapper.SwapObjects(leftHand.GetComponent<pointer>().objectHit, rightHand.GetComponent<pointer>().objectHit);
			leftHand.GetComponent<pointer>().objectHit = null;
			rightHand.GetComponent<pointer>().objectHit = null;
		}
    }
}
