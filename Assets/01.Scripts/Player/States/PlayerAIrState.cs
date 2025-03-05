using BGD.Agents;
using BGD.Animators;
using BGD.FSM;
using UnityEngine;

namespace BGD.Players
{
    public class PlayerAirState : AgentState
    {
        protected Player _player;
        protected PlayerMover _mover;
        public PlayerAirState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {
            _player = agent as Player;
            _mover = agent.GetCompo<PlayerMover>();
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();

            _mover.SetMovement(_player.PlayerInput.InputDirection.x);
        }
    }
}
