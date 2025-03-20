using BGD.Agents;
using BGD.Animators;
using BGD.FSM;
using BGD.StatSystem;
using UnityEngine;

namespace BGD.Players
{
    public class PlayerJumpState : PlayerAirState
    {
        private AgentStat _stat;

        public PlayerJumpState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {
            _stat = agent.GetCompo<AgentStat>();
        }

        public override void Enter()
        {
            base.Enter();
            _mover.StopImmediately(true);
            StatSO jumpPowerStat = _stat.GetStat(_player.jumpPowerStat);
            _mover.AddForceY(jumpPowerStat.Value);
        }

        public override void Update()
        {
            base.Update();
            if(_mover.Velocity.y < 0)
            {
                _player.ChangeState(FSMState.FALL);
            }
        }
    }
}
