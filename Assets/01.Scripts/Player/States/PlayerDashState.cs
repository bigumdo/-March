using BGD.Agents;
using BGD.Animators;
using BGD.Cores;
using BGD.FSM;
using BGD.StatSystem;
using DG.Tweening;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace BGD.Players
{
    public class PlayerDashState : AgentState
    {
        private Player _player;
        private AgentMover _mover;
        private AgentAfterimage _afterimage;
        private float _lastShootingTime = -10;
        private float _shootDealyTime;

        public PlayerDashState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {
            _player = agent as Player;
            _mover = agent.GetCompo<AgentMover>(true);
            _afterimage = agent.GetCompo<AgentAfterimage>(true);
            
        }

        public override void Enter()
        {
            _player.gameObject.layer = LayerMask.NameToLayer("Ghost");
            StatSO stat = _player.GetCompo<AgentStat>().GetStat(_player.dashCooltimeStat);
            if (stat.Value + _lastShootingTime > Time.time)
            {
                _player.ChangeState(FSMState.IDLE);
            }
            else
            {
                base.Enter();
                _mover.CanMove = false;
                _mover.StopImmediately(true);
                StatSO dashPowerStat = _player.GetCompo<AgentStat>().GetStat(_player.dashPowerStat);
                Vector2 dir = (MouseManager.Instance.MouseDir - (Vector2)_player.transform.position).normalized;
                _mover.AddForce(dir * dashPowerStat.Value);
                _afterimage.Play();
            }
        }

        public override void Update()
        {
            base.Update();
            RaycastHit2D hit = Physics2D.Raycast(_player.transform.position, _player.transform.right,0.5f,1 << LayerMask.NameToLayer("Ground"));

            if (_isEndTrigger || hit.collider != null)
            {
                _lastShootingTime = Time.time;
                _player.ChangeState(FSMState.IDLE);
                _mover.StopImmediately(true);
                _afterimage.Stop();
            }
        }

        public override void Exit()
        {
            _player.gameObject.layer = LayerMask.NameToLayer("Player");
            _mover.CanMove = true;
            base.Exit();
        }
    }
}
