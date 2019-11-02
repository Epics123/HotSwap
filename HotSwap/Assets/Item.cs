using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItem", menuName = "Item")]
public class Item : ScriptableObject
{
	public new string name;
	public string description;
	public uint weight;

	List<Item> items = new List<Item>();

	public GameObject model;
}
