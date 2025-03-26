using BGD.Agents;
using BGD.Animators;
using BGD.FSM;
using UnityEngine;

namespace BGD.Players
{
    public class PlayerDeadState : AgentState
    {
        private Player _player;

        public PlayerDeadState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {
            _player = agent as Player;
        }

        public override void Enter()
        {
            base.Enter();
            _player.GetCompo<AgentWeapon>().gameObject.SetActive(false);
        }
    }
}
