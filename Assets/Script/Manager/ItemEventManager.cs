using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEventManager : Singleton<ItemEventManager>
{
	[SerializeField]
	GameObject WhiteHole;
	[SerializeField]
	float WhiteHoleBulletSpeed;
	public void OnItemUsed(ItemType type,PlayerType owner)
	{
		switch (type)
		{
			case ItemType.None:
				break;
			case ItemType.Explodable_nextBomb:
				PlayerNextBomb nextBombEffect = gameObject.AddComponent<PlayerNextBomb>();
				nextBombEffect.playerType = owner;

				break;
			case ItemType.Freezing:
				//Destroy(PlayerControllerManager.Instance.playerControllers[owner].gameObject);
				PlayerAllMetFreezing freezeEffect = gameObject.AddComponent<PlayerAllMetFreezing>();
				freezeEffect.playerType = owner;
				freezeEffect.time = 4f;
				break;
			case ItemType.Interfere:
				break;
			case ItemType.Tracking:
				PlayerBulletTracker tracker =PlayerControllerManager.Instance.playerControllers[owner].gameObject.AddComponent<PlayerBulletTracker>();
				tracker.playerType = owner;
				tracker.time = 5f;
				break;
			case ItemType.WhiteHole:
				var tmp= Instantiate(WhiteHole,transform).GetComponent<PlayerWhiteHole>();
				if (owner == PlayerType.Player2)
				{
					tmp.transform.localScale = new Vector2(-tmp.transform.localScale.x, tmp.transform.localScale.y);
				}
				tmp.forWhichPlayer = owner;
				tmp.bulletSpeed = WhiteHoleBulletSpeed;
				break;
			default:
				break;
		}
	}

	[EditorButton]
	void whiteHoleItemDebug()
    {
		var tmp = Instantiate(WhiteHole, transform).GetComponent<PlayerWhiteHole>();
		tmp.forWhichPlayer = PlayerType.Player1;
		tmp.bulletSpeed = WhiteHoleBulletSpeed;
	}
	[EditorButton]
	void whiteHoleItemDebug2()
	{
		var tmp = Instantiate(WhiteHole, transform).GetComponent<PlayerWhiteHole>();
		tmp.forWhichPlayer = PlayerType.Player2;
		tmp.bulletSpeed = WhiteHoleBulletSpeed;
		tmp.transform.localScale = new Vector2(-tmp.transform.localScale.x, tmp.transform.localScale.y);
	}
}
