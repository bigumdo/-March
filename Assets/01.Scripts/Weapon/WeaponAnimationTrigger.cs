using BGD.Animators;
using System;
using UnityEngine;

namespace BGD.Weapons
{
    public class WeaponAnimationTrigger : MonoBehaviour
    {
        public event Action OnAnimationEndTrigger;
        public event Action OnAttackTrigger;

        public void AnimaitonEnd() => OnAnimationEndTrigger?.Invoke();
        public void AttackTrigger() => OnAttackTrigger?.Invoke();

    }
}
