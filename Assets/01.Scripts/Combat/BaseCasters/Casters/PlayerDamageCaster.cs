using BGD.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Casters
{
    public class PlayerDamageCaster : BaseCaster, IColliderCaster
    {
        private AgentAttackCompo _atkCompo;

        public override void Initialize(Agent agent)
        {
            base.Initialize(agent);
            _atkCompo = agent.GetCompo<AgentAttackCompo>();
        }

        public  bool ColliderCast(Collider2D[] colliders)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent(out AgentHealth health))
                {

                    health.ApplyDamage(_atkCompo.Damge);
                }
            }
            return false;
        }
    }
}
