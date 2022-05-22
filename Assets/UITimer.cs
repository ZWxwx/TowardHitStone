using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
	[SerializeField]
	public Timer timer;
	public Text text;
	public void Update()
	{
		text.text = string.Format("{0}:{1}", ((int)((timer.timeSpots[timer.timeSpots.Count-1]-timer.iTime) / 60)).ToString(), 
			((int)(timer.timeSpots[timer.timeSpots.Count-1] - timer.iTime) % 60).ToString()) ;
	}
}
