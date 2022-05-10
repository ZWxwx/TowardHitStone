using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User:Singleton<User>
{
	public List<Item> player1_items=new List<Item>();
	public List<Item> player2_items= new List<Item>();
}
