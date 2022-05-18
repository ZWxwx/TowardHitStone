using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteHealthBarController : MonoBehaviour
{
	public MeteoriteObject meteoriteObject;
	private float currentHealth;
	[SerializeField]
	public float CurrentHealth
	{
		get
		{
			return currentHealth;
		}
		set
		{
			currentHealth = value;
			ResetHealth();
			if (currentHealth <= 0)
			{
				OnMetDestroy();
			}
		}
	}

	public float maxHealth;

	public float originSize;

	private void Start()
	{
		if(originSize==0)
			originSize = transform.localScale.x;
		CurrentHealth = 50;
	}

	public void ResetHealth()
	{
		transform.localScale = new Vector3(originSize * CurrentHealth / maxHealth, transform.localScale.y, transform.localScale.z);
	}

	public void OnMetDestroy()
	{
		MetDestroyEventHandler.Instance.metDestroy(this.meteoriteObject);
	}
}
