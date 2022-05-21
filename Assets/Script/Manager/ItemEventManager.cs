using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEventManager : Singleton<ItemEventManager>
{
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
				break;
			default:
				break;
		}
	}
}
