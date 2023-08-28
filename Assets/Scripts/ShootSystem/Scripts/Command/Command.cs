using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Command 
{
    void Init();
    void Execute(BulletEventController eventController,BulletData bulletData,Collision collision=null,Collider collider=null);

    
    void Exit();
}
