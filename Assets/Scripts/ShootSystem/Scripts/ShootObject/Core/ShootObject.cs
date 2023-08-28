using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ShootObject : ScriptableObject
{
    public int spend=1;

    public int recover=0;

    public float fireGap;

    public float energy;

    public Vector3 offset;
    public  enum Objtype
    {
        bullet,
        magic,
        trigger,
        modified
    }
    public Objtype objtype;
    

   

    public virtual int Packet(List<ShootObject> List,int startNum)
    {
        return 0;
    }
    public virtual float Parse_Rotate()
    {
        return 0;
    }


    public virtual int SpendPoint(int point)
    {
        return point - spend + recover;
    }
}

