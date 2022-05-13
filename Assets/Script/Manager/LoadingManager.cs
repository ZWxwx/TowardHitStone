using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //User user = new User();
        User.Instance.player1_items.Add(new Item(ItemType.Explodable_nextBomb,PlayerType.Player1,count:2,sprite:"Sprite/Icon1"));
        User.Instance.player1_items.Add(new Item(ItemType.Tracking, PlayerType.Player1, count: 3, sprite: "Sprite/Icon2"));
        User.Instance.player1_items.Add(new Item(ItemType.Explodable_nextBomb, PlayerType.Player1, count: 4, sprite: "Sprite/Icon1"));
        User.Instance.player1_items.Add(new Item(ItemType.Freezing, PlayerType.Player1));
        User.Instance.player2_items.Add(new Item(ItemType.None,  PlayerType.Player2, count: 5));
        Load();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void Load()
	{
        UIItemPlane ui1= ItemManager.Instance.itemPlane1.GetComponent<UIItemPlane>();
        UIItemPlane ui2 = ItemManager.Instance.itemPlane2.GetComponent<UIItemPlane>();
        ItemManager.Instance.itemsPlayer1= User.Instance.player1_items;
        ItemManager.Instance.itemsPlayer2 = User.Instance.player2_items;
        ui1.UpdateInfo();
        ui2.UpdateInfo();
    }
}
