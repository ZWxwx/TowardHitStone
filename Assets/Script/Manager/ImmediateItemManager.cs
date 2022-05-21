using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmediateItemManager : Singleton<ImmediateItemManager>
{
    public void UseImmediateItem(PlayerType playerType)
	{
		
		if (0 < 0.5)
		{
			if (playerType == PlayerType.Player1)
			{
				foreach (var obj in MeteoriteCreateSystem.Instance.MeteoriteListLeft)
				{
					obj.GetComponent<MeteoriteObject>().healthBar.CurrentHealth -= 500f;
				}
			}
			else if(playerType == PlayerType.Player2)
			{
				foreach (var obj in MeteoriteCreateSystem.Instance.MeteoriteListRight)
				{
					obj.GetComponent<MeteoriteObject>().healthBar.CurrentHealth -= 500f;
				}
			}
			
		}
	}   
}
