using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : Singleton<ComboManager>
{
	private int comboNum;
	public  Text comboNumText;
	public int ComboNum
	{
		get
		{
			return comboNum;
		}
		set
		{
			comboNum = value;
			comboNumText.text = comboNum.ToString();
		}
	}
}
