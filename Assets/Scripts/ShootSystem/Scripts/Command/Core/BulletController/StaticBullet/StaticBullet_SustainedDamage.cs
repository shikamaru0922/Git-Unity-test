using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StaticBullet_SustainedDamage : StaticBulletController
{
    protected float nextAtkTime;
    protected bool isAtk; 

    
    public override void Init()
    {
        
        base.Init();
        nextAtkTime = Time.time + 0.05f;
        isAtk = false;
        bulletData.collider.enabled = false;
    }
   
    protected virtual void FixedUpdate()
    {
       
        
        Event_Do(BulletEventType.moveCommand_Physics);
        //eventDic[BulletEventType.moveCommand_Physics](this, bulletData,null,null);

        IsAllowToAttack();
    }
    protected virtual void Update()
    {
        LifeTimer();//ËÀÍö¼ÆËã
        Event_Do(BulletEventType.moveCommand_Update);
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        //Ôì³ÉÉËº¦
        if (other.gameObject.tag == "Enemy1" || other.gameObject.tag == "Boss" || other.gameObject.tag == "EnemyDamageBox" || other.gameObject.tag == "ZhangWeiBoss")
        {

            GameManager.Instance.EnemyTakeDamage(other.gameObject, this.bulletData.Atk);
        }
        Event_Do(BulletEventType.onTriggerCommand);
    }
    protected virtual void IsAllowToAttack()
    {
        if (Time.time > nextAtkTime && !isAtk)
        {
            isAtk = true;
            nextAtkTime = Time.time + bulletData.AtkGap;
        }
        if (isAtk)
        {
            bulletData.collider.enabled = true;
            isAtk = false;
        }
        else
        {
            bulletData.collider.enabled = false;
        }
    }


}
