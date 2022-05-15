using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{

	public ItemType itemType;
	public int count;
	//�Ƿ�������Ʒ����ʹ��
	public bool usable;
	//Ԥ������
	public int param;
	//
	//���¼���Ҫ���ʹ����Ʒ���������Ч��������ItemType��PlayerType
	public Action<ItemType,PlayerType> UseAction=ItemEventManager.Instance.OnItemUsed;
	//���¼���Ҫ���ʹ����Ʒ������ĸ���Ч��������Ʒ��UI�ĸ���
	public Action OnUsed;

	//
	public string sprite;
	//
	public PlayerType owner;

	public Item(ItemType type, PlayerType owner,int count=1,bool usable=true,int param=0,string sprite= "Sprite/DefaultIcon")
	{
		this.itemType = type;
		this.count = count;
		this.usable = usable;
		this.param = param;
		this.sprite = sprite;
		this.owner = owner;
	}

	public bool use()
	{
		if (count > 0)
		{
			count--;
			ItemEventManager.Instance.OnItemUsed(this.itemType, this.owner);
			UseAction(itemType,owner);
			OnUsed();
			Debug.LogFormat("{0}ʹ����{1}", owner.ToString(), itemType.ToString());
			return true;
		}
		return false;

	}
}
