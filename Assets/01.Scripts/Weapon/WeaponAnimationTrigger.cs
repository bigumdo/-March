using System;
using UnityEngine;

namespace BGD.Weapons
{
    public class WeaponAnimationTrigger : MonoBehaviour
    {
        public event Action OnAnimationEndTrigger;

        public void AnimaitonEnd() => OnAnimationEndTrigger?.Invoke();
    }
}
