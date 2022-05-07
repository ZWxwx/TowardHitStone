using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T _single;
    public static T Single
    {
        get
        {
            if (_single == null)
            {
                _single = FindObjectOfType<T>();
                if (_single == null)
                {
                    Debug.LogError(message: "������δ�ҵ�������ö�������Ϊ��" + typeof(T).Name);
                }
            }
            return _single;
        }
    }
    void Start()
    {
        if (_single == null)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
