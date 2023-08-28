using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using LitJson;
using System;

namespace ZhangWei
{
    public class EnemyFactory
    {
        private static EnemyFactory instance;

        public static EnemyFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EnemyFactory();
                }

                return instance;
            }
        }

        private EnemyFactory()
        {
        }

        public Enemy CreateEnemy(string id, Vector3 position, Quaternion rotation)
        {
            EnemyData enemyData;

            // �õ�����
            JsonData jsonData = DataManager.Instance.GetEnemyJsonData(id);
            if (jsonData == null)
            {
                Debug.LogError("��������ʧ��");
                return null;
            }

            enemyData = new EnemyData(jsonData["id"].ToString(), jsonData["name"].ToString(),
                int.Parse(jsonData["dmg"].ToString()), float.Parse(jsonData["atkDis"].ToString()),
                float.Parse(jsonData["atkCD"].ToString()), int.Parse(jsonData["maxHP"].ToString()),
                float.Parse(jsonData["moveSpeed"].ToString()), float.Parse(jsonData["stopDis"].ToString()),
                int.Parse(jsonData["addPlayerHP"].ToString()), int.Parse(jsonData["addPlayerEnergy"].ToString()));


            if (!ObjPools.Instance.HasPool(enemyData.Name))
            {
                // �õ�Ԥ����
                GameObject prefab = PrefabManager.Instance.GetEnemyPrefab(enemyData.Name);
                if (prefab != null)
                {
                    ObjPools.Instance.RegisterPool(enemyData.Name, prefab); // ע������
                }
            }

            // ��ϳ�һ�����˵�GameObject
            Enemy enemy = null;
            ObjPools.Instance.GetObj(enemyData.Name, (obj) =>
            {
                enemy = obj.GetComponent<Enemy>();
                if (enemy == null)
                {
                    // ���÷����������ӽű�
                    Type enemyType = Type.GetType("ZhangWei." + jsonData["enemyType"].ToString());
                    if (!enemyType.IsSubclassOf(Type.GetType("ZhangWei.Enemy")))
                        Debug.LogError($"���������{enemyType.FullName}�ű�ʧ�ܣ�����������Enemy��");
                    enemy = obj.AddComponent(enemyType) as Enemy;
                    enemy.tag = Tags.Enemy;

                    SphereCollider atkDisCollider = obj.GetComponent<SphereCollider>();
                    if (atkDisCollider == null)
                    {
                        Debug.LogError("����ģ��δ���SphereCollider");
                        atkDisCollider = obj.AddComponent<SphereCollider>();
                    }

                    NavMeshAgent agent = obj.GetComponent<NavMeshAgent>();
                    if (agent == null)
                    {
                        Debug.LogError("����ģ��δ���NavMeshAgent");
                        agent = obj.AddComponent<NavMeshAgent>();
                    }

                    NavMeshObstacle meshObstacle = obj.GetComponent<NavMeshObstacle>();
                    if (meshObstacle == null)
                    {
                        Debug.LogError("����ģ��δ���NavMeshObstacle");
                        meshObstacle = obj.AddComponent<NavMeshObstacle>();
                    }

                    HitCube hitCube = obj.GetComponentInChildren<HitCube>();
                    if (hitCube == null)
                        Debug.LogError("����ģ��δ���HitCube");

                    Animator animator = obj.GetComponent<Animator>();
                    if (animator == null)
                        Debug.LogError("����ģ��δ���Animator");

                    enemy.Init(enemyData, atkDisCollider, agent, meshObstacle, hitCube, animator);
                }
                else
                {
                    enemy.ResetData(enemyData);
                }

                enemy.transform.position = position;
                enemy.transform.rotation = rotation;
            });

            return enemy;
        }
    }
}