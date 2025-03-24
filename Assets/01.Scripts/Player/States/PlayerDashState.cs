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

namespace BGD.Players
{
    public class PlayerDashState : AgentState
    {
        private Player _player;
        private AgentMover _mover;
        private AgentAfterimage _afterimage;
        private float _lastShootingTime;
        private float _shootDealyTime;

        public PlayerDashState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {
            _player = agent as Player;
            _mover = agent.GetCompo<AgentMover>(true);
            _afterimage = agent.GetCompo<AgentAfterimage>(true);
            
        }

        public override void Enter()
        {
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
            if (_isEndTrigger)
            {
                _lastShootingTime = Time.time;
                _player.ChangeState(FSMState.IDLE);
                _mover.StopImmediately(true);
                _afterimage.Stop();
            }
        }

        public override void Exit()
        {
            _mover.CanMove = true;
            base.Exit();
        }

    }
}
