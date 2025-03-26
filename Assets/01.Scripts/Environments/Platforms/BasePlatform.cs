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
            // 자신의 콜라이더를 가져온다
            _collider = GetComponent<Collider2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //다은 collision가 PlayyerComponent를 가지고 있는지 체크하고 p로 out
            if (collision.transform.TryGetComponent(out Player p)
                && transform.position.y <= collision.transform.position.y)
            {
                //player에 Platform을 자신으로 설정
                p.Platform = this;
                Enter();
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent<Player>(out Player p))
            {
                Exit();
                // 밝고 있다 떠났기 떄문에 Platform에서 빼기
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
            Physics2D.IgnoreCollision(target, _collider, true);//서로를 무시한다.
            yield return new WaitForSeconds(time);
            Physics2D.IgnoreCollision(target, _collider, false);//서로를 무시하지 않는다.
        }
    }
}
