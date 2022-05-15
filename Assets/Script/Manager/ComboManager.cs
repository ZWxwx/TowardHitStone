using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : Singleton<ComboManager>
{
	public Dictionary<PlayerType, PlayerComboManager> playerComboManagers=new Dictionary<PlayerType, PlayerComboManager>();

	protected override void Start()
	{
		if (_instance == null)
		{
			DontDestroyOnLoad(gameObject);
		}
		playerComboManagers.Add(PlayerType.Player1, GetComponents<PlayerComboManager>()[0]);
		playerComboManagers.Add(PlayerType.Player2, GetComponents<PlayerComboManager>()[1]);
	}
}