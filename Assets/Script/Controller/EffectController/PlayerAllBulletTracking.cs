using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAllBulletTracking : TimeLimitedEffectStatus
{
	protected override IEnumerator Start()
	{
		PlayerControllerManager.Instance.playerControllers[playerType].onShotBullet += this.addTracking;
		yield return new WaitForSeconds(time);
		Destroy(this);
	}

	public void addTracking(BulletController bullet)
	{
		
	}
}
