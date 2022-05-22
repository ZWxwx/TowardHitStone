using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWhiteHole : MonoBehaviour
{
    public float bulletSpeed;
    public PlayerType forWhichPlayer;
    [Header("子对象的mask 手动绑定")]
    public GameObject mask;
    public GameObject whiteHoleBullet;
    
    private void Start()
    {
        if (forWhichPlayer == PlayerType.Player1)
        {
            transform.position = new Vector3(-4, Random.Range(-3f, 2f), 0);
        }
        else
        {
            transform.position = new Vector3(4, Random.Range(-3f, 2f), 0);
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, 0);
        }
    }
    void maskOn()
    {
        mask.SetActive(true);
    }
    void maskOff()
    {
        mask.SetActive(false);
    }
    void AnimEnd()
    {
        Destroy(gameObject);
    }
    void CreateWhiteHoleBullet()
    {
        GameObject target;
        GameObject bullet = Instantiate(whiteHoleBullet,transform.parent);
        bullet.transform.position = gameObject.transform.position;
        if (forWhichPlayer == PlayerType.Player1)
        {
            target = GameObject.Find("Player2");
        }
        else if((forWhichPlayer == PlayerType.Player2))
        {
            target = GameObject.Find("Player1");
        }
        else { return; }
        //bullet.GetComponent<Rigidbody2D>().velocity = (target.position - bullet.transform.position)*bulletSpeed;
        WhiteHoleBullet bulletCs = bullet.GetComponent<WhiteHoleBullet>();
        bulletCs.target = target;
        bulletCs.bulletSpeed = bulletSpeed;
        bulletCs.ForWhichPlayer = forWhichPlayer;
    }

}