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

    public void Start()
    {
		bullet = GetComponent<BulletController>();
        rb = GetComponent<Rigidbody2D>();
        spinSpeed = 200f;
        InvokeRepeating("TrackMove", 0.1f, 0.01f);
        targetDir = rb.velocity;
    }


    // Update is called once per frame
    void Update() {

		//      Vector3 x=transform.position;//�ӵ�λ��
		//      Vector3 y=currentTarget.transform.position;//Ŀ��λ��
		//      Vector3 z= transform.rotation.eulerAngles;//�ӵ�Ŀǰ����
		//      float angleB= Mathf.Atan2(x.y - y.y, x.x - y.x)*180/Mathf.PI+360;
		//      float angleA = z.z;
		//if (a >= 10)
		//{
		//          a = 0;
		//          rb.velocity = -new Vector2(Mathf.Sin(angleA), Mathf.Cos(angleA)) * bullet.speed; 
		//      }
		//      if ((Mathf.Abs(angleA - angleB) < 180 && angleA < angleB)|| (Mathf.Abs(angleA - angleB) > 180 && angleA > angleB))
		//{
		//          transform.Rotate(new Vector3(0, 0, 10));
		//          Debug.Log("���");
		//      }
		//else
		//{
		//          transform.Rotate(new Vector3(0, 0, -10));
		//          Debug.Log("�ҹ�");

		//      }
		//      Debug.LogFormat("�ӵ�λ��:{0}   Ŀ��λ��:{1}    �ӵ�Ŀǰ��������:{2}    Ŀ�귽��:{3}  Ŀǰ����:{4}", x, y, z, angleB, angleA);
		//      a++;
		//TrackMove();
		if (currentTarget == null)
		{
			if (!ReTracking())
			{
				Destroy(this);
			}
		}

       transform.eulerAngles+=LookAt2D(transform,transform.position+targetDir.normalized);
    }
    public Vector3 LookAt2D(Transform from, Vector3 to)
    {
        float dx = to.x - from.transform.position.x;
        float dy = to.y - from.transform.position.y;
        float rotationZ = Mathf.Atan2(dy, dx) * 180 / Mathf.PI;
       

        float originRotationZ = from.eulerAngles.z;
        float addRotationZ = rotationZ - originRotationZ;
        if (addRotationZ > 180)
            addRotationZ -= 360;
        
        return new Vector3(0, 0, addRotationZ);
    } //������

	Vector3 targetDir;
	public float spinSpeed;
	void TrackMove()
	{
		if (currentTarget != null)
		{
            Vector3 speedDir = rb.velocity.normalized;
            Vector3 DisVec = currentTarget.transform.position - transform.position;
            speedDir = new Vector3(speedDir.x * spinSpeed, speedDir.y * spinSpeed, 0);
            //DisVec = new Vector3(DisVec.x * test2, DisVec.y * test2, 0);
            targetDir = (speedDir + DisVec).normalized;
            rb.velocity = targetDir * bullet.speed;
            // Debug.Log(spinSpeed);
            if (spinSpeed > 15f)
            {
                spinSpeed -= 2f;
            }
        }
	}
	public bool ReTracking()
	{
		float minDistance = 32565;
		float temp;
		List<GameObject> lists = bullet.playerType == PlayerType.Player1 ? MeteoriteCreateSystem.Instance.MeteoriteListLeft : MeteoriteCreateSystem.Instance.MeteoriteListRight;
        if (lists == null)
            return false;

		foreach (var target in lists)
		{
			temp = (this.transform.position - target.transform.position).magnitude;
			if (temp < minDistance)
			{
				minDistance = temp;
				currentTarget = target.GetComponent<MeteoriteObject>();
			}
		}
        return true;
	}
}
