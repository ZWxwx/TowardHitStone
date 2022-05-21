using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteObject : MonoBehaviour
{
    public float score;
    public PlayerType ForWhichPlayer;//记录属于哪个玩家的
    public MeteoriteHealthBarController healthBar;
    

    // Start is called before the first frame update
    void Start()
    {
        if (ForWhichPlayer == PlayerType.Player1)
        {
            MeteoriteCreateSystem.Instance.MeteoriteList[MeteoriteCreateSystem.playerLeft].Add(gameObject);
        }
        else 
        {
            MeteoriteCreateSystem.Instance.MeteoriteList[MeteoriteCreateSystem.playerRight].Add(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnBecameInvisible()
    {
        RemoveFromList();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        //最好请不要在这里写任何东西
    }
    public void destoryMet()//在这里执行 正确的销毁指令
    {
        RemoveFromList();
        Destroy(gameObject);
    }
    void RemoveFromList()
    {
        if (ForWhichPlayer == PlayerType.Player1)
        {
            MeteoriteCreateSystem.Instance.MeteoriteList[MeteoriteCreateSystem.playerLeft].Remove(gameObject);
        }
        else
        {
            MeteoriteCreateSystem.Instance.MeteoriteList[MeteoriteCreateSystem.playerRight].Remove(gameObject);
        }
    }
}
