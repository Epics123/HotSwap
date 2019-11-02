using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Grabbing : MonoBehaviour
{
	public SteamVR_Input_Sources handType;
	public SteamVR_Behaviour_Pose controllerPose;
	public SteamVR_Action_Boolean teleportAction;
	public SteamVR_Action_Boolean grabAction;

	private GameObject collidingObject;
	private GameObject objectInHand;
	
	private void SetCollidingObject(Collider col)
	{
		if(collidingObject || !col.GetComponent<Rigidbody>())
		{
			return;
		}

		collidingObject = col.gameObject;
	}

	// Update is called once per frame
    void Update()
	{
		if(grabAction.GetLastStateDown(handType))
		{
			if(collidingObject)
			{
				GrabObject();
			}
		}

		if(grabAction.GetLastStateUp(handType))
		{
			if(objectInHand)
			{
				ReleaseObject();
			}
		}
    }

	public void OnTriggerEnter(Collider other)
	{
		SetCollidingObject(other);
	}

	public void OnTriggerStay(Collider other)
	{
		SetCollidingObject(other);
	}

	public void OnTriggerExit(Collider other)
	{
		if(!collidingObject)
		{
			return;
		}

		collidingObject = null;
	}

	private void GrabObject()
	{
		objectInHand = collidingObject;
		collidingObject = null;

		var joint = AddFixedJoint();										//MAY BE WRONG
		joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
	}

	private FixedJoint AddFixedJoint()
	{
		FixedJoint fx = gameObject.AddComponent<FixedJoint>();
		fx.breakForce = 20000;
		fx.breakTorque = 20000;
		return fx;
	}

	private void ReleaseObject()
	{
		if(GetComponent<FixedJoint>())
		{
			GetComponent<FixedJoint>().connectedBody = null;
			Destroy(GetComponent<FixedJoint>());

			objectInHand.GetComponent<Rigidbody>().velocity = controllerPose.GetVelocity();
			objectInHand.GetComponent<Rigidbody>().angularVelocity = controllerPose.GetAngularVelocity();
		}

		objectInHand = null;
	}

	public bool GetTeleportDown()
	{
		return teleportAction.GetStateDown(handType);
	}

	public bool GetGrab()
	{
		return grabAction.GetState(handType);
	}
}
