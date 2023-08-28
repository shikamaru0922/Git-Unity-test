using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZhangWei
{
    public class PrefabManager : MonoBehaviour
    {
        public static PrefabManager Instance;

        private string enemyPath = "Enemy";
        private Dictionary<string, GameObject> enemyPrefabDic;

        private void Awake()
        {
            Instance = this;

            enemyPrefabDic = new Dictionary<string, GameObject>();

            GameObject[] enemyPrefabs = Resources.LoadAll<GameObject>(enemyPath);
            foreach (GameObject obj in enemyPrefabs)
            {
                if (enemyPrefabDic.ContainsKey(obj.name))
                {
                    Debug.LogError($"加载敌人预制体失败，名字相同：{obj.name}");
                }
                else
                {
                    enemyPrefabDic.Add(obj.name, obj);
                }
                
                // .Net2.1及以上才有用
                // if (!enemyPrefabDic.TryAdd(obj.name, obj))
                //     Debug.LogError($"加载敌人预制体失败，名字相同：{obj.name}");
            }
        }

        /// <summary>
        /// 获取敌人预制体
        /// </summary>
        /// <param name="name">敌人名字</param>
        /// <returns></returns>
        public GameObject GetEnemyPrefab(string name)
        {
            if (name == null)
            {
                Debug.LogError("获得敌人预制体失败，名字为空");
                return null;
            }

            if (!enemyPrefabDic.TryGetValue(name, out GameObject obj))
            {
                Debug.LogError($"获取敌人预制体失败，没有名字为{name}的敌人");
                return null;
            }

            return obj;
        }
    }
}