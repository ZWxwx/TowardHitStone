using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemObj : MonoBehaviour
{
    public ItemType itemContent=ItemType.None;
    public PlayerType ForWhichPlayer;//记录属于哪个玩家的
    //如果要设计可以同时出现在两个玩家半场的道具，那就在子弹击中后，更新这一词条即可
    //如果子弹没加入发射玩家的标记，那就只能根据虫洞的相对位置来判断是哪一方玩家击中的了

    public float health;//所以为什么要在血条那里设置生命值呢 不理解Orz

    public void contentChange(ItemType t)
    {
        itemContent = t;
    }

    [EditorButton]
    public void ItemBonusGet()//打碎物品时提供奖励
    {
        if (itemContent == ItemType.None)
        {
            itemContent = ItemManager.Instance.ItemRandom();
        }
        if (ForWhichPlayer == PlayerType.Player1)
        {
            ItemManager.Instance.itemsPlayer1.Add(new Item(itemContent, ForWhichPlayer));
        }
        else
        {
            ItemManager.Instance.itemsPlayer2.Add(new Item(itemContent, ForWhichPlayer));
        }
        Debug.Log("提供玩家"+ForWhichPlayer.ToString()+itemContent.ToString());
        Destroy(gameObject);
    }
    

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
