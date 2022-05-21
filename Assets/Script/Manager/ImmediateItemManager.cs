using System.Collections;
using UnityEngine;

public class ImmediateItemManager : Singleton<ImmediateItemManager>
{
    public void UseImmediateItem(PlayerType playerType)
	{
		
		if (0 < 0.5)
		{
			StartCoroutine(clearPlayerMet(playerType));
		}
	}

	public IEnumerator clearPlayerMet(PlayerType playerType)
	{
		GameObject preObj;
		GameObject obj;
		int i=0;
		if (playerType == PlayerType.Player1)
		{

			while(MeteoriteCreateSystem.Instance.MeteoriteListLeft.Count>i)
			{
				obj = MeteoriteCreateSystem.Instance.MeteoriteListLeft[i];
				obj.GetComponent<MeteoriteObject>().healthBar.CurrentHealth -= 500f;
				preObj = obj;
				yield return null;
				if (preObj.Equals(obj))
				{
					i++;
				}
			}
		}
		else if (playerType == PlayerType.Player2)
		{
			while (MeteoriteCreateSystem.Instance.MeteoriteListRight.Count>i)
			{
				obj = MeteoriteCreateSystem.Instance.MeteoriteListRight[i];
				preObj = obj;
				yield return null;
				if (preObj.Equals(obj))
				{
					i++;
				}
			}
		}
		else yield return null;
	}
}
