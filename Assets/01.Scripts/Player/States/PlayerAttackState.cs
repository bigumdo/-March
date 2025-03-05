using BGD.Agents;
using BGD.Animators;
using BGD.FSM;
using UnityEngine;

namespace BGD.Players
{
    public class PlayerAttackState : AgentState
    {
        private float _lastAttackTime = 0;
        private float _attackDelayTime;
        private int _attackComboCnt = 0;

        private PlayerMover _mover;
        private AgentAttackCompo _attackCompo;
        private Player _player;

        public PlayerAttackState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {
            _player = agent as Player;
            _mover = agent.GetCompo<PlayerMover>();
            _attackCompo = agent.GetCompo<AgentAttackCompo>();
            _attackDelayTime = 0.2f;
        }

        public override void Enter()
        {
            base.Enter();
            if (Time.time > _lastAttackTime + _attackDelayTime
                || _attackComboCnt > 2)
            {
                _attackComboCnt = 0;
            }

            _renderer.SetParam(_player.attackCompoParam, _attackComboCnt);
            _mover.CanMove = false;
            _mover.StopImmediately(true);
            SetAttackData();
        }
        private void SetAttackData()
        {
            float atkDirection = _renderer.FacingDirection;
            float xInput = _player.PlayerInput.InputDirection.x; //입력된 x방향
            if (Mathf.Abs(xInput) > 0)
                atkDirection = Mathf.Sign(xInput); // 키보드로 누르고 있는 방향을 우선한다.

            AttackDataSO atkData = _attackCompo.GetAttackData($"PlayerCompoAttack{_attackComboCnt}");

            Vector2 movement = atkData.attackMove;
            movement.x *= atkDirection;
            _mover.AddForce(movement);

            _attackCompo.SetAttackData(atkData);
        }

        public override void Exit()
        {
            _attackComboCnt++;
            _lastAttackTime = Time.time;
            _mover.CanMove = true;
            _mover.StopImmediately();

            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if (_isEndTrigger)
                _player.ChangeState(FSMState.IDLE);
        }

        
    }
}
