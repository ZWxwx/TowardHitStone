using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimitedEffectStatus : MonoBehaviour
{
	public EffectType effectType;

	//�������ĳ��ҵ�Ч��ʱ��ֵ
	public PlayerType playerType;
	public float time;//״̬����ʱ��

	protected virtual IEnumerator Start()
	{
		yield return new WaitForSeconds(time);
		Destroy(this);
	}
}
