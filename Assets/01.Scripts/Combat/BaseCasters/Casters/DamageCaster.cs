using BGD.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Casters
{
    public class DamageCaster : BaseCaster, IColliderCaster
    {
        [SerializeField] private int _damage;
        private float _listTime = 0;

        public bool ColliderCast(Collider2D[] colliders)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent(out AgentHealth health))
                {
                    health.ApplyDamage(_damage);
                    _listTime = Time.time;
                }
            }
            return false;
        }
    }
}
