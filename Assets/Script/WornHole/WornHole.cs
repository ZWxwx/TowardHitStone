using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WornHole : MonoBehaviour //�����Ͽ����õ�������˭�ܱ�֤�Ժ󲻳��ֶ����������
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void move(float xMove)
    {
        transform.position += new Vector3(xMove, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
