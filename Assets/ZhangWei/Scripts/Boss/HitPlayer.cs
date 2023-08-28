using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZhangWei
{
    public class HitPlayer : MonoBehaviour
    {
        public int damage;
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Player))
            {
                // TODO: 玩家受伤
                //Debug.Log("击中玩家");
                other.GetComponent<CharactrStats>().GetHit(damage);
                UnityEngine.GameManager.Instance.isDamaging = true;
            }
        }
    }
}