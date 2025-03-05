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
            _player.PlayerInput.AttackEvent += HandleAttackEvent;
            _player.PlayerInput.JumpEvent += HandleJumpEvent;
        }

        public override void Exit()
        {
            _player.PlayerInput.AttackEvent -= HandleAttackEvent;
            _player.PlayerInput.JumpEvent -= HandleJumpEvent;
            base.Exit();
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
