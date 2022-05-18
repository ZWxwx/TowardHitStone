using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
	public Button button;

	Vector3 originSize;
	//�ƶ����Ϸ�ʱ�����ĳߴ����
	public float shortenRate;
	public float shortenOffset;
	private void Start()
	{
		originSize = this.transform.GetComponent<Image>().rectTransform.localScale;
		button = GetComponent<Button>();
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.transform.GetComponent<Image>().rectTransform.localScale = originSize * shortenRate;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		this.transform.GetComponent<Image>().rectTransform.localScale = originSize;
	}
}
