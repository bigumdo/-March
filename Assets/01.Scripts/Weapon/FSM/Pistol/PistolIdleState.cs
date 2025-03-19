using BGD.Animators;
using BGD.Players;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BGD.Weapons
{
    public class PistolIdleState : WeaponState
    {
        private Player _player;
        public PistolIdleState(Weapon weapon, AnimParamSO animParam) : base(weapon, animParam)
        {
            _player = weapon.transform.root.GetComponent<Player>();
        }

        public override void Enter()
        {
            base.Enter();
            _player.PlayerInput.AttackEvent += HandleAttackHandle;
            _player.PlayerInput.ReloadAction += HandleReloadHandle;
        }

        public override void Exit()
        {
            _player.PlayerInput.AttackEvent -= HandleAttackHandle;
            _player.PlayerInput.ReloadAction -= HandleReloadHandle;
            base.Exit();
        }

        private void HandleReloadHandle()
        {
            _weapon.ChangeState(WeaponStateEnum.Reload);
        }

        private void HandleAttackHandle()
        {
            _weapon.ChangeState(WeaponStateEnum.Shooting);
        }
    }
}
