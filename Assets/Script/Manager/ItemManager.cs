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
    [Header("��һ����ը ���ָ���")]
    public float BombChance;
    [Range(0f, 1f)]
    [Header("��ס��ʯ ���ָ���")]
    public float FreezingChance;
    [Range(0f, 1f)]
    [Header("�粨���� ���ָ���")]
    public float InterfereChance;
    [Range(0f, 1f)]
    [Header("3s׷�� ���ָ���")]
    public float TrackingChance;
    [Range(0f, 1f)]
    [Header("���ɰ׶� ���ָ���")]
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
            Debug.LogError("������Ʒ���ʶ�Ϊ0");
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
        bool f = false;//�����������Ʒ�Ƿ��Ѵ���
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
        bool f = false;//�����������Ʒ�Ƿ��Ѵ���
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
