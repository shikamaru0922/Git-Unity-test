using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxCount;
    public float countDown;

    private float timer;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        count = maxCount;
        timer = countDown;
        //Debug.Log(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= countDown && count > 0)
        {
            timer = 0;

            GameObject obj = GameObject.Find(enemyPrefab.name);
            if (obj == null)
            {
                obj = new GameObject(enemyPrefab.name);
            }

            GameObject cloneObj = Instantiate(enemyPrefab, transform.position, Quaternion.identity, transform);
            //cloneObj.transform.localPosition = Vector3.zero;

            count--;
        }
    }
}
