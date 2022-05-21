using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetDestroyEventHandler : Singleton<MetDestroyEventHandler>
{
	public Action<MeteoriteObject> metDestroy;

	private void Start()
	{
		metDestroy += handleMetDestroy;
	}

	public void handleMetDestroy(MeteoriteObject meteoriteObject)
	{
		Destroy(meteoriteObject.gameObject);
		ComboManager.Instance.playerComboManagers[meteoriteObject.ForWhichPlayer].ComboNum += 1;
		//ComboManager.Instance.playerComboManagers[meteoriteObject.ForWhichPlayer].instantiateComboText(meteoriteObject.gameObject.transform, new Vector3(0, 1, 0));
		MeteoriteCreateSystem.Instance.moveArea(meteoriteObject.ForWhichPlayer,meteoriteObject.healthBar.maxHealth);//÷¥––≥Ê∂¥“∆∂Ø÷∏¡Ó
	}
}
