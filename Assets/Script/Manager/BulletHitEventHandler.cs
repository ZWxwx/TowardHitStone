using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitEventHandler : Singleton<BulletHitEventHandler>
{
	public Action<BulletController, GameObject> hitAction;

	public new void Start()
	{
		hitAction += this.handleHitAction;
	}
	public void handleHitAction(BulletController bullet, GameObject target)
	{
		if (target.tag == "Meteorite")
		{
			target.GetComponent<MeteoriteObject>().healthBar.CurrentHealth -= 40;
			Destroy(bullet.gameObject);
			//Destroy(target);
			//ComboManager.Instance.playerComboManagers[bullet.playerType].ComboNum += 1;
			//ComboManager.Instance.playerComboManagers[bullet.playerType].instantiateComboText(target.transform, new Vector3(0, 1, 0));
			//MeteoriteCreateSystem.Instance.moveArea(bullet.playerType);//÷¥––≥Ê∂¥“∆∂Ø÷∏¡Ó
		}
	}
}
