using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimitedEffectStatus : MonoBehaviour
{
	public EffectType effectType;

	//仅在针对某玩家的效果时赋值
	public PlayerType playerType;
	public float time;//状态持续时间

	protected virtual IEnumerator Start()
	{
		yield return new WaitForSeconds(time);
		Destroy(this);
	}
}
