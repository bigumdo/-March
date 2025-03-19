using BGD.Animators;
using UnityEngine;

namespace BGD.Weapons
{
    public class WeaponRenderer : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        //public void SetParam(AnimParamSO animParam, bool value) => _animator.SetBool(animParam.hashValue, value);
        public void SetParam(AnimParamSO animParam, bool value)
        {
            _animator.SetBool(animParam.hashValue, value);
        }
        public void SetParam(AnimParamSO animParam) => _animator.SetTrigger(animParam.hashValue);


    }
}
