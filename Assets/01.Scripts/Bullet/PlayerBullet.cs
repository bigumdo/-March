using BGD.Agents;
using BGD.Casters;
using BGD.Interactions;
using BGD.ObjPooling;
using BGD.Players;
using BGD.UI;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.UIElements;

namespace BGD.Weapons
{
    public class PlayerBullet : PoolableMono
    {
        [SerializeField] private float _radius;
        [SerializeField] private float _speed = 20;
        [SerializeField] LayerMask _targetLayer;
        [SerializeField] GameObject _particle;

        private float _damage;
        private Rigidbody2D _rbCompo;
        private bool test;
        private const string _popTextName = "PopUpText";

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
            if (!test)
                _rbCompo.linearVelocity = transform.right * _speed;
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, _radius, transform.right, 2, _targetLayer);
            if (hit.collider != null && test == false)
            {
                _rbCompo.linearVelocity = Vector2.zero;

                test = true;
                PopUpText text = PoolingManager.Instance.Pop(_popTextName) as PopUpText;
                text.textMesh.text = _damage.ToString();
                text.transform.position = hit.point;

                //GameObject effect = Instantiate(_particle);
                //effect.transform.position = hit.point;
                //effect.transform.rotation = Quaternion.Euler(new Vector3(-transform.rotation.eulerAngles.z + 180, 90, 0));
                //if (hit.collider.TryGetComponent(out BaseInteraction interaction))
                //{
                //    interaction.Interaction();
                //}
                PoolingManager.Instance.Push(this);

            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        public override void ResetItem()
        {
            test = false;
        }
    }
}
