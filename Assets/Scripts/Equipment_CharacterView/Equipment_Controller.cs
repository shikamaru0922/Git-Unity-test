using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment_Controller : MonoBehaviour
{
    Equipment_View Eview;
    public MyBagUI myEquipmentUI;
    // Start is called before the first frame update
    void Start()
    {
        Eview = GetComponent<Equipment_View>();
        Addgezi();
        Addgezi();
        Addgezi();
        Addgezi();
        Addgezi();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Addgezi();
        }
    }
    void Addgezi()//增加格子的方法
    {
        MyBagUI myEQUI = this.GetComponentInChildren<MyBagUI>();
        GameObject prefab = Resources.Load<GameObject>("Item/EQ_gezi");
        GameObject clone = Instantiate(prefab);
        clone.transform.SetParent(Eview.EquipmentParent);
        clone.transform.localPosition = Vector3.zero;
        clone.transform.localScale = Vector3.one; 
        GeziUI geziUI = clone.GetComponent<GeziUI>();
        myEQUI.MyBaglist.Add(geziUI);
        myEquipmentUI.MyRefreshUI();//如果不加myindex就是-1，超出范围
    }
}
