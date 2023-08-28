using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reflect", menuName = "Command/FixUpdateCommand/Reflect")]
public class Reflect : CommandObj
{
    public override void Execute(BulletEventController eventController, BulletData bulletData, Collision collision = null, Collider collider = null)
    {
        bulletData.reflectCount++;
    }
}
