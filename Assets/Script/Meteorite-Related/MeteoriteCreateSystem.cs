using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeteoriteCreateSystem : Singleton<MeteoriteCreateSystem>
{
    [Header("��ʯ������")]
    public GameObject Meteorite;//��ʯ
    public enum creatingMode//���ɵ�ģʽ
    { 
        empty,//Ĭ����ģʽ
        FullSymmetry,//��ȫ�Գ�ģʽ
        NumSymmetry,//�����Գ�ģʽ
        FullRandom,//������ȫ���
    }
    public creatingMode cMode;
   [Header("���ɷ�Χ��������ÿ����Ҷ�Ӧһ����������ģʽ�¿����ڱ༭���￴�����������")]
    public Rectangle[] Area=new Rectangle[2];
    public void CreateModeSet(creatingMode m)
    {
        cMode = m;
    }
    [Header("�Ƿ�ʼ����")]
    public bool isCreating;//�����Ƿ�����
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Rectangle f in Area)
        {
            f.drawArea();
        }
    }
}
[System.Serializable]
public class Rectangle //������ ���Բ��� �������C++ѧ�� ������һ��
{
    public Vector2 p1, p2;
    Rectangle(float x1,float y1,float x2,float y2)
    {
        p1 = new Vector2(x1, y1);
        p2 = new Vector2(x2, y2);
    }
    Rectangle(Vector2 v1,Vector2 v2)
    {
        p1 = v1;
        p2 = v2;
    }
    public void drawArea()
    {
        Vector3 tmp1,tmp2;
        Color c=Color.red;
        tmp1 = new Vector3(p1.x, p1.y, 0);
        tmp2 = new Vector3(p1.x, p2.y, 0);
        Debug.DrawLine(tmp1,tmp2,c);
        tmp1 = tmp2;
        tmp2.Set(p2.x, p2.y, 0);
        Debug.DrawLine(tmp1, tmp2, c);
        tmp1 = tmp2;
        tmp2.Set(p2.x, p1.y, 0);
        Debug.DrawLine(tmp1, tmp2, c);
        tmp1 = tmp2;
        tmp2.Set(p1.x, p1.y, 0);
        Debug.DrawLine(tmp1, tmp2, c);
    }
}