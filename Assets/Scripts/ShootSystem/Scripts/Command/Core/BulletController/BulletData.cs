using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData : MonoBehaviour
{
    //ͨ����ֵ�����ӵ��Ļ�����Ϊ
   
    //�ƶ�
    [Header("��ʼ�ٶ�")]
    public float v_speed;
    [Header("��ʼ��")]
    public float forceValue;

    public float a_speed;//���ٶ�,��ʼ��Ϊ��

    public int reflectCount;//��������

    //�ӵ���������
    public float lifeTime;
    
    //����   
    [HideInInspector]
    public int Atk;
    [HideInInspector]
    public float AtkGap;

    
    //������������Ļ�ȡ
    [HideInInspector]
    public Rigidbody rb;
    public Collider collider;


    //���ӵ�ĩ��ȷ�ϣ�
    [Header("�ӵ�ĩ��")]
    public Transform bulletEndPosition;
    
    
    
    //�������ӵ����
    [HideInInspector]
    public int tgChildrenIndex;
    [HideInInspector]
    public int listIndex;

    public float triggerTime;

    public string b_name;
   


}
