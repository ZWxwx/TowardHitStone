using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemPlane : MonoBehaviour
{
    public List<GameObject> slots;
    public GameObject itemPrefab;
    public PlayerType playerType;
	public void UpdateInfo()
	{
        int index=0;
        if (playerType == PlayerType.Player1) {
            for (int i = 0; i < ItemManager.Instance.itemsPlayer1.Count; i++)
            {
                if (slots[index].GetComponentsInChildren<UIItem>() != null)
                {
                    foreach(var kv in slots[i].GetComponentsInChildren<UIItem>())
					{
                        Destroy(kv.gameObject);
                    }
                    
                }
                if (ItemManager.Instance.itemsPlayer1[i].count != 0)
                {
                    UIItem uiItem = Instantiate(itemPrefab, slots[index].transform).GetComponent<UIItem>();
                    uiItem.item = ItemManager.Instance.itemsPlayer1[i];
                    uiItem.item.OnUsed += uiItem.UpdateInfo;
                    uiItem.itemPlane = this;
                    uiItem.Init();
                    index++;
                }

            }
        }
        //else
        //{
        //    for (int i = 0; i < ItemManager.Instance.itemsPlayer2.Count; i++)
        //    {
        //        //slots[i].name = ItemManager.Instance.itemsPlayer1[i].itemType.ToString();
        //        if (slots[index].GetComponentInChildren<UIItem>()!= null && slots[index].transform.GetChild(0).GetComponentInChildren<UIItem>().item.count == 0)
        //        {
        //            Destroy(slots[index].transform.GetChild(0));

        //        }
        //        if (ItemManager.Instance.itemsPlayer2[i].count != 0)
        //        {
        //            UIItem uiItem = Instantiate(itemPrefab, slots[index].transform).GetComponent<UIItem>();
        //            uiItem.item = ItemManager.Instance.itemsPlayer2[i];
        //            uiItem.item.OnUsed += UpdateInfo;
        //            uiItem.itemPlane = this;
        //            uiItem.Init();
        //            index++;
        //        }
        //    }
        //}
        else if (playerType == PlayerType.Player2)
        {
            for (int i = 0; i < ItemManager.Instance.itemsPlayer2.Count; i++)
            {
                if (slots[index].GetComponentsInChildren<UIItem>() != null)
                {
                    foreach (var kv in slots[i].GetComponentsInChildren<UIItem>())
                    {
                        Destroy(kv.gameObject);
                    }

                }
                if (ItemManager.Instance.itemsPlayer2[i].count != 0)
                {
                    UIItem uiItem = Instantiate(itemPrefab, slots[index].transform).GetComponent<UIItem>();
                    uiItem.item = ItemManager.Instance.itemsPlayer2[i];
                    uiItem.item.OnUsed += uiItem.UpdateInfo;
                    uiItem.itemPlane = this;
                    uiItem.Init();
                    index++;
                }

            }
        }
    }

    public void Update()
	{
        if(playerType == PlayerType.Player1)
		{
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (slots[0].GetComponentInChildren<UIItem>() != null)
                    slots[0].GetComponentInChildren<UIItem>().item.use();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (slots[1].GetComponentInChildren<UIItem>() != null)
                    slots[1].GetComponentInChildren<UIItem>().item.use();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (slots[2].GetComponentInChildren<UIItem>() != null)
                    slots[2].GetComponentInChildren<UIItem>().item.use();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (slots[3].GetComponentInChildren<UIItem>() != null)
                    slots[3].GetComponentInChildren<UIItem>().item.use();
            }
        }
		else if(playerType == PlayerType.Player2)
		{
			if (Input.GetKeyDown(KeyCode.Keypad1))
			{
                if(slots[0].GetComponentInChildren<UIItem>()!=null)
                    slots[0].GetComponentInChildren<UIItem>().item.use();
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                if (slots[1].GetComponentInChildren<UIItem>() != null)
                    slots[1].GetComponentInChildren<UIItem>().item.use();
            }
        }

	}
}
