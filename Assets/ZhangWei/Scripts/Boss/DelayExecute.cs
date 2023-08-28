using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZhangWei
{
    public class DelayExecute : MonoBehaviour
    {
        public float delay;
        public UnityEvent delayRunMethod;

        private float _timer;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= delay)
            {
                delayRunMethod.Invoke();
                _timer = Mathf.NegativeInfinity;
            }
        }
    }
}