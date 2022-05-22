using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBackGroundController : MonoBehaviour
{
	public int v=0;
	public Vector3 v3= new Vector3(0.3f, 0.3f);
	public void Update()
	{
		this.transform.position += v3*Time.deltaTime;
		v++;
		if (v == 2000)
		{
			v3 = new Vector3(-v3.x, v3.y, v3.z);
		}
		if (v > 4000)
		{
			v = 0;
			v3 = new Vector3(v3.x, -v3.y, v3.z);
		}
	}
}
