using BGD.Animators;
using BGD.Players;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace BGD.Weapons
{
    public class WeaponState
    {
        protected Weapon _weapon;
        protected AnimParamSO _animParam;
        protected bool _isEndTrigger;

        protected WeaponRenderer _renderer;
        protected WeaponAnimationTrigger _animTrigger;

        public WeaponState(Weapon weapon, AnimParamSO animParam)
        {
            _weapon = weapon;
            _animParam = animParam;
            _renderer = weapon.GetComponentInChildren<WeaponRenderer>();
            _animTrigger = weapon.GetComponentInChildren<WeaponAnimationTrigger>();
        }

        public virtual void Enter()
        {
            _renderer.SetParam(_animParam, true);
            _animTrigger.OnAnimationEndTrigger += AnimationEndTrigger;
            _isEndTrigger = false;
        }

        public virtual void Exit()
        {
            _animTrigger.OnAnimationEndTrigger -= AnimationEndTrigger;
            _renderer.SetParam(_animParam, false);
        }

        public virtual void Update()
        {

        }

        public virtual void AnimationEndTrigger()
        {
            _isEndTrigger = true;
        }
    }
}
