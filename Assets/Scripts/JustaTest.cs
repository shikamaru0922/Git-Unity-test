using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustaTest : MonoBehaviour
{
    public GameObject[] bosses;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) 
        {
            DamageUpPlayer(bosses,50);
        }
    }

    void DamageUpPlayer(GameObject[] other, int damage)
    {
        int i = Random.Range(0, other.Length);
        Debug.Log(i);
        GameManager.Instance.EnemyTakeDamage(other[i], damage);
    }
}
