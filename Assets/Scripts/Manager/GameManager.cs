using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UnityEngine
{
    public class GameManager : Singleton<GameManager>
    {
        public CharactrStats playerStats;

        /*private CinemachineFreeLook followCamera;
        public Userlogin userlogin;*/

        List<IEndGameObserver> endGameObservers = new List<IEndGameObserver>();

        public string userName;
        public string userPassword;


        private int enemyCount;
        [SerializeField]
        private int enemyRound = 0;

        private int bossCount;

        private bool allBossesDead;

        public bool isDamaging = false;
        public bool isBack = false;
        public bool openPortral = false;


        public GameObject portral;

        public Room Nowroom;
        public string nextSceneName;
        public int EnemyCount
        {
            get => enemyCount;

            set
            {
                enemyCount = value;
                if (enemyCount == 48)
                {
                    Debug.Log("48");
                    enemyCount = 0;
                    EnemyRound++;
                    Nowroom.DoorController(-1);
                }

            }
        }

        public int BossCount 
        {
            get => bossCount;

            set
            {
                bossCount = value;
                if (BossCount == 2)
                {
                    Debug.Log("Bosscount!!" + BossCount);  
                    UIRootDontDestroy.uiRootDontDestroy.SetSuccessUI();
                    bossCount = 0;
                    
                }

            }
        }

        public int EnemyRound 
        {
            get => enemyRound;

            set 
            {
                enemyRound = value;
                if (EnemyRound ==1) 
                {
                    OpenPortral = true;
                    Vector3 vector3 = GameObject.FindWithTag("Player").transform.position;
                    vector3.x += 4;
                    vector3.y += 2;
                    vector3.z += 4;
                    Instantiate<GameObject>(portral, vector3, Quaternion.identity);
                }
            }
        }

        public bool AllBossesDead
        {
            get => allBossesDead;

            set
            {
                allBossesDead = value;
                
            }
        }

        public bool OpenPortral
        {
            get => OpenPortral;

            set
            {
                openPortral = value;
            }
        }



        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
            //SceneManager.sceneLoaded += (scene, mode) =>
            //{
            //    Debug.Log(scene.name);
            //    if (scene.name == "Loading")
            //    {
            //        GameObject.Find("ProgressText").SetActive(true);
            //    }
            //};
        }



        /*public void RigisterPlayer(CharactrStats player)
        {
            playerStats = player;

            followCamera = FindObjectOfType<CinemachineFreeLook>();

            if(followCamera != null)
            {
                followCamera.Follow = playerStats.transform;
                followCamera.LookAt = playerStats.transform.GetChild(2);
            }

        }*/

    
        public void AddObserver(IEndGameObserver observer)
        {
            endGameObservers.Add(observer);
        }

        public void RemoveObserver(IEndGameObserver observer)
        {
            endGameObservers.Remove(observer);
        }

        public void NotifyObserver()
        {
            foreach (var observer in endGameObservers)
            {
                observer.EndNotify();
            }
        }

        public Transform GetEntrance()
        {
            foreach (var item in FindObjectsOfType<TransitionDestination>())
            {
                if (item.destinationTag == TransitionDestination.DestinationTag.ENTER)
                    return item.transform;
            }
            return null;
        }

        public void EnemyTakeDamage(GameObject obj, int damage)
        {
            //Debug.Log("in EnemyTakeDamage  ");

            if (obj.CompareTag("Enemy1"))
            {
                //Debug.Log("in Shikamaru");
                if(obj != null)
                obj.GetComponent<CharactrStats>().GetHit(damage);
            }
            else if (obj.CompareTag("EnemyDamageBox"))
            {
                //Debug.Log("In Zhangwei");
                obj.GetComponentInParent<ZhangWei.Enemy>().TakeDamage(damage);
            }
            else if (obj.CompareTag("Boss"))
            {
                obj.GetComponent<CharactrStats>().GetHit(damage);
            }
            else if (obj.CompareTag("ZhangWeiBoss"))
            {
                //Debug.Log("ZhangWeiBoss TakeDamage: " + damage);
                obj.GetComponent<ZhangWei.BossHealth>().TakeDamage(damage);
            }
            else
            {
                Debug.Log("没有找到敌人脚本，扣血失败");
            }
        }
    }

}