using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEventManager : Singleton<ItemEventManager>
{
	public void OnItemUsed(ItemType type,PlayerType owner)
	{
		if (type == ItemType.Freezing)
		{

		}
	}
}
