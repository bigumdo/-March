using BGD.StatSystem;
using System;
using UnityEngine;

namespace BGD.Agents
{
    public class AgentHealth : MonoBehaviour, IAgentComponent, IAfterInit
    {
        private Agent _agent;
        private AgentStat _agentStat;

        private float _currentHealth;
        private float _maxHealth;

        public void Initialize(Agent agent)
        {
            _agent = agent;
            _agentStat = agent.GetCompo<AgentStat>();
        }

        public void AfterInit()
        {
            _currentHealth = _maxHealth = _agentStat.HpStat.Value;
        }

        public void ApplyDamage(float damage)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
            if (_currentHealth <= 0)
                _agent.OnDeadEvent?.Invoke();
        }
    }
}
