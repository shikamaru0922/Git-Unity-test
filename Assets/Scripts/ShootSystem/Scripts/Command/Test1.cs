using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    bool t;
    bool atk;
    float gap;
    private void Awake()
    {
        atk = false;
        gap = Time.time+0.05f;
        gameObject.GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        
        
    }
    private void FixedUpdate()
    {
        if (Time.time > gap && !atk)
        {
            atk = true;
            gap = Time.time + 3f;
        }
        if (atk)
        {
            gameObject.GetComponent<Collider>().enabled=true;
            atk = false;
        }
        else
        {
            gameObject.GetComponent<Collider>().enabled = false;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.name);
        Debug.Log(Time.time);
    }

    
}
