using BGD.Agents;
using BGD.Animators;
using BGD.FSM;
using BGD.StatSystem;
using UnityEngine;

namespace BGD.Players
{
    public class PlayerFallState : PlayerAirState
    {
        public PlayerFallState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {
        }

        public override void Update()
        {
            base.Update();
            if(_mover.IsGrounded)
            {
                _player.ChangeState(FSMState.IDLE);
                _mover.ResetJumpCnt();
            }
        }
    }
}
