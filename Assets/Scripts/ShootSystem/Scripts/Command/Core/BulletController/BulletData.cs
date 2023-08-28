using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData : MonoBehaviour
{
    //通过数值控制子弹的基础行为
   
    //移动
    [Header("初始速度")]
    public float v_speed;
    [Header("初始力")]
    public float forceValue;

    public float a_speed;//加速度,初始化为零

    public int reflectCount;//反弹次数

    //子弹生命周期
    public float lifeTime;
    
    //攻击   
    [HideInInspector]
    public int Atk;
    [HideInInspector]
    public float AtkGap;

    
    //基本物理组件的获取
    [HideInInspector]
    public Rigidbody rb;
    public Collider collider;


    //长子弹末端确认；
    [Header("子弹末端")]
    public Transform bulletEndPosition;
    
    
    
    //触发型子弹相关
    [HideInInspector]
    public int tgChildrenIndex;
    [HideInInspector]
    public int listIndex;

    public float triggerTime;

    public string b_name;
   


}
