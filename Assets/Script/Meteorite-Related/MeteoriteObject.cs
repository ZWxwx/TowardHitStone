using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        //最好请不要在这里写任何东西
    }
    public void destoryMet()//在这里执行 正确的销毁指令
    {



        Destroy(gameObject);
    }
}
