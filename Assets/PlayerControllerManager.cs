using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerManager : Singleton<PlayerControllerManager>
{
	public Dictionary<PlayerType, PlayerController> playerControllers = new Dictionary<PlayerType, PlayerController>();
	[Header("��ȡ��ҿ��������ù�����")]
	public PlayerController p1;
	public PlayerController p2;

	public void Start()
	{
		playerControllers.Add(PlayerType.Player1, p1);
		playerControllers.Add(PlayerType.Player2, p2);
	}
}
