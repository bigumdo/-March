using BGD.Animators;
using BGD.Players;
using System;
using UnityEngine;

namespace BGD.Weapons
{
    public class PistolEmptyingState : WeaponState
    {
        private Player _player;
        public PistolEmptyingState(Weapon weapon, AnimParamSO animParam) : base(weapon, animParam)
        {
            _player = weapon.transform.root.GetComponent<Player>();
        }

        public override void Enter()
        {
            base.Enter();
            _player.PlayerInput.ReloadAction += HandleReloadAction;
        }

        public override void Exit()
        {
            _player.PlayerInput.ReloadAction -= HandleReloadAction;
            base.Exit();
        }

        private void HandleReloadAction()
        {
            _weapon.ChangeState(WeaponStateEnum.Reload);
        }
    }
}
