using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class PlayerInRangeAction : Action
{
    public SharedFloat magnitude;
    private GameObject player;

    public override void OnStart()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override TaskStatus OnUpdate()
    {
        float sqrDis = (player.transform.position - transform.position).sqrMagnitude;
        return sqrDis <= Mathf.Pow(magnitude.Value, 2) ? TaskStatus.Success : TaskStatus.Failure;
    }
}