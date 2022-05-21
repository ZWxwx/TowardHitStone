using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public enum playerEnum //全局枚举 用来判断为那个玩家生成的内容    ///
//
//使用PlayerTap进行替代
//
//{ 
//    pL=0,
//    pR=1
//}

public class MeteoriteCreateSystem : Singleton<MeteoriteCreateSystem>
{
    #region 变量
    ItemCreate itemCreateSystem;

    public System.Action<MeteoriteObject> addNewMeteorite;

    public List<GameObject> MeteoriteListLeft, MeteoriteListRight;
    public List<GameObject>[] MeteoriteList = new List<GameObject>[2];
    public const int playerLeft = 0, playerRight = 1;

    [Header("击中陨石之后 让虫洞移动的距离倍数")]
    public float WornHoleMoveSpeed;


    [Header("陨石的物体")]
    public GameObject Meteorite;//陨石
    [Header("陨石父对象")]
    public GameObject MeteoriteFather;
    [SerializeField]
    [Header("陨石速度")]
    public float XspeedMin;
    public float XspeedMax,YspeedMin,YspeedMax;

    [Header("陨石总体大小")]
    public float MeteoriteSizeGeneral;
    [Header("陨石大小随机偏差值")]
    public float MeteoriteSizeDevince;

    [HideInInspector]
    public float targetWornHoleX; //预计的虫洞x坐标
    //[HideInInspector]
    //public bool isTargetWornHoleXChanged;//检测虫洞预计坐标有无改变

    [Header("陨石生成频率(多少秒生成一个) 可小于1")]
    [SerializeField]
    public float CreateQuency;
    [Header("陨石生成频率的偏差值，单位为秒 等于0即没有偏差 会同时向左右偏差同一个值")]
    public float QuencyDevince;
    public enum creatingMode//生成的模式
    { 
        [InspectorName("默认无模式")]
        empty,//默认无模式
        [InspectorName("完全对称模式")]
        FullSymmetry,//完全对称模式
        [InspectorName("数量对称模式")]
        NumSymmetry,//数量对称模式
        [InspectorName("两边完全随机")]
        FullRandom,//两边完全随机
    }
    public creatingMode cMode;
    [Header("生成范围有两个，每个玩家对应一个 在运行模式下可以在编辑器里看到具体的区域")]
    public Rectangle[] Area=new Rectangle[2];
    public void CreateModeSet(creatingMode m) // 设置createMode的代码，只是起方便
    {
        cMode = m;
    }
    [Header("是否开始生成")]
    public bool isCreating;//控制是否生成
    [HideInInspector]
    public Player p1, p2;

    [Header("虫洞移动距离和陨石血量之比")]
    public float moveareasSizePerHealth;
    [Header("是否开启虫洞延时移动")]
    public bool isLatencyMoveWornHole;
    [Header("虫洞延时时间 单位 秒")]
    public float WornHoleLatencyTime;

    //

    [Header("击毁陨石的基准分数，以scale为1为基准")]
    public float baseScore;
    //[Header("陨石分数和陨石血量之比")]
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

        if(isCreating)
        {
            CreateInMode(cMode);
        }
        
    }
    private void FixedUpdate()
    {
        lateMoveWornHole();
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
    #region 陨石生成相关
    private void CreateInMode(creatingMode c)//理论上这串代码可以优化
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
                        p1.createRate -= Time.deltaTime;
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
                        p1.createRate -= Time.deltaTime;
                    }

                    break;
                }
            case creatingMode.FullRandom:
                {
                    void CreateInline(Player p)//当成内联函数吧，别在意了
                    {
                        if (p.createRate <= 0)
                        {
                            CreateMeteorite(p);
                            p.createRate = CreateQuency + QuencyDevince * Random.Range(-1f, 1f);
                        }
                        else
                        {
                            p.createRate -= Time.deltaTime;
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
    public GameObject CreateMeteorite(Player player) //只生成一边的陨石
    {
        float getX()
        {
            return (player.Area.vector1.x + player.Area.vector2.x) / 2;
        }
        GameObject met;
        Transform tr;
        if (MeteoriteFather == null) //如果父对象没有 则以系统本身为父对象
        {
            tr = transform;
        }
        else {
            tr = MeteoriteFather.transform;
        }
        met = Instantiate(Meteorite, tr.position, Quaternion.Euler(0, 0, Random.Range(0, 360)), tr);//生成陨石对象
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

    public GameObject CreateMeteorite()//同时生成两边的陨石
    {
        float getX(Player p)
        {
            return (p.Area.vector1.x + p.Area.vector2.x) / 2;
        }
        GameObject met,met2;
        Transform tr;
        if (MeteoriteFather == null) //如果父对象没有 则以系统本身为父对象
        {
            tr = transform;
        }
        else
        {
            tr = MeteoriteFather.transform;
        }
        met = Instantiate(Meteorite, tr);//生成陨石对象
        met2 = Instantiate(Meteorite,  tr);//生成陨石对象  
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
    void setMeteoritePlayer(GameObject met, Player p) //定义陨石为哪方玩家生成
    {
        met.GetComponent<MeteoriteObject>().ForWhichPlayer = p.tag;
    }
    void setMeteoriteScore(MeteoriteObject meteorite)
	{
        meteorite.score=baseScore*meteorite.transform.localScale.x;
}

    private void giveSpeedAndDirection(GameObject met, Player player)//初始速度以及方向调整
    {
        Rigidbody2D Rb;
        //如果没有2D刚体组件 则创建一个 不过好像这里不太能实现
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
    private void giveSpeedAndDirection(GameObject met, GameObject met2)//同时双陨石时 初始速度以及方向调整
    {
        Rigidbody2D Rb;
        //如果没有2D刚体组件 则创建一个 不过好像这里不太能实现
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

    private void setMeteroiteSize(GameObject met,float size=0) //设置陨石大小
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
    /// 以下为生成陨石位置的随机生成代码，需要优化请在此修改
    /// </summary>
    /// <returns></returns>
    private float RandomPosGenerateForY(Rectangle _Area)//因为目前本项目与X位置无关，暂时只随机Y轴
    {
        ///
        //正态分布写到吐血 不用了
        /*
        float BoxMuller()//正太分布函数 BoxMuller转化法
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
    } //预留的 似乎没啥用....
    private WornHole WormHoleObj;
    public void moveArea(PlayerType playerTag, float value) //让两个区域一起移动x距离
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
            //Debug.Log("开始移动" + targetWornHoleX);
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
               // Debug.Log(WornHoleLateMoveTime);
                p1.Area.vector1.x += WornHoleLateMoveSpeed;
                p1.Area.vector2.x += WornHoleLateMoveSpeed;
                p2.Area.vector1.x += WornHoleLateMoveSpeed;
                p2.Area.vector2.x += WornHoleLateMoveSpeed;
                WormHoleObj.move(WornHoleLateMoveSpeed);
                WornHoleLateMoveTime -= Time.deltaTime;
                if (Mathf.Abs((targetWornHoleX - WormHoleObj.transform.position.x)) < 0.005f)
                {
                    WornHoleLateMoveTime = 0;
                    p1.Area.vector1.x = targetWornHoleX;
                    p1.Area.vector2.x = targetWornHoleX;
                    p2.Area.vector1.x = targetWornHoleX;
                    p2.Area.vector2.x = targetWornHoleX;
                    WormHoleObj.transform.position=new Vector3(targetWornHoleX, WormHoleObj.transform.position.y, WormHoleObj.transform.position.z);
                    //Debug.Log("距离达到而停止");
                }
            }
          
        }
    }
    [EditorButton]
    public void Debug_wornHoleMove()
    {
        moveArea(PlayerType.Player1,1000f);
    }

    /// <summary>
    /// 提供给物品生成的脚本
    /// </summary>
    /// <param name="player">玩家</param>
    /// <returns></returns>
    public GameObject CreateItem(Player player,GameObject itemPrefeb) //直接为生成道具那边的脚本铺路了 
    {
        float getX()
        {
            return (player.Area.vector1.x + player.Area.vector2.x) / 2;
        }
        GameObject item;
        Transform tr;
        if (itemCreateSystem.ItemFather == null) //如果父对象没有 则以系统本身为父对象
        {
            tr = transform;
        }
        else
        {
            tr = itemCreateSystem.ItemFather.transform;
        }
        item = Instantiate(itemPrefeb, tr.position, Quaternion.Euler(0, 0, Random.Range(0, 360)), tr);//生成物品对象
        var t = RandomPosGenerateForY(player.Area);
        item.transform.position = new Vector3(getX(), t, 0);
        giveSpeedAndDirection(item, player);
        //setMeteroiteSize(item);
        //setMeteoriteScore(met.GetComponent<MeteoriteObject>());
        //Debug.Log(t);
        //setMeteoritePlayer(item, player);
        //ifa (addNewMeteorite != null)
        //{
        //    addNewMeteorite(item.GetComponent<MeteoriteObject>());
        //}

        return item;
    }

    public class Player//玩家类
    {
        public PlayerType tag;
        public Rectangle Area;//对应的生成区域
        public int ListTag;
        public float createRate, createMaxRate;//玩家陨石生成计时器
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
public class Rectangle //矩形类 可以不用 但是最近C++学了 还是用一下
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
