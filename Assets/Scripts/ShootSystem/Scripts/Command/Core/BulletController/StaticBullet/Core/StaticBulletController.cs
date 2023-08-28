using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBulletController : BulletEventController
{
    [Header("攻击力")]
    public int Atk;
    [Header("攻击间隔")]
    public float AtkGap;
    [Header("存活时间")]
    public float liveTime;
    
    public override void Init()
    {
        base.Init();

        
        
        bulletData.collider.isTrigger = true;
        
        bulletData.Atk = this.Atk;
        bulletData.AtkGap = this.AtkGap;
        bulletData.lifeTime = this.liveTime;


        bulletData.v_speed = 0f;
        bulletData.forceValue = 0f;

        bulletData.tgChildrenIndex = 0;
        bulletData.listIndex = 0;                       
    }
   
    
}
