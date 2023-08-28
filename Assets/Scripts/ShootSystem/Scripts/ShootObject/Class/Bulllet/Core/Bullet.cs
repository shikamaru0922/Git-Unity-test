using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="ShootBullet",menuName ="ShootObject/Object/Bullet")]
public class Bullet : ShootObject
{
    
    
    public float euler;
    public string b_name;
    public override int Packet(List<ShootObject> List, int startNum)
    {
        return 0;

    }
    
    
}
