using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTimer : Timer
{
	public MouseClickRecorder clickRecorder;
    void Update()
    {
		if (iTime >= timeSpots[0])
		{
			clickRecorder.isOpen = true;
		}
    }
}
