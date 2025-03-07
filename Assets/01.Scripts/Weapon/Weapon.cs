using BGD.Animators;
using BGD.Cores;
using System;
using UnityEngine;

namespace BGD.Weapons
{
    public class Weapon : MonoBehaviour
    {
        public bool isEndTrigger;

        [SerializeField] private Transform _firePos;
        [SerializeField] private AnimParamSO _shootParam;

        [HideInInspector] public WeaponAnimationTrigger animTrigger;
        private WeaponRenderer _renderer;

        private void Awake()
        {
            animTrigger = GetComponentInChildren<WeaponAnimationTrigger>();
            _renderer = GetComponentInChildren<WeaponRenderer>();
        }

        private void Start()
        {
            animTrigger.OnAnimationEndTrigger += HandleAttackEndTrigger;
        }

        private void OnDestroy()
        {
            animTrigger.OnAnimationEndTrigger -= HandleAttackEndTrigger;
        }

        private void HandleAttackEndTrigger()
        {
            isEndTrigger = true;
            _renderer.SetParam(_shootParam, false);
        }

        public void Shooting()
        {
            isEndTrigger = false;
            _renderer.SetParam(_shootParam,true);
        }

        private void Update()
        {

        }
    }
}
