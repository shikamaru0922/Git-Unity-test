using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Modfied", menuName = "ShootObject/Modifile/Modfied")]
public class Modfied : ShootObject
{
    public  CommandObj command;
   


    public virtual void AddModified(GameObject bullets)
    {

        if (command.eventType == BulletEventType.DataValueCommand)
        {
            if (bullets == null)
                Debug.Log("bullet is null");
            
            command.Execute(bullets.GetComponent<BulletEventController>(), bullets.GetComponent<BulletData>());
            return;
        }
           
            bullets.GetComponent<BulletEventController>().AddBulletEvent(command.eventType, command.Execute);
       
        
    }
   
}
