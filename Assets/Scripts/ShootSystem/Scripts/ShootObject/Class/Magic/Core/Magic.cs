using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Magic", menuName = "ShootObject/Modifile/Magic")]
public class Magic : ShootObject
{
    public float rotate;

    public override int Packet(List<ShootObject> List,int startNum)
    {
        base.Packet(List,startNum);
        int i;
        int n = 1;
        for ( i=startNum+1;i<startNum+1+recover;)
        {
            n+=List[i].Packet(List,i);
            i++;
        }
        
        return n+recover;
    }
    public override float Parse_Rotate()
    {
        return rotate;
    }
    
}
   

