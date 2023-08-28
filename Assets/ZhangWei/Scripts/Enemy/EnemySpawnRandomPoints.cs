using System.Collections.Generic;
using UnityEngine;

namespace ZhangWei
{
    /// <summary>
    /// 随机几个点生成随机敌人
    /// </summary>
    public class EnemySpawnRandomPoints : EnemySpawn
    {
        public override void SpawnEnemy()
        {
            int count = Random.Range(1, spawnPoints.Length + 1);

            List<int> tempList = new List<int>();
            int idx = -1;
            for (int i = 0; i < count; i++)
            {
                while (tempList.Contains(idx))
                {
                    idx = Random.Range(0, spawnPoints.Length);
                }

                string id = DataManager.Instance.GetRandomEnemyID();
                SpawnOneEnemy(id, spawnPoints[idx].position, spawnPoints[idx].rotation);
            }
        }
    }
}