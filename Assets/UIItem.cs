using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    public Item item;
    // Start is called before the first frame update

    public void Init()
	{
        this.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>(item.sprite);
        
    }

	public void Update()
	{
        //if(this.GetComponent<Image>().overrideSprite!=null)
        //    Debug.Log(this.GetComponent<Image>().overrideSprite.);

    }
}
