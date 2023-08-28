using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//public enum EnemyStates { GUARD, PATROL, CHASE, DEAD }
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharactrStats))]

public class BossController : MonoBehaviour, IEndGameObserver
{
    private EnemyStates enemyStates;
    private NavMeshAgent agent;
    public Animator anim;
    private Collider coll;
    protected CharactrStats CharactrStats;

    [Header("Basic Setting")]
    public float sightRadius;

    protected GameObject attackTarget;
    public bool isGuard;
    private float speed;
    public float lookAtTime;
    private float remainLookAtTime;
    private float lastAttackTime;

    private Quaternion guardRotaton;

    [Header("Patrol State")]
    public float patrolRange;
    private Vector3 wayPoint;
    private Vector3 guardPos;

    //animation boolean
    bool isWalk;
    bool isChase;
    bool isFollow;
    bool isDead;
    bool playerDead;


    Vector3 formalVelocity;
    float formalAngularSpeed;
    bool inAttackColider1;
    bool inAttackAnimation;


    int tauntingCount;

    /*  just a test
        [Header("Just a Test")]
        public GameObject bat;*/

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        CharactrStats = GetComponent<CharactrStats>();
        coll = GetComponent<Collider>();

        speed = agent.speed;
        guardPos = transform.position;
        guardRotaton = transform.rotation;
        remainLookAtTime = lookAtTime;
        TimeLinePlayControl.Instance.TimeLinePlayControlAction += Open;
    }
    private void Start()
    {
        if (isGuard)
        {
            enemyStates = EnemyStates.GUARD;
        }
        else
        {
            enemyStates = EnemyStates.PATROL;
            GetNewWayPoint();
        }
        //TODO:update after change scence
        //GameManager.Instance.AddObserver(this);

        formalVelocity = GetComponent<NavMeshAgent>().velocity;
        formalAngularSpeed = GetComponent<NavMeshAgent>().angularSpeed;


    }
    //TODO:start using after change scence
    //void OnEnable()
    //{
    //    GameManager.Instance.AddObserver(this);
    //}
    void OnDisable()
    {

        /*if (!GameManager.IsInitialized) return;
        GameManager.Instance.RemoveObserver(this);*/

        if (GetComponent<LootSpawner>() && isDead) 
        {
            GetComponent<LootSpawner>().Spawnloot();
            GameManager.Instance.BossCount++;
            agent.enabled = false;
        }
            

        /* if (QuestManager.IsInitialized && isDead)
             QuestManager.Instance.UpDateQuestProgress(this.name,1);*/
    }

    private void Update()
    {
        if (CharactrStats.CurrentHealth == 0)
            isDead = true;

        if (!playerDead)
        {
            SwitchAnimation();
            SwitchStates();
            
            lastAttackTime -= Time.deltaTime;
        }

       

        /* 只是一次测试 待删除  (已注释)  */
        /*if (Input.GetKeyDown(KeyCode.Space)) 
        {
            bat.GetComponent<CharactrStats>().characterData.currentHealth = 0;
        }*/

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            
            inAttackColider1 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {

            inAttackColider1 = false;
        }
    }

    void SwitchAnimation()
    {
        anim.SetBool("Walk", isWalk);
        anim.SetBool("Chase", isChase);
        anim.SetBool("Follow", isFollow);
        anim.SetBool("Critical", CharactrStats.isCritical);
        anim.SetBool("Death", isDead);
    }

    void SwitchStates()
    {
        if (isDead)
            enemyStates = EnemyStates.DEAD;

        //if finded player, switch to CHASE
        else if (FoundPlayer())
        {
            enemyStates = EnemyStates.CHASE;
            // test Debug.Log("Finded Player"); lession 14
        }
        switch (enemyStates)
        {
            case EnemyStates.GUARD:
                isChase = false;

                if (transform.position != guardPos)
                {
                    isWalk = true;
                    //agent.isStopped = false;
                    agent.destination = guardPos;
                    //TODO: Cant doing rotation
                    if (Vector3.SqrMagnitude(guardPos - transform.position) <= agent.stoppingDistance)
                    {
                        isWalk = false;
                        transform.rotation = Quaternion.Lerp(transform.rotation, guardRotaton, 0.01f);
                    }
                }
                break;

            case EnemyStates.PATROL:
                isChase = false;
                agent.speed = speed * 0.5f;
                //if arrived at target
                if (Vector3.Distance(wayPoint, transform.position) <= agent.stoppingDistance)
                {
                    isWalk = false;
                    if (remainLookAtTime > 0)
                        remainLookAtTime -= Time.deltaTime;
                    else
                        GetNewWayPoint();
                }
                else
                {
                    isWalk = true;
                    agent.destination = wayPoint;
                }
                break;
            case EnemyStates.CHASE:

                isWalk = false;
                isChase = true;

                if (!FoundPlayer())
                {   //
                    isFollow = false;
                    if (remainLookAtTime > 0)
                    {
                        agent.destination = transform.position;
                        remainLookAtTime -= Time.deltaTime;// remainLookAtTime = remainLookAtTime -Time.deltaTime;
                    }
                    else if (isGuard)
                        enemyStates = EnemyStates.GUARD;
                    else
                        enemyStates = EnemyStates.PATROL;

                }
                else
                {
                    isFollow = true;
                    //agent.isStopped = false;
                    agent.destination = attackTarget.transform.position;
                }
                //Attack when player in radius
                if (TargetInAttackRange() || TargetInSkillRange())
                {
                    isFollow = false;
                    //agent.isStopped = true;
                    
                    
                    if (inAttackColider1&& lastAttackTime < 0) 
                    {
                        //agent.isStopped = true;
                        lastAttackTime = CharactrStats.attackData.coolDown;
                            
                            //if critical
                            CharactrStats.isCritical = Random.value < CharactrStats.attackData.criticalChance;
                            //do attack
                            Attack();                       
                    }


                    if (!inAttackAnimation)
                    {
                        Vector3 vec = attackTarget.transform.position - transform.position;
                        Quaternion rotate = Quaternion.LookRotation(vec);

                        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotate, 0.01f);
                    }



                }

                break;
            case EnemyStates.DEAD:
                coll.enabled = false;
                //agent.enabled = false;
                agent.radius = 0;//to fixed problems when enemy died consle report false
                Destroy(gameObject, 2f);

                break;

        }
    }
    void Attack()
    {
        //transform.rotation = Quaternion.Lerp(transform.rotation, transform.position - attackTarget.transform.position, 0.01f));
        
        //transform.LookAt(attackTarget.transform);
        if (TargetInAttackRange())
        {
            //play near attack animation
            inAttackAnimation = true;
            agent.isStopped = true;
            anim.SetTrigger("Attack");
            
        }
        if (CharactrStats.CurrentHealth % 22 == 1) 
        {
            if (tauntingCount == 0) 
            {
                inAttackAnimation = true;
                agent.isStopped = true;
                anim.SetTrigger("Taunting");
                tauntingCount++;
            }
            return;
        }
        if (TargetInSkillRange() && !TargetInAttackRange())//else fixed problems whitch grunt attack 02 cant play
        {
            //play far attack animation
            inAttackAnimation = true;
            agent.isStopped = true;
            tauntingCount = 0;
            anim.SetTrigger("Skill");
            

        }


    }
    bool FoundPlayer()
    {
        var colliders = Physics.OverlapSphere(transform.position, sightRadius);
        
        foreach (var target in colliders)
        {
            if (target.CompareTag("Player"))
            {
                attackTarget = target.gameObject;
                return true;
            }
        }
        attackTarget = null;
        return false;
    }

    public bool TargetInAttackRange()
    {
        if (attackTarget != null)
            return Vector3.Distance(attackTarget.transform.position, transform.position) <= CharactrStats.attackData.attackRange;
        else
            return false;
    }
    public bool TargetInSkillRange()
    {
        if (attackTarget != null)
            return Vector3.Distance(attackTarget.transform.position, transform.position) <= CharactrStats.attackData.skillRange;
        else
            return false;
    }

    void GetNewWayPoint()
    {
        remainLookAtTime = lookAtTime;

        float randomX = Random.Range(-patrolRange, patrolRange);
        float randomZ = Random.Range(-patrolRange, patrolRange);
        Vector3 randomPoint = new Vector3(guardPos.x + randomX, transform.position.y, guardPos.z + randomZ);
        //problems  fixed
        //wayPoint = randomPoint; lession 15
        NavMeshHit hit;
        wayPoint = NavMesh.SamplePosition(randomPoint, out hit, patrolRange, 1) ? hit.position : transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }

    //Animation Event
    void Hit()
    {
        if (attackTarget != null && transform.IsFacingTarget(attackTarget.transform))
        {
            var targetStats = attackTarget.GetComponent<CharactrStats>();
            targetStats.TakeDamage(CharactrStats, targetStats);
        }
    }

    public void EndNotify()
    {
        //Victory anim
        //Stop moving
        //stop agent
        anim.SetBool("Win", true);
        playerDead = true;
        //Debug.Log("notify");
        isChase = false;
        isWalk = false;
        attackTarget = null;

    }




    public void StopAgent() 
    {

        GetComponent<NavMeshAgent>().isStopped = true;
       
    }

    public void StratAgent() 
    {
        GetComponent<NavMeshAgent>().isStopped = false;
    }


    public void SetRotationtoFalse() 
    {
        inAttackAnimation = false;
        agent.isStopped = false;
       
    }

    public void Open() 
    {
        this.enabled = true;
    }
}
