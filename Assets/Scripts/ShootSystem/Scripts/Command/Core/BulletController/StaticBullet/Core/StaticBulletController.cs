using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBulletController : BulletEventController
{
    [Header("������")]
    public int Atk;
    [Header("�������")]
    public float AtkGap;
    [Header("���ʱ��")]
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
