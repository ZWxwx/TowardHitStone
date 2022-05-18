using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAllMetFreezing : TimeLimitedEffectStatus
{
	protected new IEnumerator Start()
	{
		Init();
		yield return new WaitForSeconds(time);
		Destroy(this);
	}

	void Init()
	{
		MeteoriteCreateSystem.Instance.addNewMeteorite += AddFreeze;
		if (playerType == PlayerType.Player1)
		{
			foreach (var obj in MeteoriteCreateSystem.Instance.MeteoriteListLeft)
			{
				Freeze(obj.GetComponent<MeteoriteObject>());
			}
		}
		else if(playerType == PlayerType.Player2)
		{
			foreach (var obj in MeteoriteCreateSystem.Instance.MeteoriteListRight)
			{
				Freeze(obj.GetComponent<MeteoriteObject>());
			}
		}
		
	}

	void AddFreeze(MeteoriteObject obj)
	{
		if (obj.ForWhichPlayer == playerType)
		{
			Freeze(obj);
		}
	}
	void Freeze(MeteoriteObject obj)
	{
		obj.gameObject.GetComponent<Rigidbody2D>().velocity *= 0.2f;
	}

	void UnFreeze(MeteoriteObject obj)
	{
		obj.gameObject.GetComponent<Rigidbody2D>().velocity/= 0.2f;
	}

	private void OnDestroy()
	{
		if (playerType == PlayerType.Player1)
		{
			foreach (var obj in MeteoriteCreateSystem.Instance.MeteoriteListLeft)
			{
				UnFreeze(obj.GetComponent<MeteoriteObject>());
			}
		}
		else if (playerType == PlayerType.Player2)
		{
			foreach (var obj in MeteoriteCreateSystem.Instance.MeteoriteListRight)
			{
				UnFreeze(obj.GetComponent<MeteoriteObject>());
			}
		}
	}
}
