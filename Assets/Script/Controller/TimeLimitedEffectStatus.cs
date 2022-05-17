using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimitedEffectStatus : MonoBehaviour
{
	public EffectType effectType;
	public float time;//״̬����ʱ��

	private IEnumerator Start()
	{
		yield return new WaitForSeconds(time);
		Destroy(this);
	}
}
