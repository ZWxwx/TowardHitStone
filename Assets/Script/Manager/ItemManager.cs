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

    [Range(0f, 1f)]
    [Header("下一发爆炸 出现概率")]
    public float BombChance;
    [Range(0f, 1f)]
    [Header("冻住陨石 出现概率")]
    public float FreezingChance;
    [Range(0f, 1f)]
    [Header("电波干扰 出现概率")]
    public float InterfereChance;
    [Range(0f, 1f)]
    [Header("3s追踪 出现概率")]
    public float TrackingChance;
    [Range(0f, 1f)]
    [Header("生成白洞 出现概率")]
    public float WhiteHoleChance;

    const int itemNum = 5;
    [HideInInspector]
    public float[] itemChance = new float[itemNum];
    private void Awake()
    {
        setChance();
    }
    public void setChance()
    {
        float sum = 0;
        itemChance[0] = BombChance;
        itemChance[1] = FreezingChance;
        itemChance[2] = InterfereChance;
        itemChance[3] = TrackingChance;
        itemChance[4] = WhiteHoleChance;
        for (int i = 0; i < itemNum; ++i)
        {
            sum += itemChance[i];
        }
        if(sum==0)
        {
            Debug.LogError("所有物品概率都为0");
        }
        for (int i = 0; i < itemNum; ++i)
        {
            itemChance[i] /= sum;
        }
    }
    public ItemType ItemRandom()
    {
        float randomNum = Random.Range(0f, 1f);
        int itemChoose;
        for(itemChoose=0; itemChoose < itemNum;++itemChoose)
        {
            randomNum -= itemChance[itemChoose];
            if (randomNum < 0)
            {
                break;
            }
        }
        return (ItemType)randomNum + 1;
        
    }

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
