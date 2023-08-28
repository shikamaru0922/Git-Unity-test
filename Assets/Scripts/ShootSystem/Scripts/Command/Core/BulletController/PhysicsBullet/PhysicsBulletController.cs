using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBulletController : BulletEventController
{
    [Header("投射力量")]
    public float forceValue;
   
    
    [Header("存活时间")]
    public float lifetime;
    [Header("攻击力")]
    public int Atk;

    public override void Init()
    {
        base.Init();
        bulletData.rb.useGravity = true;
        bulletData.forceValue = forceValue;
        bulletData.a_speed = 0;
        bulletData.v_speed = 0;
        bulletData.reflectCount = 0;
        bulletData.lifeTime = lifetime;
        bulletData.Atk = Atk;
        bulletData.tgChildrenIndex = 0;
        bulletData.listIndex = 0;
        
    }
    protected override void OnEnable()
    {
        
        
        bulletData.rb.AddForce(forceValue * transform.forward);
    }
    protected virtual void Update()
    {
       LifeTimer();
        Event_Do(BulletEventType.moveCommand_Update);
    }
    protected virtual void  FixedUpdate()
    {
        Event_Do(BulletEventType.moveCommand_Physics);        
    }
    protected virtual void OnDisable()
    {
        Event_Do(BulletEventType.destroyCommand);
        //消除
    }
    protected virtual void Attack_Explode()
    {

    }


}
