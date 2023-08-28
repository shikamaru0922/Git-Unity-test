using System;
using System.Collections.Generic;
using UnityEngine;
public enum BulletEventType
{
    moveCommand_Physics,
    moveCommand_Update,
    destroyCommand,
    onCollionderCommand,
    onTriggerCommand,
    DataValueCommand
}
[RequireComponent(typeof(Rigidbody),typeof(BulletData))]

public class BulletEventController : MonoBehaviour
{
     protected Dictionary<BulletEventType, Action<BulletEventController,BulletData, Collision,Collider>> eventDic;

    protected float lifeTime_Now;
    
    #region �����ӵ�
    [Header("�Ƿ�Ϊ�������ӵ�")]
    public bool isTrigger;
       
    #endregion
    public BulletData bulletData;

    protected virtual void OnEnable()
    {
        
        
    }

    //��ʼ��
    public virtual void Init()
    {
        if (eventDic == null)
        {
            eventDic = new Dictionary<BulletEventType, Action<BulletEventController, BulletData, Collision, Collider>>();
        }else
        {
            eventDic.Clear();
        }

        bulletData = GetComponent<BulletData>();
        
            bulletData.rb = gameObject.GetComponent<Rigidbody>();
            bulletData.collider = gameObject.GetComponent<Collider>();
        
        lifeTime_Now = 0f;
        bulletData.a_speed = 0f;
        bulletData.listIndex = 0;
        bulletData.tgChildrenIndex = 0;
        bulletData.triggerTime = 0.4f;
    }
        
    //����ӵ������¼�
    public virtual void AddBulletEvent(BulletEventType bulletEventType, Action<BulletEventController, BulletData, Collision, Collider> action)
    {
        if (bulletEventType == BulletEventType.DataValueCommand)
        {
            action(this,bulletData,null,null);
        }
        if (eventDic.ContainsKey(bulletEventType))
        {
            eventDic[bulletEventType]+=action;
            return;
        }
        eventDic.Add(bulletEventType, action);
    }
    //ɾ���ӵ������¼�
    public virtual void RemoveBulletEvent(BulletEventType bulletEventType, Action<BulletEventController, BulletData, Collision, Collider> action)
    {
        if (eventDic.ContainsKey(bulletEventType))
        {
            eventDic[bulletEventType] -= action;
            return;
        }
        eventDic.Add(bulletEventType, action);
    }
    
    //ʹ���ӵ������¼�
    protected virtual void Event_Do(BulletEventType bulletEventType, Collision collision = null, Collider collider = null)
    {
        if (eventDic.ContainsKey(bulletEventType))
            eventDic[bulletEventType]?.Invoke(this, bulletData, collision,collider);
           // eventDic[bulletEventType](this, bulletData, collision, collider);
    }   
    
    //�����͵Ĵ�������
    protected virtual void TirggerShoot()
    {

    }

    protected virtual void  LifeTimer()
    {

        lifeTime_Now += Time.deltaTime;
        if (lifeTime_Now >= bulletData.lifeTime)
        {
            Event_Do(BulletEventType.destroyCommand);
           
            PoolManager.Instance.ReleasePoolGameObject(bulletData.b_name, gameObject);
            return;//���գ� 
        }
        TriggerBullt();



    }
    protected virtual void TriggerBullt()
    {
        if (bulletData.tgChildrenIndex <= 0)
            return;
        if (lifeTime_Now > bulletData.triggerTime)
        {
            ShootMachine.Instance.ShootGroups(bulletData.listIndex, bulletData.tgChildrenIndex, this.transform);
            bulletData.tgChildrenIndex = 0;
        }
            
        

    }

}
