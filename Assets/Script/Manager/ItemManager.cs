using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public List<Item> itemsPlayer1 = new List<Item>();
    public List<Item> itemsPlayer2 = new List<Item>();
    public GameObject itemPlane1;
    public GameObject itemPlane2;
    public GameObject itemPrefab;

    public void AddToItemPlayer1(Item item){
        bool f = false;//代表所添加物品是否已存在
        foreach(var kv in itemsPlayer1)
		{
			if (kv.itemType == item.itemType)
			{
                f = true;
                kv.count += item.count;
                break;
			}
		}
		if (!f)
		{
            itemsPlayer1.Add(item);
		}
    }

    public void AddToItemPlayer2(Item item)
    {
        bool f = false;//代表所添加物品是否已存在
        foreach (var kv in itemsPlayer2)
        {
            if (kv.itemType == item.itemType)
            {
                f = true;
                kv.count += item.count;
                break;
            }
        }
        if (!f)
        {
            itemsPlayer2.Add(item);
        }
    }

}
