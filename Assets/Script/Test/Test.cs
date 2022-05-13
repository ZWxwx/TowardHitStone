using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	public void testAddItem()
	{
		ItemManager.Instance.AddToItemPlayer1(new Item(ItemType.Tracking, PlayerType.Player1));
		GameObject.Find("ItemsPlane_Player1").GetComponent<UIItemPlane>().UpdateInfo();
	}

	public void testAddItemNew()
	{
		ItemManager.Instance.AddToItemPlayer1(new Item(ItemType.Interfere, PlayerType.Player1));
		GameObject.Find("ItemsPlane_Player1").GetComponent<UIItemPlane>().UpdateInfo();
	}
}
