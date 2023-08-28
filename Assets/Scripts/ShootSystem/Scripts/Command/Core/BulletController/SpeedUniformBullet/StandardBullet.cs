using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : SpeedUniformBtController
{
    
   
    private void Update()
    {
        LifeTimer();
        Event_Do(BulletEventType.moveCommand_Update);
    }
    private void FixedUpdate()
    {
        Event_Do(BulletEventType.moveCommand_Physics);
        bulletData.rb.velocity = transform.forward * bulletData.v_speed;
    }
    private void OnDisable()
    {
        Event_Do(BulletEventType.destroyCommand);
    }
    private void OnCollisionEnter(Collision collision)
    {

        

        Event_Do(BulletEventType.onCollionderCommand);
        if (collision.gameObject.tag == "Enemy1" || collision.gameObject.tag == "Boss" || collision.gameObject.tag == "EnemyDamageBox" || collision.gameObject.tag == "ZhangWeiBoss")
        {

            GameManager.Instance.EnemyTakeDamage(collision.gameObject, this.bulletData.Atk);
        }
        if (bulletData.reflectCount > 0)
        {
            bulletData.reflectCount--;
            Debug.Log("re");
            Reflect_Buttle(collision);
            return;
        }
        PoolManager.Instance.ReleasePoolGameObject(bulletData.b_name, gameObject);
    }
    void Reflect_Buttle(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        //Reflect
        Vector3 reflect_bullet = Vector3.Reflect(transform.position, contact.normal);
        reflect_bullet = new Vector3(reflect_bullet.x, 0, reflect_bullet.z);
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, reflect_bullet);
    }
}
