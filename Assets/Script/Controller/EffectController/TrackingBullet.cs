using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingBullet : MonoBehaviour
{
    public BulletController bullet;
    public MeteoriteObject currentTarget;
    public float originAngle;//��ʼ��
    public Rigidbody2D rb;
    //public Vector3 eular;//��λ�Ƕ�(0~360)

    float a=0;

    void Start()
    {
        bullet = GetComponent<BulletController>();
        rb = GetComponent<Rigidbody2D>();
        //ReTracking();
        currentTarget = GameObject.Find("MeteoriteTest").GetComponent<MeteoriteObject>();
    }


    // Update is called once per frame
    void Update() {

        Vector3 x=transform.position;//�ӵ�λ��
        Vector3 y=currentTarget.transform.position;//Ŀ��λ��
        Vector3 z= transform.rotation.eulerAngles;//�ӵ�Ŀǰ����
        float angleB= Mathf.Atan2(x.y - y.y, x.x - y.x)*180/Mathf.PI+360;
        float angleA = z.z;
		if (a >= 10)
		{
            a = 0;
            rb.velocity = -new Vector2(Mathf.Sin(angleA), Mathf.Cos(angleA)) * bullet.speed; 
        }
        if ((Mathf.Abs(angleA - angleB) < 180 && angleA < angleB)|| (Mathf.Abs(angleA - angleB) > 180 && angleA > angleB))
		{
            transform.Rotate(new Vector3(0, 0, 10));
            Debug.Log("���");
        }
		else
		{
            transform.Rotate(new Vector3(0, 0, -10));
            Debug.Log("�ҹ�");

        }
        Debug.LogFormat("�ӵ�λ��:{0}   Ŀ��λ��:{1}    �ӵ�Ŀǰ��������:{2}    Ŀ�귽��:{3}  Ŀǰ����:{4}", x, y, z, angleB, angleA);
        a++;
    }

	void ReTracking()
	{
		float minDistance = 32565;
		float temp;
		List<GameObject> lists = bullet.playerType == PlayerType.Player1 ? MeteoriteCreateSystem.Instance.MeteoriteListLeft : MeteoriteCreateSystem.Instance.MeteoriteListRight;

		foreach (var target in lists)
		{
			temp = (this.transform.position - target.transform.position).magnitude;
			if (temp < minDistance)
			{
				minDistance = temp;
				currentTarget = target.GetComponent<MeteoriteObject>();
			}
		}
	}
}
