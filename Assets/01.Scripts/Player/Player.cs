using BGD.Agents;
using BGD.Animators;
using BGD.FSM;
using BGD.Obstacles;
using BGD.StatSystem;
using System;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace BGD.Players
{
    public class Player : Agent
    {
        public AgentStateListSO states;
        public AnimParamSO attackCompoParam;
        public AgentHealth Health { get; private set; }
        [field : SerializeField] public PlayerInputSO PlayerInput {get; private set;}
        [HideInInspector] public BasePlatform Platform;

        [Header("Stat")]
        public StatSO jumpPowerStat;
        public StatSO jumpCntStat;
        public StatSO dashPowerStat;
        public StatSO dashCooltimeStat;
        public StatSO dashCntStat;

        private StateMachine _stateMachine;
        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new StateMachine(this, states);
            _stateMachine.Initialize(FSMState.IDLE);
            Health = GetCompo<AgentHealth>();
        }

        protected override void AfterInitComponenets()
        {
            base.AfterInitComponenets();
        }

        private void Update()
        {
            _stateMachine.currentState.Update();
        }

        public void ChangeState(FSMState changeState)
        {
            _stateMachine.ChangeState(changeState);
        }

        public override void HandleDeadEvent()
        {
            base.HandleDeadEvent();
            ChangeState(FSMState.DEAD);
        }
    }
}
