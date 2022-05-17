using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
	public Rect rotateRange;
	public Vector3 currentAngle;

	public Vector3 originRotation;

	public Vector2 rotateVector;
	[Header("角度，脚本中会转成弧度")]
	public float rotateAngle;


	public float rotateSpeed;

	public Vector2 cameraCenter;
	Vector2 mousePositionToCenter;
	[Header("鼠标位置向量转旋转向量的修正值")]
	public Vector2 rotateFixedVector;

	public void Start()
	{
		originRotation = Quaternion.ToEulerAngles(transform.rotation);
	}

	public void Update()
	{
		/*
		if(currentAngle+Mathf.Sin(rotateAngle * Mathf.PI / 180)*rotateSpeed>)
		rotateVector = new Vector2(Mathf.Sin(rotateAngle*Mathf.PI/180), Mathf.Cos(rotateAngle * Mathf.PI / 180));
		currentAngle = Quaternion.ToEulerAngles(transform.rotation);
		
		//transform.Rotate(rotateVector.x,0,rotateVector.y);
		transform.Rotate(rotateVector*rotateSpeed);
		*/
		cameraCenter = new Vector2(Camera.main.scaledPixelWidth / 2, Camera.main.scaledPixelHeight / 2);
		mousePositionToCenter = (Vector2)Input.mousePosition-cameraCenter;
		this.transform.rotation =Quaternion.Euler(new Vector2(originRotation.y + mousePositionToCenter.y * rotateFixedVector.y, originRotation.x + mousePositionToCenter.x * rotateFixedVector.x));

	}
}
