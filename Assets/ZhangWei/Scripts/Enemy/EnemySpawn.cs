using System.Collections;
using UnityEngine;

namespace ZhangWei
{
    public abstract class EnemySpawn : MonoBehaviour
    {
        protected Transform[] spawnPoints;
        public int spawnCount;
        public float spawnCD;

        private void Awake()
        {
            spawnPoints = new Transform[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                spawnPoints[i] = transform.GetChild(i);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Spawn());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                SpawnEnemy();
            }
        }

        /// <summary>
        /// ָ��λ������һ������
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pos"></param>
        protected void SpawnOneEnemy(string id, Vector3 pos, Quaternion rotation)
        {
            EnemyFactory.Instance.CreateEnemy(id, pos, rotation);
        }

        private IEnumerator Spawn()
        {
            while (spawnCount > 0)
            {
                SpawnEnemy();
                spawnCount--;
                yield return new WaitForSeconds(spawnCD);
            }
        }

        public abstract void SpawnEnemy();
    }
}