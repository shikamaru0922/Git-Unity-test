using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ZhangWei
{
    public class BossAttack : MonoBehaviour
    {
        public List<SkillConfig> skillConfigs; // 所有需要举起法杖释放的技能配置

        private List<Skill> _canUseSkills; // CD冷却好，可以使用的技能

        private List<Skill> _cdSkills; // 冷却中的技能，不能使用

        public bool CanSkill => _canUseSkills != null && _canUseSkills.Count > 0;

        public string skillStr;
        [HideInInspector] public int skillID; // 由此动画触发此技能

        // Start is called before the first frame update
        void Start()
        {
            _canUseSkills = new List<Skill>(skillConfigs.Count);
            _cdSkills = new List<Skill>(skillConfigs.Count);
            skillConfigs.ForEach(skillConfig =>
                _canUseSkills.Add(new Skill(skillConfig.skillPrefab, skillConfig.cd,
                    skillConfig.generateTransform))); // 初始化将所有技能加入可用列表里
            skillID = Animator.StringToHash(skillStr);
        }

        // Update is called once per frame
        void Update()
        {
            UpdateSkillState();
        }

        private void UpdateSkillState() // 更新技能状态
        {
            List<Skill> waitToReomveCDSkill = new List<Skill>(_cdSkills.Count); // 待从_cdSkills中移除的已冷却好的技能
            _cdSkills.ForEach(cdSkill =>
            {
                float countDown = cdSkill.UpdateCountDown(); // 每帧调用技能的倒计时
                if (countDown == 0)
                {
                    waitToReomveCDSkill.Add(cdSkill); // 将冷却好的技能加入到waitToReomveCDSkill
                }
            });
            waitToReomveCDSkill.ForEach(cdSkill =>
            {
                _cdSkills.Remove(cdSkill); // 从_cdSkills中移除所有已冷却号的技能
                _canUseSkills.Add(cdSkill); // 将冷却号的技能加入到_canUseSkills
            });
        }

        public void RandomSkill() // 随机一个技能
        {
            if (_canUseSkills.Count == 0)
            {
                throw new Exception("当前没有可用技能或所有技能均在冷却中");
                return;
            }

            int idx = Random.Range(0, _canUseSkills.Count); // 随机一个技能索引
            Skill skill = _canUseSkills[idx];
            skill.GenerateSkillObj(transform.rotation);
            _canUseSkills.Remove(skill); // 从可用技能中移除
            _cdSkills.Add(skill); // 加入冷却列表
        }
    }

    [System.Serializable]
    public struct SkillConfig
    {
        public GameObject skillPrefab; // 技能预制体
        public float cd; // 技能CD
        public Transform generateTransform; // 技能生成位置
    }

    public class Skill
    {
        private GameObject _skillPrefab; // 技能预制体
        private GameObject _skillObj; // 实例化出来的技能
        private float _cd; // 技能CD
        private float _cdCountDown; // 技能CD倒计时，为0时才能释放技能
        private Transform _generateTransform; // 技能生成位置

        public Skill(GameObject skillPrefab, float cd, Transform generateTransform)
        {
            _skillPrefab = skillPrefab ? skillPrefab : throw new ArgumentNullException(nameof(skillPrefab));
            _cd = cd;
            _generateTransform = generateTransform
                ? generateTransform
                : throw new ArgumentNullException(nameof(generateTransform));
        }

        public float UpdateCountDown() // 每帧调用进行技能CD倒计时
        {
            if (_cdCountDown > 0)
            {
                _cdCountDown -= Time.deltaTime;
            }

            if (_cdCountDown < 0)
            {
                _cdCountDown = 0;
            }

            return _cdCountDown;
        }

        public void GenerateSkillObj(Quaternion rotation = default) // 实例化技能
        {
            if (!_skillPrefab)
            {
                throw new Exception("技能预制体为空，实例化失败");
                return;
            }

            if (_cdCountDown != 0)
            {
                throw new Exception("技能CD未冷却，实例化失败");
                return;
            }

            GameObject.Instantiate(_skillPrefab, _generateTransform.position, rotation);
            _cdCountDown = _cd; // 技能进入CD
        }

        public void DestorySkillObj(float delay = default) // 销毁技能GameObject
        {
            if (!_skillObj)
            {
                throw new Exception("销毁技能GameObject失败，_skillObj为空");
                return;
            }

            GameObject.Destroy(_skillObj, delay);
        }
    }
}