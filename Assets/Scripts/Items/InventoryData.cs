using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory")]
public class InventoryData : ScriptableObject
{
	public List<ItemStack> items = new List<ItemStack>();

	public void AddItem(ItemData itemData)
	{
		// check if item is already avaliable
		foreach (var item in items)
		{
			// if item exists add to item count
			if (item.itemData == itemData)
			{
				if (itemData.allowMultiple)
				{
					item.count++;
				}
				return;
			}
		}

		items.Add(new ItemStack(itemData, 1));
	}

	public void RemoveItem(ItemData itemData)
	{
		// check if item is already avaliable
		foreach (var item in items)
		{
			// if item exists remove from item count
			if (item.itemData == itemData)
			{
				// if count is 0, remove item
				item.count--;
				if (item.count <= 0)
				{
					items.Remove(item);
				}
				return;
			}
		}
	}

	public bool Contains(ItemData itemData) 
	{ 
		var item = items.Find(item => item.itemData == itemData);

		return (item != null);
	}

	public int Count(ItemData itemData)
	{
		int count = items.Count(item => item.itemData == itemData);

		return count;
	}
}
