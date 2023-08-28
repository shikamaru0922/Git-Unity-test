using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZhangWei
{
    /// <summary>
    /// ��⹥������ײCube
    /// </summary>
    public class HitCube : MonoBehaviour
    {
        public Transform follow;
        private Vector3 offset;
        private Transform realFollowTarget;
        private Collider _collider;
        public event Action<GameObject> OnHitPlayer;

        // Start is called before the first frame update
        void Start()
        {
            realFollowTarget = new GameObject("RealFollowTarget").transform;
            realFollowTarget.parent = follow;
            realFollowTarget.position = transform.position;
            realFollowTarget.forward = transform.forward;
            offset = transform.position - realFollowTarget.position;
            _collider = GetComponent<Collider>();
            if (_collider == null)
                Debug.LogError("HitCube��δ�����ײ��");
            else
                _collider.enabled = false;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = realFollowTarget.position + offset;
            transform.forward = realFollowTarget.forward;
        }

        public void ShowCollider()
        {
            _collider.enabled = true;
        }

        public void HideCollider()
        {
            _collider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == Tags.Player)
            {
                OnHitPlayer.Invoke(other.gameObject);
            }
        }
    }
}