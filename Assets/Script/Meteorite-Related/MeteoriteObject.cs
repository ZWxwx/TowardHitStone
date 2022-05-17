using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteObject : MonoBehaviour
{
    public playerEnum ForWhichPlayer;//��¼�����ĸ���ҵ�
    

    // Start is called before the first frame update
    void Start()
    {
        if (ForWhichPlayer == playerEnum.pL)
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
        //����벻Ҫ������д�κζ���
    }
    public void destoryMet()//������ִ�� ��ȷ������ָ��
    {
        RemoveFromList();
        Destroy(gameObject);
    }
    void RemoveFromList()
    {
        if (ForWhichPlayer == playerEnum.pL)
        {
            MeteoriteCreateSystem.Instance.MeteoriteList[MeteoriteCreateSystem.playerLeft].Remove(gameObject);
        }
        else
        {
            MeteoriteCreateSystem.Instance.MeteoriteList[MeteoriteCreateSystem.playerRight].Remove(gameObject);
        }
    }
}
