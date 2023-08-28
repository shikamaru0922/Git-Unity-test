using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZhangWei
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance => instance;

        // public KeyCode randomWeaponKey;
        // public KeyCode getWeaponKey;
        // public KeyCode reloadBulletKey;
        // public KeyCode BattleKey; //f���л�ս�����ս��
        // public KeyCode PreviousWeaponKey; //Q���л���һ������
        // public KeyCode NextWeaponKey; //E���л���һ������
        //
        // [Header("�ӵ�Ԥ����")] public GameObject pistolBullet; // ��ǹ�ӵ�
        // public GameObject enhancedPistolBullet; // ǿ����ǹ�ӵ�
        // public GameObject scmBullet; // ���ǹ�ӵ�
        //
        // [Header("����Ԥ����")] public GameObject darkTreant;
        // public GameObject demonWolf;
        // public GameObject lichCyan;
        // public GameObject orcSlinger;
        //
        // [Header("������ЧԤ����")] public GameObject laserWandHit;

        public float recoveryEnemyTime = 2f; // ������������������Ϻ���յ������ǰ�ȴ���ʱ��

        public LayerMask enemyMask;

        private void Awake()
        {
            if (instance == null) instance = this;

            RegisterObjPools();

            enemyMask = LayerMask.GetMask("Enemy");
        }

        /// <summary>
        /// ע��������Ϸ����Ҫ�õ��Ķ����
        /// </summary>
        private void RegisterObjPools()
        {
            // ObjPools.Instance.RegisterPool(ObjPoolName.pistolBullet, pistolBullet);
            // ObjPools.Instance.RegisterPool(ObjPoolName.enhancedPistolBullet, enhancedPistolBullet);
            // ObjPools.Instance.RegisterPool(ObjPoolName.smgBullet, scmBullet);

            // ObjPools.Instance.RegisterPool(ObjPoolName.darkTreant, darkTreant);
            // ObjPools.Instance.RegisterPool(ObjPoolName.demonWolf, demonWolf);
            // ObjPools.Instance.RegisterPool(ObjPoolName.lichCyan, lichCyan);
            // ObjPools.Instance.RegisterPool(ObjPoolName.orcSlinger, orcSlinger);

            // ObjPools.Instance.RegisterPool(ObjPoolName.LaserWandHit, laserWandHit);

#if UNITY_EDITOR
            Debug.Log("--------------------------------------------");
#endif
        }

        /// <summary>
        /// ����ܵ��˺�
        /// </summary>
        /// <param name="damage"></param>
        public void PlayerTakeDamage(GameObject obj, int damage)
        {
            // TODO: ����������˷���
            obj.GetComponent<CharactrStats>().GetHit(damage);
            UnityEngine.GameManager.Instance.isDamaging = true;
        }

        /// <summary>
        /// ����һ�Ѫ
        /// </summary>
        /// <param name="hp"></param>
        public void AddPlayerHP(int hp)
        {
            // TODO: ������Ҽ�Ѫ����
        }

        /// <summary>
        /// ����һ�ŭ��
        /// </summary>
        /// <param name="engery"></param>
        public void AddPlayerEngery(int engery)
        {
            // TODO: ������Ҽ�ŭ������
        }

        public void GameOver()
        {
        }
    }
}