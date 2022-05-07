using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject gunTube;

    //���
    public PlayerType playerType = PlayerType.Player1;

    //���λ��
    Vector3 pos;

    //
    Vector3 forwardVector;

    //�ɴ��Ƕ�
    float angle;
    float Angle
    {
        get
        {
            return angle;
        }
        set
        {
            angle = value>90?90:value<-90?-90:value;
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y,(playerType==PlayerType.Player1?1: playerType == PlayerType.Player2?-1:0)* angle);
        }
    }

    //�ɴ���ת�ٶ�(��ÿ��)
    public float rotateSpeed;


    void Start()
    {
		
    }
    
    // Update is called once per frame
    void Update()
    {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        forwardVector = pos - transform.transform.position;
        if(playerType==PlayerType.Player1)
            Angle += Input.GetAxis("Vertical_Player1") * Time.deltaTime * rotateSpeed;
        if(playerType== PlayerType.Player2)
            Angle += Input.GetAxis("Vertical_Player2") * Time.deltaTime * rotateSpeed;

    }
}
