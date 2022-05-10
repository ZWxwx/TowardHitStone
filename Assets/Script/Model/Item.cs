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
	public Action OnUsed;
	//
	public string sprite;


	public Item(ItemType type,int count=1,bool usable=true,int param=0,string sprite= "Sprite/DefaultIcon")
	{
		this.itemType = type;
		this.count = count;
		this.usable = usable;
		this.param = param;
		this.sprite = sprite;
	}

	public void use()
	{
		if(this.OnUsed!=null)
			OnUsed();
	}
}
