using BGD.Animators;
using System;
using UnityEngine;

namespace BGD.Weapons
{
    public class PistolShootingState : WeaponState
    {
        private Weapon _weapon;
        private float _lastShootingTime;
        private float _shootDealyTime;


        public PistolShootingState(Weapon weapon, AnimParamSO animParam) : base(weapon, animParam)
        {
            _weapon = weapon;
            _shootDealyTime = weapon.WeaponDataSO.dealyTime;
        }

        public override void Enter()
        {
            if (_shootDealyTime + _lastShootingTime > Time.time)
            {
                _weapon.ChangeState(WeaponStateEnum.Idle);
                return;
            }
            base.Enter();
            if (!_weapon.CanShooting)
            {
                _weapon.ChangeState(WeaponStateEnum.Empty);
            }
            _animTrigger.OnAttackTrigger += HandleAttackEvent;
        }

        public override void Update()
        {
            base.Update();
            if (_isEndTrigger)
            {
                _weapon.ChangeState(WeaponStateEnum.Idle);
            }
        }

        public override void Exit()
        {
            _animTrigger.OnAttackTrigger -= HandleAttackEvent;
            base.Exit();
        }

        private void HandleAttackEvent()
        {
            _weapon.Fire();
        }

        public override void AnimationEndTrigger()
        {
            base.AnimationEndTrigger();
            _weapon.BulletReduction();
            _lastShootingTime = Time.time;
        }

    }
}
