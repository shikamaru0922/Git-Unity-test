using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ZhangWei
{
    public abstract class Enemy : MonoBehaviour
    {
        private SphereCollider atkDisCollider; // ���ƹ��������ShpereCollider
        private BoxCollider damageCollider; // �ӵ���ײ��Collider
        private NavMeshAgent agent; // ����Ѱ·��NavMeshAgent
        private NavMeshObstacle meshObstacle; // ���ڷ�ֹ��������ӵ��
        private EnemyData data; // ��������
        private HitCube hitCube; // ���ڹ�����ײ���
        private Animator animator;
        private float dieAnimationTime; // ��ǰ��������������ʱ��
        private float recoveryEnemyTime; // ������������������Ϻ���յ������ǰ�ȴ���ʱ��
        private GameObject[] playerObjs;
        private int atkID;
        private int moveID;
        private int dieID;
        private bool stopAtk;
        private bool stopPathfind;
        private LootSpawner spawner;

        public bool IsDie => data.HP == 0;

        private void OnEnable()
        {
            if (stopPathfind)
            {
                StartCoroutine(Pathfinding());
            }

            if (damageCollider != null)
            {
                damageCollider.enabled = true;
            }
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <param name="data">��������</param>
        /// <param name="atkDisCollider">���ƹ�������</param>
        /// <param name="agent">���ڵ���</param>
        public void Init(EnemyData data, SphereCollider atkDisCollider, NavMeshAgent agent,
            NavMeshObstacle meshObstacle, HitCube hitCube, Animator animator)
        {
            this.data = data;
            this.atkDisCollider = atkDisCollider;
            this.agent = agent;
            this.meshObstacle = meshObstacle;
            this.hitCube = hitCube;
            this.animator = animator;

            spawner = GetComponent<LootSpawner>();

            damageCollider = transform.Find("DamageBox").GetComponent<BoxCollider>();

            recoveryEnemyTime = GameManager.Instance.recoveryEnemyTime;

            playerObjs = GameObject.FindGameObjectsWithTag(Tags.Player); // ��ȡ��Ϸ�е���Ҷ���
            agent.enabled = false;
            agent.angularSpeed = 360;
            meshObstacle.enabled = false; // �ر�NavMeshObstacle
            meshObstacle.carving = false;

            hitCube.OnHitPlayer += Hit; // ע�ṥ�������¼�

            atkID = Animator.StringToHash("Atk");
            moveID = Animator.StringToHash("Move");
            dieID = Animator.StringToHash("Die");

            // ���һЩ��ʼ������
            atkDisCollider.radius = data.AtkDis; // ������Χ
            atkDisCollider.isTrigger = true; // ��Ϊ������
            agent.speed = data.MoveSpeed; // �����ƶ��ٶ�
            agent.stoppingDistance = data.StopDis; // NavMeshAgent��ֹͣ����
            animator.Play("Idle");
            dieAnimationTime = animator.GetClipLength("Die"); // ��ǰ��������������ʱ��
            StartCoroutine(Pathfinding());
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="target"></param>
        protected virtual IEnumerator Attack(Transform target)
        {
            stopAtk = false;
            while (!stopAtk)
            {
                // ÿһ�ι���֮ǰ�ȼ�⳯���Ƿ���ȷ
                Vector3 pos = target.position;
                pos.y = transform.position.y;
                Vector3 dir = pos - transform.position;
                // �������
                if (Vector3.Angle(transform.forward, dir) > 1)
                {
                    while (true)
                    {
                        transform.forward = Vector3.Slerp(transform.forward, dir, Time.deltaTime * 8);
                        if (Vector3.Angle(transform.forward, dir) < 1)
                        {
                            break;
                        }

                        yield return null;
                    }
                }

                // �������֮��Ź���
                animator.SetTrigger(atkID); // ���Ź�������
                yield return new WaitForSeconds(data.AtkCD);
            }
        }

        /// <summary>
        /// ֹͣ����
        /// </summary>
        private void StopAtk()
        {
            stopAtk = true;
        }

        /// <summary>
        /// �����������
        /// </summary>
        private void Hit(GameObject obj)
        {
            GameManager.Instance.PlayerTakeDamage(obj, data.Dmg);
        }

        public void TakeDamage(int damage)
        {
            data.HP = Mathf.Clamp(data.HP - damage, 0, data.MaxHP);
            //Debug.Log($"受伤：{damage}");
            if (data.HP == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            animator.SetBool(dieID, true);
            Invoke("OnDieAnimationEnd", dieAnimationTime + recoveryEnemyTime);

            if (!stopPathfind)
            {
                stopPathfind = true;
                agent.isStopped = true;
                agent.enabled = false;
                meshObstacle.carving = false;
                meshObstacle.enabled = false;
                agent.velocity = Vector3.zero; // �����Կ��õ�������ֹͣ
            }

            damageCollider.enabled = false;

            GameManager.Instance.AddPlayerHP(data.AddPlayerHP); // ����һ�Ѫ
            GameManager.Instance.AddPlayerEngery(data.AddPlayerEnergy); // ����һ�ŭ��
        }

        public void ResetData(EnemyData data)
        {
            this.data = data;
        }

        /// <summary>
        /// ��������������Ϻ�
        /// </summary>
        private void OnDieAnimationEnd()
        {
            ObjPools.Instance.SetObj(data.Name, gameObject); // ��������
            
            UnityEngine.GameManager.Instance.EnemyCount++;
            
            if (spawner)
            {
                spawner.Spawnloot();
            }
        }

        /// <summary>
        /// ����������Ѱ·��ÿ֡����
        /// </summary>
        private IEnumerator Pathfinding()
        {
            animator.SetBool(moveID, true); // �����ƶ�����

            AnimatorStateInfo stateInfo;
            while (true)
            {
                stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                if (stateInfo.IsTag(Tags.Move)) // �жϵ�ǰ�Ƿ����ڲ���Move������ֻ�в���Move�����ſ�ʼѰ·�ƶ�
                    break;

                yield return null;
            }

            // �ر�NavMeshObstacleֹͣ�ڶ���������һ֡����NavMeshAgent�Է�ֹ˲��
            meshObstacle.enabled = false;
            meshObstacle.carving = false;
            yield return null;

            agent.enabled = true;
            stopPathfind = false;
            agent.isStopped = false;

            while (!stopPathfind)
            {
                NavMeshPath path = new NavMeshPath();
                Vector3[] corners;
                Transform minDisPlayer = null; // Ѱ·������̵����
                float minSqrDis = float.MaxValue;

                // ѭ���ҳ���ǰѰ·������̵����
                foreach (GameObject playerObj in playerObjs)
                {
                    if (agent.CalculatePath(playerObj.transform.position, path)) // ģ������Ѱ·��ÿ����ҵ�·��
                    {
                        corners = path.corners;

                        // ���������·������ܾ���
                        float sqrDis = 0;
                        if (corners.Length > 0)
                        {
                            sqrDis = Vector3.SqrMagnitude(transform.position - corners[0]);
                            for (int i = 1; i < corners.Length; i++)
                                sqrDis += Vector3.SqrMagnitude(corners[i - 1] - corners[i]);
                        }

                        if (minSqrDis > sqrDis)
                        {
                            minSqrDis = sqrDis;
                            minDisPlayer = playerObj.transform;
                        }
                    }
                }

                if (minDisPlayer != null)
                {
                    agent.SetDestination(minDisPlayer.position); // ������̾�������Ѱ·
                }

                yield return null;
            }
        }

        private void StopPathfind()
        {
            if (!stopPathfind && agent.enabled)
            {
                stopPathfind = true;
                agent.isStopped = true;
                agent.enabled = false;
                agent.velocity = Vector3.zero; // �����Կ��õ�������ֹͣ
                animator.SetBool(moveID, false); // ֹͣ�ƶ�����
                // ֹͣѰ·����NavMeshObstacle�������ڶ�                                 
                meshObstacle.enabled = true;
                meshObstacle.carving = true;
            }
        }

        /// <summary>
        /// ���������п�ʼ���й������ʱ��֡�¼����������˵Ĺ�����ײ���
        /// </summary>
        public void ShowHitCube()
        {
            hitCube.ShowCollider();
        }

        /// <summary>
        /// ���������н����������ʱ��֡�¼����رյ��˵Ĺ�����ײ���
        /// </summary>
        public void HideHitCube()
        {
            hitCube.HideCollider();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == Tags.Player)
            {
                StopPathfind(); // ֹͣѰ·
                StartCoroutine(Attack(other.transform)); // ��ҽ��빥����Χʱ����
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == Tags.Player)
            {
                StopAtk(); // ֹͣ����
                StartCoroutine(Pathfinding()); // ����뿪������ΧʱѰ·
            }
        }
    }

    public class MeleeEnemy : Enemy
    {
        //protected override void Attack(Transform target)
        //{
        //    base.Attack(target);
        //    Debug.Log("��ս���˹���");
        //}
    }

    public class DistanceEnemy : Enemy
    {
        //protected override void Attack(Transform target)
        //{
        //    base.Attack(target);
        //    Debug.Log("Զ�̵��˹���");
        //}
    }
}