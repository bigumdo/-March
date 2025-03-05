using BGD.Agents;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.FSM
{
    public class StateMachine
    {
        public AgentState currentState { get; private set; }

        private Dictionary<FSMState, AgentState> _states;

        public StateMachine(Agent agent, AgentStateListSO stateList)
        {
            _states = new Dictionary<FSMState, AgentState>();
            foreach (StateSO state in stateList.states)
            {
                try
                {
                    Type t = Type.GetType(state.className);
                    var entityState = Activator.CreateInstance(t, agent, state.animParam) as AgentState;
                    _states.Add(state.stateName, entityState);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"{state.stateName}로딩 문제있음 , Error.Message : {ex.Message}");
                }
            }
        }

        public void Initialize(FSMState startState)
        {
            currentState = GetState(startState);
            currentState.Enter();
        }

        public void ChangeState(FSMState changeState)
        {
            currentState.Exit();
            currentState = GetState(changeState);
            currentState.Enter();
        }

        public AgentState GetState(FSMState state) => _states.GetValueOrDefault(state);
    }
}
