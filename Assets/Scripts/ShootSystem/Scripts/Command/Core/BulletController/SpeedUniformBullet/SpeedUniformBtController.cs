using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUniformBtController : BulletEventController
{
    [Header("初始速度")]
    public float v_speed;
    [Header("加速度")]
    public float a_speed;
    [Header("反弹次数")]
    public int reflectCount;
    [Header("存活时间")]
    public float lifetime;
    [Header("攻击力")]
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
