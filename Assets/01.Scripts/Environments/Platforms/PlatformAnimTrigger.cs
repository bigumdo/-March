using System;
using UnityEngine;

namespace BGD.Obstacles
{
    public class PlatformAnimTrigger : MonoBehaviour
    {
        public event Action OnPlatformTrigger;
        public event Action OnRestoreTrigger;

        public void AnimationTrigger() => OnPlatformTrigger?.Invoke();
        public void AnimationRestore() => OnRestoreTrigger?.Invoke();
    }
}
