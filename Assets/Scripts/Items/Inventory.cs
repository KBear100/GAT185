using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	[SerializeField] InventoryData inventoryData;
	[SerializeField] InventoryData defaultInventoryData;
	[SerializeField] ItemData defaultItemData;

	private Item[] items; // items that are a part of the owner game object (melee weapon, guns)
	public Item currentItem { get; private set; }

	private void Start()
	{
        //inventoryData.items.Clear();
        //foreach (var item in defaultInventoryData.items)
        //{
        //    inventoryData.items.Add(item);
        //}

        // search children for items (items that the owner game object possess)
        items = GetComponentsInChildren<Item>(true);
		if (defaultItemData != null)
		{
			currentItem = GetItemFromItemData(defaultItemData);
			currentItem?.Equip();
		}
	}

	public void EquipNextItem()
	{
		if (inventoryData.items.Count == 0) return;
		
		// if not currently equiped, equip first equipable item in inventory
		if (currentItem == null)
		{
			var itemStack = inventoryData.items.Find(itemStack => itemStack.itemData.equipable);
			if (itemStack != null)
			{
				currentItem = GetItemFromItemData(itemStack.itemData);
				currentItem?.Equip();
			}
		}
		else
		{
			// find current item data in inventory and get index in inventory list
			var itemStack = inventoryData.items.Find(itemStack => itemStack.itemData == currentItem.GetData());
			int index = inventoryData.items.IndexOf(itemStack);
			// search through inventory to next equipable item
			int count = 1;
			ItemData newItemData = null;
			while (count++ < inventoryData.items.Count)
			{
				index++;
				if (index == inventoryData.items.Count) index = 0;
				if (inventoryData.items[index].itemData.equipable)
				{
					newItemData = inventoryData.items[index].itemData;
				}
			}
			// if new item data exists, unequip old item and equip new item
			if (newItemData != null)
			{
				currentItem.Unequip();
				currentItem = GetItemFromItemData(newItemData);
				currentItem?.Equip();
			}
		}
	}

	public void AddItem(ItemData itemData)
	{
		inventoryData.AddItem(itemData);
	}

	private Item GetItemFromItemData(ItemData itemData)
	{
		// find item in items with matching item data
		return items.FirstOrDefault(item => item.GetData() == itemData);
	}

	public void Use()
	{
		if (currentItem == null) return;

		currentItem.Use();
	}

	public void StopUse()
	{
		if (currentItem == null) return;

		currentItem.StopUse();
	}

	public void OnAnimEventItemUse()
	{
		if (currentItem == null) return;

		currentItem.OnAnimEventItemUse();
	}

}
