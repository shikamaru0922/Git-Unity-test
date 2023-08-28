using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBullet :Bullet
{

    public override int Packet(List<ShootObject> List, int startNum)
    {
       
        return List[startNum+1].Packet(List,startNum+1)+1;
    }
}
