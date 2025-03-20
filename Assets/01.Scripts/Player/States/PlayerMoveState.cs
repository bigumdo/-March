using BGD.Agents;
using BGD.Animators;
using BGD.StatSystem;
using UnityEngine;

namespace BGD.Players
{
    public class PlayerMoveState : PlayerGroundState
    {
        public PlayerMoveState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            _mover.SetMovement(_player.PlayerInput.InputDirection.x);
            float xInput = _player.PlayerInput.InputDirection.x;
            if (Mathf.Abs(xInput) < 0.5f)
            {
                _player.ChangeState(FSM.FSMState.IDLE);
            }
        }
    }
}
