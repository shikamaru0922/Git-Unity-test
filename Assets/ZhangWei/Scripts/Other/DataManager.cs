using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

namespace ZhangWei
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance;

        private string enemyDataPath = "Data/EnemyData";
        private Dictionary<string, JsonData> enemyJsonData;
        private List<string> allEnemyID;

        private void Awake()
        {
            Instance = this;

            enemyJsonData = new Dictionary<string, JsonData>();
            Parse();

            // �����е���ID��С->���˳��浽�б���
            allEnemyID = new List<string>();
            foreach (string id in enemyJsonData.Keys)
            {
                allEnemyID.Add(id);
            }

            allEnemyID.Sort((a, b) => int.Parse(a) - int.Parse(b));
        }

        /// <summary>
        /// ������������
        /// </summary>
        private void Parse()
        {
            TextAsset textAsset = Resources.Load<TextAsset>(enemyDataPath);
            if (textAsset == null)
                Debug.LogError("������Դʧ��");
            else
                enemyJsonData = JsonMapper.ToObject<Dictionary<string, JsonData>>(textAsset.text);
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="id">����Ψһid</param>
        /// <returns></returns>
        public JsonData GetEnemyJsonData(string id)
        {
            if (id == null)
            {
                Debug.Log("��ȡ��������ʧ��");
                return null;
            }

            if (enemyJsonData.TryGetValue(id, out JsonData jsonData))
            {
                return jsonData;
            }
            else
            {
                Debug.Log($"û��IDΪ{id}�ĵ�������");
                return null;
            }
        }

        /// <summary>
        /// ���һ������ĵ���ID
        /// </summary>
        /// <returns></returns>
        public string GetRandomEnemyID()
        {
            int idx = Random.Range(0, allEnemyID.Count);
            return allEnemyID[idx];
        }
    }
}