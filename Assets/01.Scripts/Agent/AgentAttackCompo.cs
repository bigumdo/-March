using BGD.Casters;
using BGD.StatSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Agents
{
    public class AgentAttackCompo : MonoBehaviour, IAgentComponent, IAfterInit
    {
        private AgentCaster _caster;
        private AgentMover _mover;
        private AgentStat _agentStat;
        private Dictionary<string, AttackDataSO> _attackDictionary;
        private AttackDataSO _currentAttackData;
        private AgentAnimationTrigger _animTrigger;
        private float _damage;

        [SerializeField]private StatSO _damageStat;
        [SerializeField]private List<AttackDataSO> _attackDatas;

        public float Damge { get; private set; }

        public void Initialize(Agent agent)
        {
            _caster = agent.GetCompo<AgentCaster>();
            _mover = agent.GetCompo<AgentMover>();
            _agentStat = agent.GetCompo<AgentStat>();
            _animTrigger = agent.GetCompo<AgentAnimationTrigger>();
            _attackDictionary =  new Dictionary<string, AttackDataSO>();
            _attackDatas.ForEach(data => _attackDictionary.Add(data.dataName,data));
        }
        public void AfterInit()
        {
            _damageStat = _agentStat.GetStat(_damageStat);
            _animTrigger.OnAttackTrigger += HandleAttackTrigger;
        }

        private void HandleAttackTrigger()
        {
            Damge = _damageStat.Value;
            _caster.Cast(CastTypeEnum.Damge);
        }

        public AttackDataSO GetAttackData(string dataName)
        {
            AttackDataSO data = _attackDictionary.GetValueOrDefault(dataName,null);
            Debug.Assert(data!=null,$"{dataName}없는 데이터.");
            return data;
        }

        public void SetAttackData(AttackDataSO changeAtkData)
        {
            _currentAttackData = changeAtkData;
        }
    }
}
