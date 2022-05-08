using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum player //全局枚举 用来判断为那个玩家生成的内容
{ 
    pl=0,
    p2=1
}
public class MeteoriteCreateSystem : Singleton<MeteoriteCreateSystem>
{
    [Header("陨石的物体")]
    public GameObject Meteorite;//陨石
    [Header("陨石父对象")]
    public GameObject MeteortieFather;

    public enum creatingMode//生成的模式
    { 
        empty,//默认无模式
        FullSymmetry,//完全对称模式
        NumSymmetry,//数量对称模式
        FullRandom,//两边完全随机
    }
    public creatingMode cMode;
   [Header("生成范围有两个，每个玩家对应一个，在运行模式下可以在编辑器里看到具体的区域")]
    public Rectangle[] Area=new Rectangle[2];
    public void CreateModeSet(creatingMode m)
    {
        cMode = m;
    }
    [Header("是否开始生成")]
    public bool isCreating;//控制是否生成
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

    [EditorButton]
    public GameObject CreateMeteorite(player _player=player.pl)
    {
        Rectangle AreaNow=Area[0];
        switch (_player)
        {
            case player.pl:AreaNow = Area[0];break;
            case player.p2:AreaNow = Area[1]; break;
        }
        float getX()
        {
            return (AreaNow.vector1.x - AreaNow.vector2.x) / 2 - AreaNow.vector1.x;
        }
        GameObject met;
        Transform tr;
        if (MeteortieFather == null) //如果父对象没有 则以系统本身为父对象
        {
            tr = transform;
        }
        else {
            tr = MeteortieFather.transform;
        }
        met = Instantiate(Meteorite,tr);
        var t = RandomPosGenerateForY(AreaNow);
        met.transform.position=new Vector3(getX(),t,0);
        //Debug.Log(t);
        return met;
    }
    /// <summary>
    /// 以下为生成陨石位置的随机生成代码，需要优化请在此修改
    /// </summary>
    /// <returns></returns>
    float RandomPosGenerateForY(Rectangle _Area)//因为目前本项目与X位置无关，暂时只随机Y轴
    {
        
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