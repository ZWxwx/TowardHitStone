using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteHoleBullet : MonoBehaviour
{
    [SerializeField]
    private GameObject explodeIns;
    public GameObject target;
    public float bulletSpeed;
    public PlayerType ForWhichPlayer;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        InvokeRepeating("track",0,1f);
    }
    void track()
    {
        if (target == null)
        {
            if (ForWhichPlayer == PlayerType.Player1)
            {
                rb.velocity = new Vector2(1, 0) * bulletSpeed;
            }
            else { rb.velocity = new Vector2(-1, 0) * bulletSpeed; }
        }
        else
        {
            rb.velocity = (target.transform.position - transform.position).normalized * bulletSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject == target)
        {
            Debug.LogWarning("�˴�Ӧִ������ұ�ը������");
            //��������Ϲд�� ��Ҫֱ�����ҵ�ȻҲûɶ���
            Instantiate(explodeIns).transform.position=collision.gameObject.transform.position;
            Instantiate(explodeIns).transform.position = gameObject.transform.position;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
}
