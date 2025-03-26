using BGD.Players;
using System.Collections;
using UnityEngine;

namespace BGD.Obstacles
{
    public class BasePlatform : MonoBehaviour
    {
        protected Collider2D _collider;

        protected virtual void Awake()
        {
            // �ڽ��� �ݶ��̴��� �����´�
            _collider = GetComponent<Collider2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //���� collision�� PlayyerComponent�� ������ �ִ��� üũ�ϰ� p�� out
            if (collision.transform.TryGetComponent(out Player p)
                && transform.position.y <= collision.transform.position.y)
            {
                //player�� Platform�� �ڽ����� ����
                p.Platform = this;
                Enter();
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent<Player>(out Player p))
            {
                Exit();
                // ��� �ִ� ������ ������ Platform���� ����
                p.Platform = null;
            }
        }

        protected virtual void Enter()
        {

        }

        protected virtual void Exit()
        {

        }

        public void SetIgnore(Collider2D target, float time)
        {
            StartCoroutine(IgnoreCollision(target, time));
        }

        private IEnumerator IgnoreCollision(Collider2D target, float time)
        {
            Physics2D.IgnoreCollision(target, _collider, true);//���θ� �����Ѵ�.
            yield return new WaitForSeconds(time);
            Physics2D.IgnoreCollision(target, _collider, false);//���θ� �������� �ʴ´�.
        }
    }
}
