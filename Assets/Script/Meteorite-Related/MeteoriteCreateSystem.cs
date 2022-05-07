using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeteoriteCreateSystem : Singleton<MeteoriteCreateSystem>
{
    [Header("陨石的物体")]
    public GameObject Meteorite;//陨石
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
}
[System.Serializable]
public class Rectangle //矩形类 可以不用 但是最近C++学了 还是用一下
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