using BGD.Players;
using BGD.Weapons;
using System;
using UnityEngine;

namespace BGD.Agents
{
    public class AgentWeapon : MonoBehaviour, IAgentComponent, IAfterInit
    {
        public Weapon CurrentWeapon { get; private set; }

        [SerializeField] private PlayerInputSO _playerInput;

        private Player _player;
        private PlayerRenderer _renderer;
        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            _renderer = agent.GetCompo<PlayerRenderer>();
            CurrentWeapon = GetComponentInChildren<Weapon>();
        }
        public void AfterInit()
        {
            _playerInput.AttackEvent += HandleAttackHandle;
        }

        private void OnDestroy()
        {
            _playerInput.AttackEvent -= HandleAttackHandle;
        }

        private void HandleAttackHandle()
        {
            CurrentWeapon.Shooting();
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

