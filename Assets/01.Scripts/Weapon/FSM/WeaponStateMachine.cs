using BGD.Agents;
using BGD.FSM;
using BGD.Players;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Weapons
{
    public class WeaponStateMachine
    {
        public WeaponState CurrentState { get; private set; }

        private Dictionary<WeaponStateEnum, WeaponState> _states;
        public WeaponStateMachine(Weapon weapon, WeaponStateListSO list)
        {
            _states = new Dictionary<WeaponStateEnum, WeaponState>();
            foreach(WeaponStateSO state in list.states)
            {
                try
                {
                    Type t = Type.GetType(state.className);
                    var entityState = Activator.CreateInstance(t, weapon, state.animParam) as WeaponState;
                    _states.Add(state.stateName, entityState);
                }
                catch(Exception ex)
                {
                    Debug.LogError($"{state.stateName}로딩 문제있음 , Error.Message : {ex.Message}");
                }
            }
        }
        public void Initialize(WeaponStateEnum startState)
        {
            CurrentState = _states[startState];
            CurrentState.Enter();
        }

        public void ChangeState(WeaponStateEnum changeState)
        {
            CurrentState.Exit();
            CurrentState = _states[changeState];
            CurrentState.Enter();
        }

        public void Update()
        {
            CurrentState.Update();
        }
    }
}
