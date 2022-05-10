using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	private BulletType currentBulletType;
	public BulletType CurrentBulletType
	{
		get
		{
			return currentBulletType;
		}
		set
		{
			currentBulletType = value;
		}

	}

}
