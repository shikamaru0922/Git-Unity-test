using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjFc : ObjFactory
{
    
    public override GameObject CreatObj(string name)
    {
        if (prefab==null)
        {
            prefab = Resources.Load<GameObject>(path + name);
        }
        GameObject temp = GameObject.Instantiate(prefab);
       
        temp.GetComponent<BulletData>().b_name = name;
        return temp ;
    }
}
