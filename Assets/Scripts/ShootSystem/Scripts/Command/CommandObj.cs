using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public abstract class CommandObj :ScriptableObject
{
    public BulletEventType eventType;
    public abstract void Execute(BulletEventController eventController, BulletData bulletData, Collision collision = null, Collider collider = null);
    
    public virtual  void Exit()
    {        
    }

    public virtual void Init()
    {
    }
}
