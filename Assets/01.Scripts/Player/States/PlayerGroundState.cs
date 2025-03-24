using BGD.Agents;
using BGD.Animators;
using BGD.FSM;
using System;
using System.Diagnostics;
using UnityEngine;

namespace BGD.Players
{
    public abstract class PlayerGroundState : AgentState
    {
        protected PlayerMover _mover;
        protected Player _player;
        protected PlayerGroundState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {
            _player = agent as Player;
            _mover = agent.GetCompo<PlayerMover>();
        }

        public override void Enter()
        {
            base.Enter();
            //_player.PlayerInput.AttackEvent += HandleAttackEvent;
            _player.PlayerInput.JumpEvent += HandleJumpEvent;
            _player.PlayerInput.DashEvent += HandleDashEvent;
        }

        public override void Exit()
        {
            //_player.PlayerInput.AttackEvent -= HandleAttackEvent;
            _player.PlayerInput.JumpEvent -= HandleJumpEvent;
            _player.PlayerInput.DashEvent -= HandleDashEvent;
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if (!_mover.IsGrounded)
                _player.ChangeState(FSMState.FALL);
        }

        private void HandleDashEvent()
        {
            _player.ChangeState(FSMState.DASH);
        }

        private void HandleJumpEvent()
        {
            if(_mover.CanJump)
            _player.ChangeState(FSMState.JUMP);
        }

        private void HandleAttackEvent()
        {
            _player.ChangeState(FSMState.ATTACK);
        }
    }
}
