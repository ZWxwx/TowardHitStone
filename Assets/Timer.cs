using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool isOpen;
	 
	public float iTime;
	//�ϴ�ֹͣ��ʱ��ʱ��
	public float preTime;

	//ʱ��ڵ�
	public List<float> timeSpots;

	public bool IsOpen
	{
		get
		{
			return isOpen;
		}
		set
		{
			if (isOpen != value)
			{
				isOpen = value;
				if (isOpen)
				{
					StartCoroutine("restartTime");
				}
				else
				{
					preTime = iTime;
					iTime = 0;
					StopCoroutine("restartTime");
				}
			}
		}
	}

	[EditorButton]
	public void open()
	{
		IsOpen = true;
	}

	[EditorButton]

	public void close()
	{
		IsOpen = false;
	}



	public IEnumerator restartTime()
	{
		while (true)
		{
			iTime += Time.deltaTime;
			yield return null;
		}
	}
}
