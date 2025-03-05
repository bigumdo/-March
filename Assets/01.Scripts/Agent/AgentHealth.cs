using System;
using UnityEngine;

namespace BGD.Agents
{
    public class AgentHealth : MonoBehaviour, IAgentComponent
    {
        public Agent _agent;

        public void Initialize(Agent agent)
        {
            _agent = agent;
        }

        internal void ApplyDamage(float damage)
        {

        }
    }
}
