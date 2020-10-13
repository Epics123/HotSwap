using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItem", menuName = "Item")]
public class Item : ScriptableObject
{
	public new string name;
	public string description;
	public uint weight;
	public AudioClip uniqueAudio;
	public AudioClip collisionAudio;
    public bool good;

	List<Item> items = new List<Item>();

	public GameObject model;
}
