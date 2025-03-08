using BGD.Agents;
using BGD.Animators;
using BGD.FSM;
using BGD.StatSystem;
using System;
using UnityEngine;

namespace BGD.Players
{
    public class Player : Agent
    {
        public AgentStateListSO states;
        public AnimParamSO attackCompoParam;
        public AgentHealth Health { get; private set; }
        [field : SerializeField] public PlayerInputSO PlayerInput {get; private set;}

        [Header("Stat")]
        public StatSO jumpPowerStat;

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
    }
}
