using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Golem : EnemyController
{
    [Header("Skill")]

    public float kickForce = 25;
    public GameObject rockPrefab;
    public Transform handPos;

    public new void Attack()
    {
        transform.LookAt(attackTarget.transform);
        /*if (TargetInAttackRange())
        {
            //play near attack animation
            anim.SetTrigger("Attack");
        }
        if (TargetInSkillRange() || !TargetInAttackRange())//else fixed problems whitch grunt attack 02 cant play
        {
            //play far attack animation
            anim.SetTrigger("Skill");
        }*/
    }

    //Animation Event
    public void Kickoff()
    {
        if (attackTarget != null && transform.IsFacingTarget(attackTarget.transform))
        {
            var targetStats = attackTarget.GetComponent<CharactrStats>();

            Vector3 direction = (attackTarget.transform.position - transform.position).normalized;

            targetStats.GetComponent<NavMeshAgent>().isStopped = true;
            targetStats.GetComponent<NavMeshAgent>().velocity = direction * kickForce;
            targetStats.GetComponent<Animator>().SetTrigger("Dizzy");
            targetStats.TakeDamage(CharactrStats, targetStats);

        }
    }

    //Animation Event
    public void ThrowRock()
    {
        //TODO:try to let rock attach to the hand
        if(attackTarget != null)
        {
            var rock = Instantiate(rockPrefab, handPos.position, Quaternion.identity);
            rock.GetComponent<Rock>().target = attackTarget;
        }
    }

    public void Death()
    {
        //anim.SetTrigger("Death01");
        anim.SetBool("Death01",true);
        //UIManager.Instance.score += 1;
        GetComponent<NavMeshAgent>().radius= 0;
        Destroy(gameObject, 1f);
        //StartSiking();
    }
}
