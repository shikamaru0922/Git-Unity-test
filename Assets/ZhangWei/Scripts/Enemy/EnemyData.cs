using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZhangWei
{
    public class EnemyData
    {
        private string id; // Ψһid
        private string name; // ��������
        private int dmg; // �˺�
        private float atkDis; // ��������
        private float atkCD; // ����CD
        private int maxHP;
        private int hp; // ��ǰѪ��
        private float moveSpeed; // �ƶ��ٶ�
        private float stopDis; // NavMeshAgent��ʼֹͣ�ľ���
        private int addPlayerHP; // ��ɱ�����һظ���Ѫ��
        private int addPlayerEnergy; // ��ɱ��������ӵ�ŭ��ֵ

        public string ID => id;
        public string Name => name;
        public int Dmg => dmg;
        public float AtkDis => atkDis;
        public float AtkCD => atkCD;
        public int MaxHP => maxHP;

        public int HP
        {
            get => hp;
            set => hp = value;
        }

        public float MoveSpeed => moveSpeed;
        public float StopDis => stopDis;
        public int AddPlayerHP => addPlayerHP;
        public int AddPlayerEnergy => addPlayerEnergy;

        public EnemyData(string id, string name, int dmg, float atkDis, float atkCD, int maxHP, float moveSpeed,
            float stopDis, int addPlayerHP, int addPlayerEnergy)
        {
            this.id = id;
            this.name = name;
            this.dmg = dmg;
            this.atkDis = atkDis;
            this.atkCD = atkCD;
            this.maxHP = maxHP;
            this.HP = maxHP;
            this.moveSpeed = moveSpeed;
            this.stopDis = stopDis;
            this.addPlayerHP = addPlayerHP;
            this.addPlayerEnergy = addPlayerEnergy;
        }
    }
}