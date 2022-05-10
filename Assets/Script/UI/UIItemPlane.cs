using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItemPlane : MonoBehaviour
{
    public GameObject[] slots;
    public List<Item> items;
    public GameObject itemPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
	{
		for (int i = 0;  i < items.Count; i++)
		{
            slots[i].name = items[i].itemType.ToString();
            UIItem uiItem=Instantiate(itemPrefab, slots[i].transform).GetComponent<UIItem>();
            uiItem.item = items[i];
            uiItem.Init();
		}
	}
}
