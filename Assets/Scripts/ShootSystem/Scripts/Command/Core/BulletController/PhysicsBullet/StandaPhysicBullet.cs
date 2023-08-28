using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandaPhysicBullet : PhysicsBulletController
{
    private void OnCollisionEnter(Collision collision)
    {
        Event_Do(BulletEventType.onCollionderCommand);
        if (collision.gameObject.tag == "Enemy1" || collision.gameObject.tag == "Boss" || collision.gameObject.tag == "EnemyDamageBox" || collision.gameObject.tag == "ZhangWeiBoss")
        {
            
            GameManager.Instance.EnemyTakeDamage(collision.gameObject, this.bulletData.Atk);
        }

    }
    protected override void Attack_Explode()
    {
        
    }
}
