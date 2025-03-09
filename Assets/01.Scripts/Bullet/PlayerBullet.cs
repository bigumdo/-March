using BGD.Agents;
using BGD.Casters;
using BGD.Interactions;
using BGD.Players;
using UnityEngine;

namespace BGD.Weapons
{
    public class PlayerBullet : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private float _speed = 20;

        private float _damage;
        private Rigidbody2D _rbCompo;

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
            if (hit.collider != null && hit.collider.TryGetComponent(out BaseInteraction interaction))
            {
                interaction.Interaction();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
