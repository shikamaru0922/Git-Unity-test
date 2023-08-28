using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    private int hp;
    public int Hp
    {
        get { return hp; }
        set
        {
            hp += value;
            Debug.Log(hp);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        hp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.gameObject.AddComponent<Modle_>();
        }
    }
}
