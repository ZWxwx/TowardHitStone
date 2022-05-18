using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WornHole : MonoBehaviour //理论上可以用单例，但谁能保证以后不出现多个传送门呢
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void move(float xMove)
    {
        transform.position += new Vector3(xMove, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
