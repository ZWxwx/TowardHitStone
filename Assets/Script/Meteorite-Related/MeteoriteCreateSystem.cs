using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public enum playerEnum //ȫ��ö�� �����ж�Ϊ�Ǹ�������ɵ�����    ///
//
//ʹ��PlayerTap�������
//
//{ 
//    pL=0,
//    pR=1
//}

public class MeteoriteCreateSystem : Singleton<MeteoriteCreateSystem>
{
    #region ����
    ItemCreate itemCreateSystem;

    public System.Action<MeteoriteObject> addNewMeteorite;

    public List<GameObject> MeteoriteListLeft, MeteoriteListRight;
    public List<GameObject>[] MeteoriteList = new List<GameObject>[2];
    public const int playerLeft = 0, playerRight = 1;

    [Header("������ʯ֮�� �ó涴�ƶ��ľ��뱶��")]
    public float WornHoleMoveSpeed;


    [Header("��ʯ������")]
    public GameObject Meteorite;//��ʯ
    [Header("��ʯ������")]
    public GameObject MeteoriteFather;
    [SerializeField]
    [Header("��ʯ�ٶ�")]
    public float XspeedMin;
    public float XspeedMax,YspeedMin,YspeedMax;

    [Header("��ʯ�����С")]
    public float MeteoriteSizeGeneral;
    [Header("��ʯ��С���ƫ��ֵ")]
    public float MeteoriteSizeDevince;

    [HideInInspector]
    public float targetWornHoleX; //Ԥ�Ƶĳ涴x����
    //[HideInInspector]
    //public bool isTargetWornHoleXChanged;//���涴Ԥ���������޸ı�

    [Header("��ʯ����Ƶ��(����������һ��) ��С��1")]
    [SerializeField]
    public float CreateQuency;
    [Header("��ʯ����Ƶ�ʵ�ƫ��ֵ����λΪ�� ����0��û��ƫ�� ��ͬʱ������ƫ��ͬһ��ֵ")]
    public float QuencyDevince;
    public enum creatingMode//���ɵ�ģʽ
    { 
        [InspectorName("Ĭ����ģʽ")]
        empty,//Ĭ����ģʽ
        [InspectorName("��ȫ�Գ�ģʽ")]
        FullSymmetry,//��ȫ�Գ�ģʽ
        [InspectorName("�����Գ�ģʽ")]
        NumSymmetry,//�����Գ�ģʽ
        [InspectorName("������ȫ���")]
        FullRandom,//������ȫ���
    }
    public creatingMode cMode;
    [Header("���ɷ�Χ��������ÿ����Ҷ�Ӧһ�� ������ģʽ�¿����ڱ༭���￴�����������")]
    public Rectangle[] Area=new Rectangle[2];
    public void CreateModeSet(creatingMode m) // ����createMode�Ĵ��룬ֻ���𷽱�
    {
        cMode = m;
    }
    [Header("�Ƿ�ʼ����")]
    public bool isCreating;//�����Ƿ�����
    [HideInInspector]
    public Player p1, p2;

    [Header("�涴�ƶ��������ʯѪ��֮��")]
    public float moveareasSizePerHealth;
    [Header("�Ƿ����涴��ʱ�ƶ�")]
    public bool isLatencyMoveWornHole;
    [Header("�涴��ʱʱ�� ��λ ��")]
    public float WornHoleLatencyTime;

    //

    [Header("������ʯ�Ļ�׼��������scaleΪ1Ϊ��׼")]
    public float baseScore;
    //[Header("��ʯ��������ʯѪ��֮��")]
    #endregion
    public void emptyFunc(MeteoriteObject met)
	{
        ;
	}
    private void Awake()
    {
        p1 = new Player(PlayerType.Player1, Area[0],playerLeft);
        p2 = new Player(PlayerType.Player2, Area[1],playerRight);
        MeteoriteList[0] = new List<GameObject>();
        MeteoriteList[1] = new List<GameObject>();
        MeteoriteListLeft = MeteoriteList[playerLeft];
        MeteoriteListRight = MeteoriteList[playerRight];
        WormHoleObj = GameObject.Find("WormHole").GetComponent<WornHole>();
        targetWornHoleX = WormHoleObj.transform.position.x;
        itemCreateSystem = gameObject.GetComponent<ItemCreate>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Rectangle f in Area)
        {
            f.drawArea();
        }

        
        
    }
    private void FixedUpdate()
    {
        lateMoveWornHole();
        if (isCreating)
        {
            CreateInMode(cMode);
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
    #region ��ʯ�������
    private void CreateInMode(creatingMode c)//�������⴮��������Ż�
    {
        
        switch (c)
        {
            case creatingMode.FullSymmetry:
                {
                    if (p1.createRate <= 0)
                    {
                        CreateMeteorite();
                        p1.createRate =  CreateQuency + QuencyDevince * Random.Range(-1f, 1f);
                    }
                    else 
                    {
                        p1.createRate -= Time.fixedDeltaTime;
                    }
                    break;
                }
            case creatingMode.NumSymmetry:
                {
                    if (p1.createRate <= 0)
                    {
                        CreateMeteorite(p1);
                        CreateMeteorite(p2);
                        p1.createRate = CreateQuency + QuencyDevince * Random.Range(-1f, 1f);
                    }
                    else
                    {
                        p1.createRate -= Time.fixedDeltaTime;
                    }

                    break;
                }
            case creatingMode.FullRandom:
                {
                    void CreateInline(Player p)//�������������ɣ���������
                    {
                        if (p.createRate <= 0)
                        {
                            CreateMeteorite(p);
                            p.createRate = CreateQuency + QuencyDevince * Random.Range(-1f, 1f);
                        }
                        else
                        {
                            p.createRate -= Time.fixedDeltaTime;
                        }
                    }
                    CreateInline(p1);
                    CreateInline(p2);
                    break;
                }
        }
    }


    [EditorButton]
    void Debug_CreateOne()
    {

        switch (cMode)
        {
            case creatingMode.FullSymmetry:
                {
                    
                    CreateMeteorite();
                    break;
                }
            case creatingMode.NumSymmetry:
            case creatingMode.FullRandom:
                {
                        CreateMeteorite(p1);
                        CreateMeteorite(p2);
                        break;
                }
               
        }
    }
    public GameObject CreateMeteorite(Player player) //ֻ����һ�ߵ���ʯ
    {
        float getX()
        {
            return (player.Area.vector1.x + player.Area.vector2.x) / 2;
        }
        GameObject met;
        Transform tr;
        if (MeteoriteFather == null) //���������û�� ����ϵͳ����Ϊ������
        {
            tr = transform;
        }
        else {
            tr = MeteoriteFather.transform;
        }
        met = Instantiate(Meteorite, tr.position, Quaternion.Euler(0, 0, Random.Range(0, 360)), tr);//������ʯ����
        var t = RandomPosGenerateForY(player.Area);
        met.transform.position=new Vector3(getX(),t,0);
        giveSpeedAndDirection(met,player);
        setMeteroiteSize(met);
        setMeteoriteScore(met.GetComponent<MeteoriteObject>());
        //Debug.Log(t);
        setMeteoritePlayer(met, player);
		if (addNewMeteorite != null)
		{
            addNewMeteorite(met.GetComponent<MeteoriteObject>());
        }
        
        return met;
    }

    public GameObject CreateMeteorite()//ͬʱ�������ߵ���ʯ
    {
        float getX(Player p)
        {
            return (p.Area.vector1.x + p.Area.vector2.x) / 2;
        }
        GameObject met,met2;
        Transform tr;
        if (MeteoriteFather == null) //���������û�� ����ϵͳ����Ϊ������
        {
            tr = transform;
        }
        else
        {
            tr = MeteoriteFather.transform;
        }
        met = Instantiate(Meteorite, tr);//������ʯ����
        met2 = Instantiate(Meteorite,  tr);//������ʯ����  
        var t = RandomPosGenerateForY(p1.Area);
        met.transform.position = new Vector3(getX(p1), t, 0);
        met2.transform.position = new Vector3(getX(p2), t, 0);
        giveSpeedAndDirection(met,met2);
        setMeteoritePlayer(met, p1);
        setMeteoritePlayer(met2, p2);
        setMeteroiteSize(met);
        met2.transform.localScale = met.transform.localScale;
        setMeteoriteScore(met.GetComponent<MeteoriteObject>());
        setMeteoriteScore(met2.GetComponent<MeteoriteObject>());
        //Debug.Log(t);
        return met;
    }
    void setMeteoritePlayer(GameObject met, Player p) //������ʯΪ�ķ��������
    {
        met.GetComponent<MeteoriteObject>().ForWhichPlayer = p.tag;
    }
    void setMeteoriteScore(MeteoriteObject meteorite)
	{
        meteorite.score=baseScore*meteorite.transform.localScale.x;
}

    private void giveSpeedAndDirection(GameObject met, Player player)//��ʼ�ٶ��Լ��������
    {
        Rigidbody2D Rb;
        //���û��2D������� �򴴽�һ�� �����������ﲻ̫��ʵ��
        Rb = met.GetComponent<Rigidbody2D>() ?? met.AddComponent<Rigidbody2D>();
        int dir=0;
        switch (player.tag)
        {
            case PlayerType.Player1: dir = -1; break;
            case PlayerType.Player2: dir = 1; break;
        }

        Vector2 Ydir = new Vector2((player.Area.vector1.x + player.Area.vector2.x) / 2 - met.transform.position.x,
            (player.Area.vector1.y + player.Area.vector2.y) / 2 - met.transform.position.y);

        Rb.velocity = new Vector2(Random.Range(XspeedMin,XspeedMax)*dir, Random.Range(YspeedMin, YspeedMax) * Ydir.y);

    }
    private void giveSpeedAndDirection(GameObject met, GameObject met2)//ͬʱ˫��ʯʱ ��ʼ�ٶ��Լ��������
    {
        Rigidbody2D Rb;
        //���û��2D������� �򴴽�һ�� �����������ﲻ̫��ʵ��
        Rb = met.GetComponent<Rigidbody2D>() ?? met.AddComponent<Rigidbody2D>();
        int dir = -1;
    

        Vector2 Ydir = new Vector2((p1.Area.vector1.x + p1.Area.vector2.x) / 2 - met.transform.position.x,
            (p1.Area.vector1.y + p1.Area.vector2.y) / 2 - met.transform.position.y);
        float xspeed = Random.Range(XspeedMin, XspeedMax),yspeed= Random.Range(YspeedMin, YspeedMax);
        Rb.velocity = new Vector2(xspeed * dir, yspeed * Ydir.y);

        Rb = met2.GetComponent<Rigidbody2D>() ?? met.AddComponent<Rigidbody2D>();
        dir = -dir;
        Rb.velocity = new Vector2(xspeed * dir, yspeed * Ydir.y);
    }

    private void setMeteroiteSize(GameObject met,float size=0) //������ʯ��С
    {
        if(size==0)
        {
            size = MeteoriteSizeGeneral+Random.Range(-MeteoriteSizeDevince,MeteoriteSizeDevince);
        }
        if(size<=0)
        { size = 0.01f; }
        met.transform.localScale = new Vector3(size, size, 0);
    }

    /// <summary>
    /// ����Ϊ������ʯλ�õ�������ɴ��룬��Ҫ�Ż����ڴ��޸�
    /// </summary>
    /// <returns></returns>
    private float RandomPosGenerateForY(Rectangle _Area)//��ΪĿǰ����Ŀ��Xλ���޹أ���ʱֻ���Y��
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
    #endregion
    public void setArea(Rectangle newAreaLeft,Rectangle newAreaRight)
    {
        p1.Area = newAreaLeft;
        p2.Area = newAreaRight;
    } //Ԥ���� �ƺ�ûɶ��....
    #region �涴�ƶ����
    private WornHole WormHoleObj;
    public void moveArea(PlayerType playerTag, float value) //����������һ���ƶ�x����
    {
            float xMove = 0;
            if (playerTag == PlayerType.Player1)
            {
                xMove = value * moveareasSizePerHealth;
            }
            else if (playerTag == PlayerType.Player2)
            {
                xMove = -1 * value * moveareasSizePerHealth;
            }
            xMove *= WornHoleMoveSpeed;
        if (!isLatencyMoveWornHole)
        {
            p1.Area.vector1.x += xMove;
            p1.Area.vector2.x += xMove;
            p2.Area.vector1.x += xMove;
            p2.Area.vector2.x += xMove;
            WormHoleObj.move(xMove);
        }
        else 
        {
            targetWornHoleX += xMove;
            //Debug.Log("xMove="+xMove);
            WornHoleLateMoveSpeed = ReGetWornHoleSpeed();
            WornHoleLateMoveTime = WornHoleLatencyTime;
            //Debug.Log("��ʼ�ƶ�" + targetWornHoleX);
        }
    }
    private float WornHoleLateMoveSpeed=0, WornHoleLateMoveTime=0;
    private float ReGetWornHoleSpeed()
    {
        float speed;
        speed = ((targetWornHoleX-WormHoleObj.transform.position.x) / WornHoleLatencyTime * Time.fixedDeltaTime);
        //Debug.Log(WormHoleObj.transform.position.x+"-"+targetWornHoleX+"="+(targetWornHoleX - WormHoleObj.transform.position.x));
        //Debug.Log((WormHoleObj.transform.position.x - targetWornHoleX)+"/"+ WornHoleLatencyTime+"="+(WormHoleObj.transform.position.x - targetWornHoleX) / WornHoleLatencyTime);
        return speed;
    }
    private void lateMoveWornHole()
    {
        if (isLatencyMoveWornHole)
        {
            if (WornHoleLateMoveTime > 0)
            {
                Debug.Log(WornHoleLateMoveTime);
                p1.Area.vector1.x += WornHoleLateMoveSpeed;
                p1.Area.vector2.x += WornHoleLateMoveSpeed;
                p2.Area.vector1.x += WornHoleLateMoveSpeed;
                p2.Area.vector2.x += WornHoleLateMoveSpeed;
                WormHoleObj.move(WornHoleLateMoveSpeed);
                WornHoleLateMoveTime -= Time.fixedDeltaTime;
                if (Mathf.Abs((targetWornHoleX - WormHoleObj.transform.position.x)) < 0.005f)
                {
                    WornHoleLateMoveTime = 0;
                    p1.Area.vector1.x = targetWornHoleX;
                    p1.Area.vector2.x = targetWornHoleX;
                    p2.Area.vector1.x = targetWornHoleX;
                    p2.Area.vector2.x = targetWornHoleX;
                    WormHoleObj.transform.position=new Vector3(targetWornHoleX, WormHoleObj.transform.position.y, WormHoleObj.transform.position.z);
                    Debug.Log("����ﵽ��ֹͣ");
                }
            }
          
        }
    }
    [EditorButton]
    public void Debug_wornHoleMove()
    {
        moveArea(PlayerType.Player1,1000f);
    }
    #endregion
    /// <summary>
    /// �ṩ����Ʒ���ɵĽű�
    /// </summary>
    /// <param name="player">���</param>
    /// <returns></returns>
    public GameObject CreateItem(Player player,GameObject itemPrefeb) //ֱ��Ϊ���ɵ����ǱߵĽű���·�� 
    {
        float getX()
        {
            return (player.Area.vector1.x + player.Area.vector2.x) / 2;
        }
        GameObject itemIns;
        Transform tr;
        if (itemCreateSystem.ItemFather == null) //���������û�� ����ϵͳ����Ϊ������
        {
            tr = transform;
        }
        else
        {
            tr = itemCreateSystem.ItemFather.transform;
        }
        itemIns = Instantiate(itemPrefeb, tr.position, Quaternion.Euler(0, 0, Random.Range(0, 360)), tr);//������Ʒ����
        var t = RandomPosGenerateForY(player.Area);
        itemIns.transform.position = new Vector3(getX(), t, 0);
        giveSpeedAndDirection(itemIns, player);
        //setMeteroiteSize(item);
        //setMeteoriteScore(met.GetComponent<MeteoriteObject>());
        //Debug.Log(t);
        //setMeteoritePlayer(item, player);
        //ifa (addNewMeteorite != null)
        //{
        //    addNewMeteorite(item.GetComponent<MeteoriteObject>());
        //}

        return itemIns;
    }

    public class Player//�����
    {
        public PlayerType tag;
        public Rectangle Area;//��Ӧ����������
        public int ListTag;
        public float createRate, createMaxRate;//�����ʯ���ɼ�ʱ��
        public float itemCreateRate;
        public Player(PlayerType a,Rectangle b,int c)
        {
            createRate = 0f;
            itemCreateRate = 0f;
            tag = a;
            Area = b;
            ListTag = c;
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
