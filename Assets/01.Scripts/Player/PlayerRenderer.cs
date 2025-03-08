using BGD.Agents;
using BGD.Cores;
using BGD.Players;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace BGD.Players
{
    public class PlayerRenderer : AgentRenderer
    {
        public float MouseAngle { get; private set; }
        private Player _player;

        public override void Initialize(Agent agent)
        {
            base.Initialize(agent);
            _player = agent as Player;
        }
        private void Update()
        {
            Vector2 MouseDir = MouseManager.Instance.MouseDir;
            MouseAngle = Mathf.Atan2(MouseDir.y - _player.transform.position.y
                , MouseDir.x - _player.transform.position.x) * Mathf.Rad2Deg;
            float xMove = MouseDir.x > 0 ? 1 : -1;
            FlipControl(xMove);
        }
    }
}
