using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class RestartMoveBehaviour : Action
{
    public BehaviorTree tree;

    public override void OnStart()
    {
        //Debug.Log(tree.BehaviorName);
        tree.EnableBehavior();
        //tree.enabled = true;
    }
}