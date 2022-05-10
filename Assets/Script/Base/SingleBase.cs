using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBase : MonoBehaviour
{
    private static SingleBase _instance;
    public static SingleBase instance
	{
		get
		{
            return _instance;
		}
		set
		{
            if(_instance==null)
                _instance = value;
		}
	}
}
