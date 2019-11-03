﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInterface : MonoBehaviour
{
	public Item item;

    public string getWeightClass()
	{
		return "medium";
	}

	public AudioClip getUniqueAudio()
	{
		return item.uniqueAudio;
	}

	public AudioClip getCollisionAudio()
	{
		return item.uniqueAudio;
	}
}
