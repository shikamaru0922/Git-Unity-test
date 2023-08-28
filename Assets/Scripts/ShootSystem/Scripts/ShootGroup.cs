using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGroup 
{
    
    ShootObject trigger;
    List<ShootObject> modified_List;
    List<ShootObject> magic_List;
    List<ShootObject> bullets_List;
   
    public Dictionary<int, int> triggerChildrenDic;
    public int listIndex;

    public float energy;
    public float fireGap;
    public List<ShootObject> Modified_List { get => modified_List; set => modified_List = value; }
    public List<ShootObject> Magic_List { get => magic_List; set => magic_List = value; }
    public List<ShootObject> Bullets_List { get => bullets_List; set => bullets_List = value; }
    public ShootObject Trigger { get => trigger; set => trigger = value; }
    public ShootGroup()
    {
        modified_List = new List<ShootObject>();
        magic_List = new List<ShootObject>();
        bullets_List = new List<ShootObject>();
        triggerChildrenDic = new Dictionary<int, int>();
    }

    public ShootGroup(List<ShootObject> modified_List, List<ShootObject> magic_List, List<ShootObject> bullets_List, ShootObject trigger)
    {
        Modified_List = modified_List;
        Magic_List = magic_List;
        Bullets_List = bullets_List;
        Trigger = trigger;
    }
}
