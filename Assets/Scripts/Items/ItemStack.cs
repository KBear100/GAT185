using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A stack is a group of identical items that can be stored in a single inventory space
[Serializable]
public class ItemStack
{
	public ItemData itemData;
	public int count;

	public ItemStack()
	{
		itemData = null;
		count = 0;
	}

	public ItemStack(ItemData itemData, int count)
	{
		this.itemData = itemData;
		this.count = count;
	}

	public ItemStack(ItemStack itemStack) 
	{ 
		itemData = itemStack.itemData;
		count = itemStack.count;
	}
}
