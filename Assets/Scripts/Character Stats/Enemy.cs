using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform player;
    private Animator animator;
    private Transform enemy;
    private float rate = 2;
    private float timer;
        // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        enemy = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(agent.isActiveAndEnabled)
        agent.SetDestination(player.position);
        
        CheckPlayer();
    }

    public void Death() 
    {
        animator.SetTrigger("Death");
        //UIManager.Instance.score += 1;
        agent.isStopped = true;
        StartSiking();
    }

    private void StartSiking() 
    {
        GetComponent<Rigidbody>().isKinematic = false ;
        GetComponent<Collider>().isTrigger = true;
        agent.enabled = false;
        Destroy(gameObject, 2f);
    }

    public void CheckPlayer() 
    {
        //enemy.LookAt(player.position + new Vector3(0, 0.5f, 0));
        Vector3 dir = player.position - transform.position;
        dir.y = 0;

        if (Vector3.SqrMagnitude(dir) < 4 && Vector3.Angle(dir, transform.forward) < 30)
        {
            timer += Time.deltaTime;
            if (timer > rate)
            {
                AttackPlayer();
                timer = 0;
            }

        }
        else 
        {
            timer = rate;
        }

    }

    public void AttackPlayer() 
    {
        Debug.Log("hurt");
        //player.GetComponent<PlayerHealth>().Damage(10);
    }
}
