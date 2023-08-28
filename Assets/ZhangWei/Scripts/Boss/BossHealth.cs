using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZhangWei
{
    public class BossHealth : MonoBehaviour
    {
        public BehaviorTree move;
        public int maxHP;
        public BehaviorTree[] stopTrees;

        private int hp;
        private LootSpawner spawner;

        public int HP
        {
            get { return hp; }
            set { hp = value; }
        }

        private Animator animator;
        private int dieID;

        private void Awake()
        {
            spawner = GetComponent<LootSpawner>();
            animator = GetComponent<Animator>();
            dieID = Animator.StringToHash("Die");
            HP = maxHP;
            TimeLinePlayControl.Instance.TimeLinePlayControlAction += Open;
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
            HP = Mathf.Clamp(HP, 0, HP);
            if (HP == 0)
            {
                animator.SetTrigger(dieID);
                UnityEngine.GameManager.Instance.BossCount++;

                foreach (var tree in stopTrees)
                {
                    tree.DisableBehavior();
                }

                //GameManager.Instance.GameOver();
                if (spawner)
                {
                    spawner.Spawnloot();
                }
            }
        }

        public void Open()
        {
            move.enabled = true;
        }
    }
}