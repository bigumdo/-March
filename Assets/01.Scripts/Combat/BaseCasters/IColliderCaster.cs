using UnityEngine;

namespace BGD.Casters
{
    public interface IColliderCaster
    {
        public bool ColliderCast(Collider2D[] colliders);
    }
}
