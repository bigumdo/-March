using BGD.Agents;
using BGD.Casters;
using BGD.Players;
using UnityEngine;

namespace BGD.Weapons
{
    public class PlayerBullet : MonoBehaviour
    {
        [SerializeField] private float _radius;

        private float _damage;
        private Rigidbody2D _rbCompo;
        private float _speed = 10;

        private void Awake()
        {
            _rbCompo = GetComponent<Rigidbody2D>();
        }

        public void SetBullet(float damage, Vector2 dir, float speed)
        {
            transform.right = dir;
            _damage = damage;
            _speed = speed;
        }

        private void FixedUpdate()
        {
            _rbCompo.linearVelocity = transform.right * _speed;
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, _radius, Vector2.zero);
            if (hit.collider != null && hit.collider.TryGetComponent(out Player player))
            {
                player.heal
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
