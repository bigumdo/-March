using BGD.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Casters
{
    public class AttackRangeCaster : BaseCaster, IColliderCaster
    {
        private AgentAttackCompo _atkCompo;

        public override void Initialize(Agent agent)
        {
            base.Initialize(agent);
            _atkCompo = agent.GetCompo<AgentAttackCompo>();
        }
        public bool ColliderCast(Collider2D[] colliders)
        {

            return true;
        }
    }
}
