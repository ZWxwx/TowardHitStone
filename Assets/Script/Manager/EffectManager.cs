using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    public void HandleTimeLimitedEffect(TimeLimitedEffectStatus status)
	{
		switch (status.effectType)
		{
			case EffectType.freezing:
				Time.timeScale = 0.5f;
				break;
			default:
				break;
		}
	}
}
