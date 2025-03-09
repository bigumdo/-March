using BGD.Agents;
using BGD.Animators;
using BGD.FSM;
using BGD.Weapons;
using System;
using UnityEngine;

namespace BGD.Players
{
    public class PlayerAttackState : AgentState
    {
        //private float _attackDelayTime;

        private PlayerMover _mover;
        private AgentAttackCompo _attackCompo;
        private Player _player;
        private AgentWeapon _weapon;

        public PlayerAttackState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {
            _player = agent as Player;
            _mover = agent.GetCompo<PlayerMover>();
            _attackCompo = agent.GetCompo<AgentAttackCompo>();
            _weapon = agent.GetCompo<AgentWeapon>();
            //_attackDelayTime = 0.2f;
        }

        public override void Enter()
        {
            base.Enter();
            _isEndTrigger = false;
            _mover.CanMove = false;
            _mover.StopImmediately(true);
            _weapon.CurrentWeapon.animTrigger.OnAnimationEndTrigger += HandleAttackEndEvent;
            SetAttackData();
        }
        private void SetAttackData()
        {
            float atkDirection = _renderer.FacingDirection;
            float xInput = _player.PlayerInput.InputDirection.x; //입력된 x방향
            if (Mathf.Abs(xInput) > 0)
                atkDirection = Mathf.Sign(xInput); // 키보드로 누르고 있는 방향을 우선한다.

            //AttackDataSO atkData = _attackCompo.GetAttackData($"PlayerCompoAttack{_attackComboCnt}");

            //Vector2 movement = atkData.attackMove;
            //movement.x *= atkDirection;
            //
            //_attackCompo.SetAttackData(atkData);
        }

        public override void Exit()
        {
            _mover.CanMove = true;
            _mover.StopImmediately();
            _weapon.CurrentWeapon.animTrigger.OnAnimationEndTrigger -= HandleAttackEndEvent;
            base.Exit();
        }

        private void HandleAttackEndEvent()
        {
            _isEndTrigger = true;
        }

        public override void Update()
        {
            base.Update();

            if (_isEndTrigger)
                _player.ChangeState(FSMState.IDLE);
        }

        
    }
}
