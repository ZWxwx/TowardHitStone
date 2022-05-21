using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreate : MonoBehaviour
{
    // Start is called before the first frame update
    MeteoriteCreateSystem metSystem;

    [Header("是否开始生成")]
    public bool isItemCreating;//控制是否生成

    [Tooltip("随机道具实例")]
    [Header("随机道具实例")]
    public GameObject ItemObject;
    [Header("道具父对象")]
    public GameObject ItemFather;
    private void Awake()
    {
        metSystem = gameObject.GetComponent<MeteoriteCreateSystem>();
    }
    private void FixedUpdate()
    {
        if (isItemCreating)
        {
            CreateInMode(metSystem.cMode);
        }
    }
    [Header("道具生成频率(多少秒生成一个)")]
    public float ItemCreateQuency;
    [Header("道具生成频率的偏差值，单位为秒")]
    [Tooltip("等于0即没有偏差 会同时向左右偏差同一个值")]
    public float ItemQuencyDevince;

    [Header("道具总体大小")]
    public float ItemSizeGeneral;
    [Header("道具大小随机偏差值")]
    [Tooltip("通常建议这个数值调为0")]
    public float ItemSizeDevince;
    [Header("道具速度")]
    public float XspeedMin;
    public float XspeedMax, YspeedMin, YspeedMax;

    
    private void giveSpeedAndDirection(GameObject Item,MeteoriteCreateSystem.Player player)//初始速度以及方向调整
    {
        Rigidbody2D Rb;
        //如果没有2D刚体组件 则创建一个 不过好像这里不太能实现
        Rb = Item.GetComponent<Rigidbody2D>() ?? Item.AddComponent<Rigidbody2D>();
        int dir = 0;
        switch (player.tag)
        {
            case PlayerType.Player1: dir = -1; break;
            case PlayerType.Player2: dir = 1; break;
        }

        Vector2 Ydir = new Vector2((player.Area.vector1.x + player.Area.vector2.x) / 2 - Item.transform.position.x,
            (player.Area.vector1.y + player.Area.vector2.y) / 2 - Item.transform.position.y);

        Rb.velocity = new Vector2(Random.Range(XspeedMin, XspeedMax) * dir, Random.Range(YspeedMin, YspeedMax) * Ydir.y);
    }
    private void giveSpeedAndDirection(GameObject Item, GameObject Item2)//同时双陨石时 初始速度以及方向调整
    {
        Rigidbody2D Rb;
        //如果没有2D刚体组件 则创建一个 不过好像这里不太能实现
        Rb = Item.GetComponent<Rigidbody2D>() ?? Item.AddComponent<Rigidbody2D>();
        int dir = -1;


        Vector2 Ydir = new Vector2((metSystem.p1.Area.vector1.x + metSystem.p1.Area.vector2.x) / 2 - Item.transform.position.x,
            (metSystem.p1.Area.vector1.y + metSystem.p1.Area.vector2.y) / 2 - Item.transform.position.y);
        float xspeed = Random.Range(XspeedMin, XspeedMax);
        Rb.velocity = new Vector2(xspeed * dir, Random.Range(YspeedMin, YspeedMax) * Ydir.y);

        Rb = Item2.GetComponent<Rigidbody2D>() ?? Item.AddComponent<Rigidbody2D>();
        dir = -dir;
        Rb.velocity = new Vector2(xspeed * dir, Random.Range(YspeedMin, YspeedMax) * Ydir.y);
    }

    public GameObject CreateItem(MeteoriteCreateSystem.Player player)
    {
        GameObject ItemIns = metSystem.CreateItem(player,ItemObject);
        giveSpeedAndDirection(ItemIns, player);
        ItemIns.GetComponent<RandomItemObj>().ForWhichPlayer = player.tag;
        return ItemIns;
    }
    public GameObject CreateItem()
    {
        GameObject ItemIns = metSystem.CreateItem(metSystem.p1, ItemObject);
        GameObject ItemIns2 = metSystem.CreateItem(metSystem.p2, ItemObject);
        ItemIns2.transform.position = new Vector3(ItemIns2.transform.position.x, ItemIns.transform.position.y, 0);
        giveSpeedAndDirection(ItemIns, ItemIns2);
        ItemIns.GetComponent<RandomItemObj>().ForWhichPlayer = PlayerType.Player1;
        ItemIns2.GetComponent<RandomItemObj>().ForWhichPlayer = PlayerType.Player2;
        ItemIns.GetComponent<RandomItemObj>().itemContent = ItemIns2.GetComponent<RandomItemObj>().itemContent = ItemManager.Instance.ItemRandom();
        return ItemIns;
    }

    private void CreateInMode(MeteoriteCreateSystem.creatingMode c)//理论上这串代码可以优化
    {

        switch (c)
        {
            case MeteoriteCreateSystem.creatingMode.FullSymmetry:
                {
                    if (metSystem.p1.itemCreateRate <= 0)
                    {
                        CreateItem();
                        metSystem.p1.itemCreateRate = ItemCreateQuency + ItemQuencyDevince * Random.Range(-1f, 1f);
                    }
                    else
                    {
                        metSystem.p1.itemCreateRate -= Time.deltaTime;
                    }
                    break;
                }
            case MeteoriteCreateSystem.creatingMode.NumSymmetry:
                {
                    if (metSystem.p1.itemCreateRate <= 0)
                    {
                        CreateItem(metSystem.p1);
                        CreateItem(metSystem.p2);
                        metSystem.p1.itemCreateRate = ItemCreateQuency + ItemQuencyDevince * Random.Range(-1f, 1f);
                    }
                    else
                    {
                        metSystem.p1.itemCreateRate -= Time.deltaTime;
                    }

                    break;
                }
            case MeteoriteCreateSystem.creatingMode.FullRandom:
                {
                    void CreateInline(MeteoriteCreateSystem.Player p)//当成内联函数吧，别在意了
                    {
                        if (p.itemCreateRate <= 0)
                        {
                            CreateItem(p);
                            p.itemCreateRate = ItemCreateQuency + ItemQuencyDevince * Random.Range(-1f, 1f);
                        }
                        else
                        {
                            p.itemCreateRate -= Time.deltaTime;
                        }
                    }
                    CreateInline(metSystem.p1);
                    CreateInline(metSystem.p2);
                    break;
                }
        }
    }


    [EditorButton]
    void Debug_CreateOneItem()
    {

        switch (metSystem.cMode)
        {
            case MeteoriteCreateSystem.creatingMode.FullSymmetry:
                {

                    CreateItem();
                    break;
                }
            case MeteoriteCreateSystem.creatingMode.NumSymmetry:
            case MeteoriteCreateSystem.creatingMode.FullRandom:
                {
                    CreateItem(metSystem.p1);
                    CreateItem(metSystem.p2);
                    break;
                }

        }
    }
}
