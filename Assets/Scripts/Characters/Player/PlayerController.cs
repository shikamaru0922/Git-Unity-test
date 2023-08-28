using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    //private NavMeshAgent agent;
    private Animator anim;

    private CharactrStats CharactrStats;

    private GameObject attackTarget;
    private float lastAttackTime;
    private bool isDead;

    private float stopDiastance;

    void Awake()
    {
        //agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        CharactrStats = GetComponent<CharactrStats>();
        //stopDiastance = agent.stoppingDistance;
    }

    private void OnEnable()
    {
        //MouseManager.Instance.OnMouseClicked += MoveToTarget;
       // MouseManager.Instance.OnEnemyClicked += EventAttack;
       // GameManager.Instance.RigisterPlayer(CharactrStats);
    }

    void Start()
    {
        
        // CharactrStats.MaxHealth = 2; lession 16  11:30s
        //SaveManager.Instance.LoadPlayerData();
        
    }

    private void OnDisable()
    {
       // MouseManager.Instance.OnMouseClicked -= MoveToTarget;
       // MouseManager.Instance.OnEnemyClicked -= EventAttack;
    }


    private void Update()
    {
        // unsucess to realize death animation   fixed in lession 21
        isDead = CharactrStats.CurrentHealth == 0;

        if (isDead)
            GameManager.Instance.NotifyObserver();

        SwitchAnimation();
        lastAttackTime -= Time.deltaTime;
    }

    private void SwitchAnimation()
    {
        //anim.SetFloat("Speed", agent.velocity.sqrMagnitude);
       // anim.SetBool("Death", isDead);
    }

    public void MoveToTarget(Vector3 target)
    {
        StopAllCoroutines();
        if (isDead) return;

        /*agent.stoppingDistance = stopDiastance;
        agent.isStopped = false;
        agent.destination = target;*/
    }

    /*private void EventAttack(GameObject target)
    {
        if (isDead) return;
        if (target != null)
        {
            attackTarget = target;
            CharactrStats.isCritical = UnityEngine.Random.value < CharactrStats.attackData.criticalChance;
            StartCoroutine(MovetoAttackTarget());
        }
    }

    IEnumerator MovetoAttackTarget()
    {
        agent.isStopped = false;
        agent.stoppingDistance = CharactrStats.attackData.attackRange;
        transform.LookAt(attackTarget.transform);
          
        while (Vector3.Distance(attackTarget.transform.position, transform.position) > CharactrStats.attackData.attackRange)
        {
            agent.destination = attackTarget.transform.position;
            yield return null;
        }
        agent.isStopped = true;

        //attack
        if (lastAttackTime < 0)
        {
            anim.SetBool("Critical", CharactrStats.isCritical);
            anim.SetTrigger("Attack");
            
            //attack cd
            lastAttackTime = CharactrStats.attackData.coolDown;
        }
    }*/
    //Animation Event
    void Hit()
    {
        if (attackTarget.CompareTag("Attackable"))
        {
            if (attackTarget.GetComponent<Rock>() && attackTarget.GetComponent<Rock>().rockStates == Rock.RockStates.HitNothing)
            {
                attackTarget.GetComponent<Rock>().rockStates = Rock.RockStates.HitEnemy;
                attackTarget.GetComponent<Rigidbody>().velocity = Vector3.one;
                attackTarget.GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.Impulse);

            }
        }
        else
        { 
            var targetStates = attackTarget.GetComponent<CharactrStats>();
            targetStates.TakeDamage(CharactrStats, targetStates);
        }
    }
}
