using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class WaitForNoGameObjectsWithTag : Action
{
    public SharedString tag;
    private GameObject[] arrowObjs;

    public override TaskStatus OnUpdate()
    {
        arrowObjs = GameObject.FindGameObjectsWithTag(tag.Value);
        if (arrowObjs != null && arrowObjs.Length == 0)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
}