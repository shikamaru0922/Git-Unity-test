using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Getdata : Singleton<Getdata>
{
    public List<ShootObject> shootObjects;
    ShootMachine sm;
   public void Getmydata()
    {
        shootObjects.Clear();
        for(int i = 0; i < MyInventoryManager.Instance.myEquipmentData.myitems.Count; i++)
        {
            if (MyInventoryManager.Instance.myEquipmentData.myitems[i].myitemData != null)
            {
                shootObjects.Add(MyInventoryManager.Instance.myEquipmentData.myitems[i].myitemData.shootObject);
            }
        }
        if (sm == null)
        {
           sm= GameObject.Find("PlayerGrid").GetComponentInChildren<ShootMachine>();
        }
        sm.SetShootGroup(shootObjects);
        
    }
}
