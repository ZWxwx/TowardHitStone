using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //User user = new User();
        User.Single.player1_items.Add(new Item(ItemType.Explodable_nextBomb,sprite:"Sprite/Icon1"));
        User.Single.player1_items.Add(new Item(ItemType.Freezing));
        User.Single.player2_items.Add(new Item(ItemType.None));
        Load();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void Load()
	{
        UIItemPlane ui1= ItemManager.Single.itemPlane1.GetComponent<UIItemPlane>();
        UIItemPlane ui2 = ItemManager.Single.itemPlane2.GetComponent<UIItemPlane>();
        ui1.items = User.Single.player1_items;
        ui2.items = User.Single.player2_items;
        ui1.Init();
        ui2.Init();
    }
}
