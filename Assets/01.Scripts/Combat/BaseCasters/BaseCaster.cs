using BGD.Agents;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace BGD.Casters
{
    public enum CastMethodType
    {
        Circle,
        Box,
        Ray
    }

    public enum RayDirection
    {
        Right,
        Left,
        Up,
        Down
    }


    public abstract class BaseCaster : MonoBehaviour
    {
        protected Agent _agent;

        public CastMethodType castMethodType;
        public CastTypeEnum castType;//캐스트 타입
        public LayerMask targetLayer;//어떤 것을 캐스트 할 것인가

        public Vector2 castOffset;
        public Vector2 castSize;
        public RayDirection rayDirection;
        public float rayDistance;
        public float castRange;
        public int castCnt;//얼마나 캐스트할 것인가


        public virtual void Initialize(Agent agent)
        {
            _agent = agent;
        }

        private void OnDrawGizmosSelected()
        {
            Vector2 castDir;
            if (_agent == null)
                castDir = new Vector2(castOffset.x * 1, castOffset.y);
            else
                castDir = new Vector2(castOffset.x * _agent.GetCompo<AgentRenderer>(true).FacingDirection, castOffset.y);
            Gizmos.color = Color.green;

            switch (castMethodType)
            {
                case CastMethodType.Circle:
                    Gizmos.DrawWireSphere((Vector2)transform.position + castOffset,castRange);
                    break;
                case CastMethodType.Box:
                    Gizmos.DrawWireCube((Vector2)transform.position + castOffset,castSize);
                    break;
                case CastMethodType.Ray:
                    Vector2 dir;
                    switch (rayDirection)
                    {
                        case RayDirection.Right:
                            dir = Vector2.right;
                            break;
                        case RayDirection.Left:
                            dir = Vector2.left;
                            break;
                        case RayDirection.Up:
                            dir = Vector2.up;
                            break;
                        case RayDirection.Down:
                            dir = Vector2.down;
                            break;
                        default:
                            dir = Vector2.right;
                            break;
                    }

                    Gizmos.DrawRay((Vector2)transform.position + castOffset, dir * rayDistance);
                    break;
            }
        }
    }
}
