using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ZhangWei
{
    /// <summary>
    /// �����Ŀ¼
    /// </summary>
    public class ObjPools
    {
        private static ObjPools instance;

        public static ObjPools Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ObjPools();
                }

                return instance;
            }
        }

        // ������ж���ض��е��ֵ�
        private Dictionary<string, Queue<GameObject>> pools;

        // ���ÿ�����������Ԥ����
        private Dictionary<string, GameObject> poolPrefabs;

        private ObjPools()
        {
            pools = new Dictionary<string, Queue<GameObject>>();
            poolPrefabs = new Dictionary<string, GameObject>();
        }

        /// <summary>
        /// ע������
        /// </summary>
        /// <param name="poolName">���������</param>
        /// <param name="prefab">Ԥ����</param>
        public void RegisterPool(string poolName, GameObject prefab)
        {
            if (pools.ContainsKey(poolName))
            {
                Debug.LogError($"ע������ʧ�ܣ��Ѵ��ڸö���أ�{poolName}");
                return;
            }

            if (prefab == null)
            {
                Debug.LogError("ע������ʧ�ܣ�Ԥ����Ϊ��");
                return;
            }

            pools.Add(poolName, new Queue<GameObject>());
            poolPrefabs.Add(poolName, prefab);

#if UNITY_EDITOR
            //Debug.Log($"ע�����سɹ���{poolName}");
#endif
        }

        /// <summary>
        /// ��ָ������ػ�ȡGameObject
        /// </summary>
        /// <param name="poolName">����ص�����</param>
        /// <returns>ָ��������δʹ�õ�GameObject</returns>
        public GameObject GetObj(string poolName, Action<GameObject> beforeActive = null)
        {
            if (!pools.ContainsKey(poolName))
            {
                Debug.LogError($"�Ӷ������ȡGameObjectʧ�ܣ�δ��ǰע�����أ�{poolName}");
                return null;
            }

            // �õ�ָ������
            Queue<GameObject> queue = pools[poolName];
            GameObject go = null;

            // ��������л���δʹ�õ�GmaeObject�򷵻�һ��
            if (queue.Count > 0)
            {
                go = queue.Dequeue();
            }
            // ���������û�п��õ�GmaeObject��ʵ����һ��
            else
            {
                GameObject prefab = poolPrefabs[poolName];
                go = GameObject.Instantiate(prefab);

                GameObject parentGO = GameObject.Find(poolName);
                if (parentGO == null)
                {
                    parentGO = new GameObject(poolName);
                    parentGO.transform.SetAsLastSibling();
                }

                go.transform.SetParent(parentGO.transform);
            }

            if (beforeActive != null)
            {
                beforeActive.Invoke(go);
            }

            go.SetActive(true);
            return go;
        }

        /// <summary>
        /// ��GameObject����ָ�������
        /// </summary>
        /// <param name="poolName">���������</param>
        /// <param name="gameObject">Ҫ�����GameObject</param>
        public void SetObj(string poolName, GameObject gameObject, Action<GameObject> doAfterDeactive = null)
        {
            if (!pools.ContainsKey(poolName))
            {
                Debug.LogError($"��GameObject��������ʧ�ܣ�δ��ǰע�����أ�{poolName}");
                return;
            }

            if (gameObject == null)
            {
                Debug.LogError($"��GameObject��������ʧ�ܣ�Ҫ����Ķ���Ϊ��");
                return;
            }

            // �õ�ָ������
            Queue<GameObject> queue = pools[poolName];

            // ����Ϸ�������غ�������
            gameObject.SetActive(false);

            if (doAfterDeactive != null)
            {
                doAfterDeactive.Invoke(gameObject);
            }

            gameObject.transform.position = Vector3.zero;
            queue.Enqueue(gameObject);
        }

        public bool HasPool(string name)
        {
            return pools.ContainsKey(name);
        }
    }

    public class ObjPoolName
    {
        // �����ӵ�
        public static string pistolBullet = "��ǹ�ӵ�";
        public static string enhancedPistolBullet = "ǿ����ǹ�ӵ�";
        public static string smgBullet = "���ǹ�ӵ�";

        // ���ֵ���
        public static string darkTreant = "DarkTreant";
        public static string demonWolf = "DemonWolf";
        public static string lichCyan = "LichCyan";
        public static string orcSlinger = "OrcSlinger";

        // ���ֻ�����Ч
        public static string LaserWandHit = "laserWandHit";
    }
}