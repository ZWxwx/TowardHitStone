using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WornHoleDestroyArea : MonoBehaviour
{
	public List<string> tagsOfDestroyedItems;
	public PlayerType playerToDestroy;
	public BoxCollider2D areas;

	public void Start()
	{
		areas = GetComponent<BoxCollider2D>();
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		foreach (var tag in tagsOfDestroyedItems)
		{
			if (collision.gameObject.tag == tag)
			{
				switch (tag)
				{
					case "Meteorite":
					if (collision.gameObject.GetComponent<MeteoriteObject>().ForWhichPlayer == playerToDestroy)
					{
						Destroy(collision.gameObject);
					}
					break;
					case "Bullet":
						if (collision.gameObject.GetComponent<BulletController>().playerType== playerToDestroy)
						{
							Destroy(collision.gameObject);
						}
						break;

					default:
						break;
				}

			}
		}
	}

}
