using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerComboBar : MonoBehaviour
{
	public float originSize;
	public PlayerComboManager comboManager;
	public Image barFilled;
	public Action<PlayerType> onBarFull;
	public void Start()
	{
		if (originSize == 0)
		{
			originSize = GetComponent<RectTransform>().rect.width;
		}
		comboManager.addCombo += this.UpdateBar;
		barFilled = GetComponent<Image>();
		onBarFull += ImmediateItemManager.Instance.UseImmediateItem;
	}
	public void Update()
	{

	}

	public void UpdateBar(float value)
	{ 
		barFilled.fillAmount += value;
		if (barFilled.fillAmount >= 1)
		{
			barFilled.fillAmount = 0;
			if (onBarFull != null)
			{
				onBarFull(comboManager.playerType);
			}
			
		}
	}
}
