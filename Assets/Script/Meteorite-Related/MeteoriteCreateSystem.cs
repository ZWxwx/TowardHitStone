using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum playerEnum //ȫ��ö�� �����ж�Ϊ�Ǹ�������ɵ�����
{ 
    pL=0,
    pR=1
}
public class MeteoriteCreateSystem : Singleton<MeteoriteCreateSystem>
{
    [Header("��ʯ������")]
    public GameObject Meteorite;//��ʯ
    [Header("��ʯ������")]
    public GameObject MeteortieFather;
    [SerializeField]
    [Header("��ʯ�ٶ�")]
    public float XspeedMin;
    public float XspeedMax,YspeedMin,YspeedMax;

    [Header("��ʯ����Ƶ��(1����ٸ�����С��1)")]
    [SerializeField]
    public float CreateQuency;
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

    Player p1, p2;
    // Start is called before the first frame update
    private void Awake()
    {
        p1 = new Player(playerEnum.pL, Area[0]);
        p2 = new Player(playerEnum.pR, Area[1]);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Rectangle f in Area)
        {
            f.drawArea();
        }

    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        UnityEditor.Handles.color = Color.green;
        for(int i = 0; i < Area.Length; i++)
		{
            UnityEditor.Handles.DrawWireCube((Area[i].vector1 + Area[i].vector2) / 2, Area[i].vector1 - Area[i].vector2);
        }
        //UnityEditor.Handles.ArrowHandleCap(0, this.transform.position, this.transform.rotation, 1f, EventType.Repaint);
    }
#endif

    [EditorButton]
    void Debug_Test()
    {
        CreateMeteorite(p1);
        CreateMeteorite(p2);
    }
    public GameObject CreateMeteorite(Player player)
    {
        float getX()
        {
            return (player.Area.vector1.x + player.Area.vector2.x) / 2;
        }
        GameObject met;
        Transform tr;
        if (MeteortieFather == null) //���������û�� ����ϵͳ����Ϊ������
        {
            tr = transform;
        }
        else {
            tr = MeteortieFather.transform;
        }
        met = Instantiate(Meteorite,tr);//������ʯ����
        var t = RandomPosGenerateForY(player.Area);
        met.transform.position=new Vector3(getX(),t,0);
        giveSpeedAndDirection(met,player);
        //Debug.Log(t);
        return met;
    }

    void giveSpeedAndDirection(GameObject met, Player player)//��ʼ�ٶ��Լ��������
    {
        Rigidbody2D Rb;
        //���û��2D������� �򴴽�һ�� �����������ﲻ̫��ʵ��
        Rb = met.GetComponent<Rigidbody2D>() ?? met.AddComponent<Rigidbody2D>();
        int dir=0;
        switch (player.tag)
        {
            case playerEnum.pL: dir = -1; break;
            case playerEnum.pR: dir = 1; break;
        }

        Vector2 Ydir = new Vector2((player.Area.vector1.x + player.Area.vector2.x) / 2 - met.transform.position.x,
            (player.Area.vector1.y + player.Area.vector2.y) / 2 - met.transform.position.y);

        Rb.velocity = new Vector2(Random.Range(XspeedMin,XspeedMax)*dir, Random.Range(YspeedMin, YspeedMax) * Ydir.y);

    }

    
    /// <summary>
    /// ����Ϊ������ʯλ�õ�������ɴ��룬��Ҫ�Ż����ڴ��޸�
    /// </summary>
    /// <returns></returns>
    float RandomPosGenerateForY(Rectangle _Area)//��ΪĿǰ����Ŀ��Xλ���޹أ���ʱֻ���Y��
    {
        ///
        //��̬�ֲ�д����Ѫ ������
        /*
        float BoxMuller()//��̫�ֲ����� BoxMullerת����
        {
            float p1, p2,w=0.0f,c;
            do {
                do
                {
                    p1 = Random.Range(0.01f, 1.01f);
                    p2 = Random.Range(0.01f, 1.01f);
                    w = p1 * p1 + p2 * p2;
                } while (w == 0.0f || w >= 1.0f);
                c = Mathf.Sqrt(-2 * Mathf.Log(p1)) * Mathf.Cos(2 * Mathf.PI * p2);
            } while (c < -5 || c > 5);
            c = c / 5 + 0.5f;
            Debug.Log(c);
            return c;
        }
        float Position;
        Position = Mathf.Max(_Area.vector1.y, _Area.vector2.y) - BoxMuller() * Mathf.Abs(_Area.vector1.y - _Area.vector2.y);
        return Position;
        */
        float Position;
        Position = Mathf.Max(_Area.vector1.y, _Area.vector2.y) - Random.Range(0.01f,0.99f) * Mathf.Abs(_Area.vector1.y - _Area.vector2.y);
        return Position;


    }
   public class Player//�����
    {
        public playerEnum tag;
        public Rectangle Area;//��Ӧ����������
        public Player(playerEnum a,Rectangle b)
        {
            tag = a;
            Area = b;
        }
    }
}



[System.Serializable]
public class Rectangle //������ ���Բ��� �������C++ѧ�� ������һ��
{
    public Vector2 vector1, vector2;

    Rectangle(float x1,float y1,float x2,float y2)
    {
        vector1 = new Vector2(x1, y1);
        vector2 = new Vector2(x2, y2);
    } 
    Rectangle(Vector2 v1,Vector2 v2)
    {
        vector1 = v1;
        vector2 = v2;
    }
    public void drawArea()
    {
        Vector3 tmp1,tmp2;
        Color c=Color.red;
        tmp1 = new Vector3(vector1.x, vector1.y, 0);
        tmp2 = new Vector3(vector1.x, vector2.y, 0);
        Debug.DrawLine(tmp1,tmp2,c);
        tmp1 = tmp2;
        tmp2.Set(vector2.x, vector2.y, 0);
        Debug.DrawLine(tmp1, tmp2, c);
        tmp1 = tmp2;
        tmp2.Set(vector2.x, vector1.y, 0);
        Debug.DrawLine(tmp1, tmp2, c);
        tmp1 = tmp2;
        tmp2.Set(vector1.x, vector1.y, 0);
        Debug.DrawLine(tmp1, tmp2, c);
    }
}
