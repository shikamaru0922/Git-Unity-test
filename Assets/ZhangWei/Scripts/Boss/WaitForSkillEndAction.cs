using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class WaitForSkillEndAction : Action
{
    public Animator animator;

    private int skill1Hash;
    private int skill2Hash;

    public override void OnStart()
    {
        skill1Hash = Animator.StringToHash("Skill1");
        skill2Hash = Animator.StringToHash("Skill2");
    }

    public override TaskStatus OnUpdate()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (info.shortNameHash == skill1Hash || info.shortNameHash == skill2Hash)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Running;
        }
    }
}