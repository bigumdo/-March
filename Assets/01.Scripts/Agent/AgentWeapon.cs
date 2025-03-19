using BGD.Players;
using BGD.Weapons;
using System;
using UnityEngine;

namespace BGD.Agents
{
    public class AgentWeapon : MonoBehaviour, IAgentComponent
    {
        public Weapon CurrentWeapon { get; private set; }

        private Player _player;
        private PlayerRenderer _renderer;

        private float shootingDelay;
        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            _renderer = agent.GetCompo<PlayerRenderer>();
            CurrentWeapon = GetComponentInChildren<Weapon>();
        }

        private void Update()
        {
            if(_renderer.FacingDirection > 0)
                CurrentWeapon.transform.localRotation = Quaternion.Euler(0, 0, _renderer.MouseAngle);
            else
                CurrentWeapon.transform.localRotation = Quaternion.Euler(180, 180, -_renderer.MouseAngle);
        }
    }
}

