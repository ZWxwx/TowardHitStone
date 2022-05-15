using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Action onShot;

    //玩家
    public PlayerType playerType = PlayerType.Player1;

    //鼠标位置
    Vector3 pos;

    //前向量
    Vector3 forwardVector;

    //基础速度
    public float speed;

    //射击角向量
    public Vector3 shotVector;
    //飞船角度(世界)
    float angle;
    float Angle
    {
        get
        {
            return angle;
        }
		set
		{
            if (playerType == PlayerType.Player1)
                angle = value > 90 ? 90 : value < -90 ? -90 : value;
            else if (playerType == PlayerType.Player2)
                angle = value > 270 ? 270 : value < 90 ? 90 : value;
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y,angle);
        }
    }

    //飞船旋转速度(度每秒)
    public float rotateSpeed;
    //
    public GameObject bulletObjectPrefab;
    public GameObject gunTubeObject;
    public GameObject headPositionObject;
    public GameObject worldObject;



    void Start()
    {
        onShot += Shot;
    }
    
    // Update is called once per frame
    void Update()
    {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        forwardVector = pos - transform.transform.position;
        if(playerType==PlayerType.Player1)
            Angle += Input.GetAxis("Vertical_Player1") * Time.deltaTime * rotateSpeed;
        if (playerType == PlayerType.Player2)
            Angle -= Input.GetAxis("Vertical_Player2") * Time.deltaTime * rotateSpeed;
        shotVector = headPositionObject.transform.position - gunTubeObject.transform.position;
		if (Input.GetButtonDown("Fire_Player1")&&playerType==PlayerType.Player1|| Input.GetButtonDown("Fire_Player2") && playerType == PlayerType.Player2)
		{
            onShot();
		}

	}

    void Shot()
    {
        GameObject tmp=Instantiate(bulletObjectPrefab,gunTubeObject.transform.position, Quaternion.Euler(0,0,Angle), worldObject.transform);
        tmp.GetComponent<Rigidbody2D>().velocity = shotVector * speed;
        tmp.GetComponent<BulletController>().playerType = this.playerType;
    }
}

