using System;
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

	public Rigidbody2D rb;
	public List<string> crashedItemTags;

	private void Start()
	{
		rb=GetComponent<Rigidbody2D>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		touchObject(this,collision.gameObject);
	}

	public void touchObject(BulletController bulletController,GameObject collisionObj)
	{
		BulletHitEventHandler.Instance.handleHitAction(bulletController, collisionObj);
	}

	public void OnDestroy()
	{
	}
}
