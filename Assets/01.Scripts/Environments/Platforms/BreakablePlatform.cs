using DG.Tweening;
using System;
using UnityEngine;

namespace BGD.Obstacles
{
    public class BreakablePlatform : BasePlatform
    {
        [SerializeField] private float _reSpawnTime;
        private float _currentTime;
        private Animator _animator;
        private PlatformAnimTrigger _animTrigger;
        private bool _isOnPlatform;

        protected override void Awake()
        {
            base.Awake();
            _animator = transform.Find("Visual").GetComponent<Animator>();
            _animTrigger = GetComponentInChildren<PlatformAnimTrigger>();
        }

        private void Start()
        {
            _animTrigger.OnPlatformTrigger += HandlePlatformTriggerEvent;
            _animTrigger.OnRestoreTrigger += HandlePlatformRestoreEvent;
        }

        private void OnDestroy()
        {
            _animTrigger.OnPlatformTrigger -= HandlePlatformTriggerEvent;
            _animTrigger.OnRestoreTrigger -= HandlePlatformRestoreEvent;
        }

        private void HandlePlatformRestoreEvent()
        {
            _collider.enabled = true;
        }

        private void HandlePlatformTriggerEvent()
        {
            _collider.enabled = false;
        }

        protected override void Enter()
        {
            base.Enter();
            _animator.SetBool("BREAK", true);
            _isOnPlatform = true;
        }
        protected override void Exit()
        {
            base.Exit();
            if(_isOnPlatform)
                DOVirtual.DelayedCall(_reSpawnTime, () =>
                {
                    _animator.SetBool("BREAK", false);
                });
        }
    }
}
