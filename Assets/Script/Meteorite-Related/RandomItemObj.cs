using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemObj : MonoBehaviour
{

    public PlayerType ForWhichPlayer;//��¼�����ĸ���ҵ�
    //���Ҫ��ƿ���ͬʱ������������Ұ볡�ĵ��ߣ��Ǿ����ӵ����к󣬸�����һ��������
    //����ӵ�û���뷢����ҵı�ǣ��Ǿ�ֻ�ܸ��ݳ涴�����λ�����ж�����һ����һ��е���

    public float health;//����ΪʲôҪ��Ѫ��������������ֵ�� �����Orz
    


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
