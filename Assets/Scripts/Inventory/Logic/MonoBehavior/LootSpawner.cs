using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [System.Serializable]
    public class LootItem
    {
        public GameObject item;
        [Range(0, 1)]
        public float weight;
    }

    public LootItem[] lootItem;

    public void Spawnloot()
    {
        float currentValue = Random.value;
        for (int i = 0; i < lootItem.Length; i++)
        {
            if(currentValue <= lootItem[i].weight)
            {
                GameObject obj = Instantiate(lootItem[i].item);
                obj.transform.position = transform.position + Vector3.up * 2;
                //break;//drop one time then STOP
            }
        }
    }
}
