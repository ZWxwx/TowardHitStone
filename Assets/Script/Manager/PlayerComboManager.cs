using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerComboManager:MonoBehaviour
{
	public float comboTime;//combo持续时间，该时间内未摧毁任何陨石则comboNum清零，否则（摧毁陨石）则重置至该时间
	public float iComboTime;
	private int comboNum;
	public Text comboNumText;

	//随连击数改变颜色
	public float colorParam1;
	public Color originColor;
	public Color endColor;

	public void Start()
	{
		StartCoroutine(runComboTime());
	}
	public int ComboNum
	{
		get
		{
			return comboNum;
		}
		set
		{
			comboNum = value;
			comboNumText.text = string.Format("{0} combo!", comboNum);
			if (value != 0)
			{
				resetTime();
				comboNumText.color = value < colorParam1 ? new Color((endColor.r - originColor.r) * value / colorParam1 + originColor.r,
					(endColor.g - originColor.g) * value / colorParam1 + originColor.g,
					(endColor.b - originColor.b) * value / colorParam1 + originColor.b) : endColor;
			}
			else
			{
				comboNumText.color = new Color(comboNumText.color.r, comboNumText.color.g, comboNumText.color.b, 0.5f);
			}
		}
	}

	IEnumerator runComboTime()
	{
		while (true)
		{
			if (iComboTime - Time.deltaTime > 0)
			{
				iComboTime -= Time.deltaTime;
			}
			else if (iComboTime - Time.deltaTime < 0)
			{
				iComboTime = 0;
				resetNum();
			}
			yield return null;
		}
	}


	public void resetTime()
	{
		iComboTime = comboTime;
	}
	public void resetNum()
	{
		ComboNum = 0;
	}

	public void instantiateComboText(Transform transform, Vector3 offset)
	{
		comboNumText.gameObject.transform.position = transform.position + offset;
	}
}
