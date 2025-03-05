using UnityEngine;

namespace BGD.Casters
{
    public interface IRaycastCaster
    {
        public void RayCast(RaycastHit2D hit);
    }
}
