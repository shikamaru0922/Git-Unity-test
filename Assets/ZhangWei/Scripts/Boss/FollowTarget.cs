using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace ZhangWei
{
    public class FollowTarget : MonoBehaviour
    {
        public string targetTag;
        public bool isLerp;
        public float speed;
        public float duration;
        private float _timer;
        private Transform _target;

        // Start is called before the first frame update
        void Start()
        {
            _target = GameObject.FindGameObjectWithTag(targetTag).transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (isLerp)
            {
                LerpFollow();
            }
            else
            {
                AbsolutelyFollow();
            }
        }

        void LerpFollow()
        {
            if (_timer >= duration)
                return;
            Vector3 pos = _target.position;
            pos.y = transform.position.y;
            transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
            _timer += Time.deltaTime;
        }

        void AbsolutelyFollow()
        {
            transform.position = _target.position;
        }
    }
}