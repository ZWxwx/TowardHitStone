using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteHoleBulletHealthBar : MonoBehaviour
{
	public WhiteHoleBullet whiteHoleBullet;
	[SerializeField]
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
				OnBulletDestroy();
			}
		}
	}

	public float maxHealth;



	private void Start()
	{

		CurrentHealth = maxHealth;
		ResetHealth();
	}

	public void ResetHealth()
	{
		
		transform.localScale = new Vector3(CurrentHealth / maxHealth / 2f, transform.localScale.y, transform.localScale.z);
	}

	public void OnBulletDestroy()
	{
		Destroy(transform.parent.gameObject);
	}

	public void Update()
	{
		this.transform.rotation = Quaternion.Euler(0, 0, 0);
	}
}
