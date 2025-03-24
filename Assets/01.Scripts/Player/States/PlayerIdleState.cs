using BGD.Agents;
using BGD.Animators;
using BGD.FSM;
using UnityEngine;

namespace BGD.Players 
{
    public class PlayerIdleState : PlayerGroundState
    {
        public PlayerIdleState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            //_mover.SetRigidType(RigidbodyType2D.Kinematic);
            _mover.StopImmediately();
            _mover.CanMove = false;
        }

        public override void Update()
        {
            base.Update();
            //_mover.StopImmediately(true);
            if (Mathf.Abs(_player.PlayerInput.InputDirection.x) > 0.5f)
            {
                _player.ChangeState(FSMState.MOVE);
            }
            if(!_mover.IsGrounded)
            {
                _player.ChangeState(FSMState.FALL);
            }
        }

        public override void Exit()
        {
            _mover.CanMove = true;
            //_mover.SetRigidType(RigidbodyType2D.Dynamic);
            base.Exit();
        }
    }
}
