using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "SpeedUp", menuName = "Command/ValueChangeCommand/SpeedUp")]
public class SpeedUp : CommandObj
{
    public override void Execute(BulletEventController eventController, BulletData bulletData, Collision collision = null, Collider collider = null)
    {
        bulletData.v_speed += Time.deltaTime;
    }
}
