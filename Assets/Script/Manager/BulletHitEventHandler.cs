using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitEventHandler : Singleton<BulletHitEventHandler>
{
	public Action<BulletController, GameObject> hitAction;

	public void Start()
	{
		hitAction += this.handleHitAction;
	}
	public void handleHitAction(BulletController bullet, GameObject target)
	{
		if (target.tag == "Meteorite")
		{
			Destroy(target);
			ComboManager.Instance.ComboNum += 1;
		}
	}
}
