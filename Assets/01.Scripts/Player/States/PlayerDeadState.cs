using BGD.Agents;
using BGD.Animators;
using BGD.FSM;
using UnityEngine;

namespace BGD.Players
{
    public class PlayerDeadState : AgentState
    {
        public PlayerDeadState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {

        }
    }
}
