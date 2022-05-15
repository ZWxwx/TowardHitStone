using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    public UIItemPlane itemPlane;
    public Item item;
    public Text numText;
	// Start is called before the first frame update

	public void Start()
	{
	}
	public void Init()
	{
        this.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>(item.sprite);
		this.numText.text = item.count.ToString();
	}

	public void UpdateInfo()
	{
		if (item.count == 0)
		{
			itemPlane.UpdateInfo();
			return;
		}
		this.numText.text = item.count.ToString();
		
	}

	public void UpdateInfo(ItemType type,PlayerType playerType)
	{
		if (item.count == 0)
		{
			itemPlane.UpdateInfo();
			return;
		}
		this.numText.text = item.count.ToString();

	}

	public void OnDestroy()
	{
		item.OnUsed -= UpdateInfo;
	}
}
