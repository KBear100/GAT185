using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
	EQUIPMENT,
	WEAPON,
	AMMO
}

public enum UsageType
{
	SINGLE,
	AUTO,
	BURST,
	STREAM
}

public enum ItemID
{
	MELEE,
	GUN
}

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
	[Header("Item")]
	public string id;
	public string description;
	public Sprite icon;
	public bool allowMultiple;

	public ItemType itemType;
	public UsageType usageType;
	
	public string animTriggerName;
	
	public GameObject itemPrefab;
	public GameObject pickupPrefab;
}
