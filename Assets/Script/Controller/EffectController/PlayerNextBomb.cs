using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNextBomb : EventTriggerEffectStatus
{
    public bool alreadyAdd=false;
	public PlayerType playerType;
    public override void Start()
    {
		effectEnd += OnEffectEnd;
		effectEndSource += this.effectEnd;
		PlayerControllerManager.Instance.playerControllers[playerType].onShotBullet += this.addBombEffect;
		PlayerControllerManager.Instance.playerControllers[playerType].onShot += this.effectEndSource;
		
		

	}

    public void addBombEffect(BulletController bullet)
	{
		if (!alreadyAdd)
		{
            //²âÊÔÐ§¹û
            bullet.gameObject.transform.localScale *= 10;
			alreadyAdd = true;
		}
	}

	void OnEffectEnd()
	{
		Destroy(this);
	}

	private void OnDestroy()
	{
		if (PlayerControllerManager.Instance.playerControllers[playerType] != null)
		{
			PlayerControllerManager.Instance.playerControllers[playerType].onShotBullet -= this.addBombEffect;
			PlayerControllerManager.Instance.playerControllers[playerType].onShot -= this.effectEndSource;
		}
		
	}

}
