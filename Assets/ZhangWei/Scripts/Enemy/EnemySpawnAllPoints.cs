using UnityEngine;

namespace ZhangWei
{
    /// <summary>
    /// 所有点全部生成一个随机敌人
    /// </summary>
    public class EnemySpawnAllPoints : EnemySpawn
    {
        public override void SpawnEnemy()
        {
            foreach (Transform t in spawnPoints)
            {
                string id = DataManager.Instance.GetRandomEnemyID();
                SpawnOneEnemy(id, t.position, t.rotation);
                //SpawnOneEnemy(id, transform.TransformPoint(t.localPosition));
            }
        }
    }
}