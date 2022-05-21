using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletTracker : TimeLimitedEffectStatus
{
	protected override IEnumerator Start()
	{
		PlayerControllerManager.Instance.playerControllers[playerType].onShotBullet += this.addTrackerEffect;
		yield return new WaitForSeconds(time);
		Destroy(this);
		
	}
	public void addTrackerEffect(BulletController bullet)
	{
		bullet.gameObject.AddComponent<TrackingBullet>();
	}

	public void OnDestroy()
	{
		PlayerControllerManager.Instance.playerControllers[playerType].onShotBullet -= this.addTrackerEffect;
	}
}
