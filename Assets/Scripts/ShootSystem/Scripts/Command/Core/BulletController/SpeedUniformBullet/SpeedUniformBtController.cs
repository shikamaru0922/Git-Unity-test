using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUniformBtController : BulletEventController
{
    [Header("��ʼ�ٶ�")]
    public float v_speed;
    [Header("���ٶ�")]
    public float a_speed;
    [Header("��������")]
    public int reflectCount;
    [Header("���ʱ��")]
    public float lifetime;
    [Header("������")]
    public int Atk;

    public override void Init()
    {
        base.Init();
        bulletData.v_speed = v_speed;
        bulletData.a_speed = a_speed;
        bulletData.reflectCount = reflectCount;
        bulletData.lifeTime = lifetime;
        bulletData.Atk = Atk;
        bulletData.tgChildrenIndex = 0;
        bulletData.listIndex = 0;
        bulletData.forceValue = 0;
    }
    

}
