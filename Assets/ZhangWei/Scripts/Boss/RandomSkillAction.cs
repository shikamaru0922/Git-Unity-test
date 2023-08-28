using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using ZhangWei;

public class RandomSkillAction : Action
{
    private Animator _animator;
    private BossAttack[] _attacks;

    public override void OnAwake()
    {
        _animator = GetComponent<Animator>();
        _attacks = transform.GetComponents<BossAttack>();
    }

    public override void OnStart()
    {
        int idx = Random.Range(0, _attacks.Length);
        BossAttack attack = _attacks[idx];
        if (attack.CanSkill)
        {
            _animator.SetTrigger(attack.skillID);
        }
        else
        {
            foreach (BossAttack bossAttack in _attacks)
            {
                if (bossAttack.CanSkill)
                {
                    _animator.SetTrigger(bossAttack.skillID);
                    break;
                }
            }
        }
    }
}