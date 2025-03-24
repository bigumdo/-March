using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.FSM
{
    public enum FSMState
    {
        IDLE,
        MOVE,
        DEAD,
        ATTACK,
        BATTLE,
        JUMP,
        FALL,
        DASH
    }

    [CreateAssetMenu(menuName = "SO/FSM/AgentStateListSO")]
    public class AgentStateListSO : ScriptableObject
    {
        public List<StateSO> states = new List<StateSO>();
    }
}
