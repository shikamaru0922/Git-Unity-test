using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShootObject1 : ScriptableObject
{
    public int spend = 1;

    public int recover = 0;


    public enum Objtype
    {
        bullet,
        magic,
        trigger,
        modified
    }
    public Objtype objtype;

    public virtual void  Pares<T>(out T value)
    {
        value = default(T);
    }

    public virtual int Packet(List<ShootObject> List, int startNum)
    {
        return 0;
    }
    public virtual float Parse_Rotate()
    {
        return 0;
    }
    public virtual GameObject Parse_BulletPrefab()
    {
        return null;
    }




    public virtual int SpendPoint(int point)
    {
        return point - spend + recover;
    }
}
