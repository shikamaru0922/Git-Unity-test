using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class LookPlayerAction : Action
{
    private GameObject player;

    public override void OnStart()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override TaskStatus OnUpdate()
    {
        Vector3 pos = player.transform.position;
        pos.y = transform.position.y;
        Vector3 forward = pos - transform.position;
        transform.forward = forward;
        return TaskStatus.Success;
    }
}