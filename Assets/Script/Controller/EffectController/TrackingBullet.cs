using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingBullet : MonoBehaviour
{
    public BulletController bullet;
    public MeteoriteObject currentTarget;
    // Start is called before the first frame update
    void Start()
    {
        bullet = GetComponent<BulletController>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void ReTracking()
	{
        float minDistance=32565;
        float temp;
        GameObject trackingTarget;
        List<GameObject> lists = bullet.playerType == PlayerType.Player1 ? MeteoriteCreateSystem.Instance.MeteoriteListLeft : MeteoriteCreateSystem.Instance.MeteoriteListRight;

        foreach (var target in lists)
		{
            temp = (this.transform.position - target.transform.position).magnitude;
			if (temp < minDistance)
			{
                minDistance = temp;
                trackingTarget = target;
			}
		}
	}
}
