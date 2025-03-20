using BGD.Animators;
using UnityEngine;

namespace BGD.Weapons
{
    public class PistolReloadState : WeaponState
    {
        public PistolReloadState(Weapon weapon, AnimParamSO animParam) : base(weapon, animParam)
        {
        }

        public override void Update()
        {
            base.Update();
            if (_isEndTrigger)
                _weapon.ChangeState(WeaponStateEnum.Idle);
        }

        public override void AnimationEndTrigger()
        {
            base.AnimationEndTrigger();
            _weapon.Reload();
        }

    }
}
