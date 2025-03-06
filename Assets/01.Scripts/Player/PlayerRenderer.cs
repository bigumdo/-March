using BGD.Agents;
using BGD.Players;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace BGD.Players
{
    public class PlayerRenderer : AgentRenderer
    {
        public Vector2 MouseDir { get; private set; }
        public float MouseAngle { get; private set; }
        private Player _player;

        public override void Initialize(Agent agent)
        {
            base.Initialize(agent);
            _player = agent as Player;
        }
        private void Update()
        {
            MouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
            MouseAngle = Mathf.Atan2(MouseDir.y - _player.transform.position.y
                , MouseDir.x - _player.transform.position.x) * Mathf.Rad2Deg;
            float xMove = MouseDir.x > 0 ? 1 : -1;
            FlipControl(xMove);
        }

        public override void FlipControl(float xMove)
        {
            if (Mathf.Abs(FacingDirection + xMove) < 0.5f)
                Flip();
        }

        public override void Flip()
        {
            FacingDirection *= -1;
            _player.transform.Rotate(0, 180, 0);
            //_agent.transform.localScale = new Vector3(FacingDirection,1,1);
        }
    }
}
