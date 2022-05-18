using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerEffectStatus : MonoBehaviour
{
	public Action effectEndSource;
	public Action effectEnd;


	public virtual void Start()
	{
		effectEndSource += this.effectEnd;
	}
}
