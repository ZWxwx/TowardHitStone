using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemObj : MonoBehaviour
{
    public ItemType itemContent=ItemType.None;
    public PlayerType ForWhichPlayer;//��¼�����ĸ���ҵ�
    //���Ҫ��ƿ���ͬʱ������������Ұ볡�ĵ��ߣ��Ǿ����ӵ����к󣬸�����һ��������
    //����ӵ�û���뷢����ҵı�ǣ��Ǿ�ֻ�ܸ��ݳ涴�����λ�����ж�����һ����һ��е���

    public float health;//����ΪʲôҪ��Ѫ��������������ֵ�� �����Orz

    public void contentChange(ItemType t)
    {
        itemContent = t;
    }

    [EditorButton]
    public void ItemBonusGet()//������Ʒʱ�ṩ����
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
        Debug.Log("�ṩ���"+ForWhichPlayer.ToString()+itemContent.ToString());
        Destroy(gameObject);
    }
    

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
